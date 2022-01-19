using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.JWT;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class UserBL : BaseBL<VdtUser>, IUserBL
    {
        private readonly JwtSettings _jwtSettings;

        public UserBL(CoreService coreService, JwtSettings jwtSettings) : base(coreService)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public ServiceResponse Register(User registerModel)
        {
            var res = new ServiceResponse();

            // Thực hiện kiểm tra tên userName đã tồn tại hay chưa
            var findByUserName = _entity.FirstOrDefault(p => string.Equals(registerModel.UserName, p.UserName, StringComparison.OrdinalIgnoreCase));
            if (findByUserName != null)
                return res.OnError(Code.Success, SubCode.ErrorDuplicate, null, "Tên đăng nhập đã tồn tại!");

            // Thực hiện kiểm tra email đã tồn tại hay chưa
            var findByEmail = _entity.FirstOrDefault(p => string.Equals(registerModel.Email, p.Email, StringComparison.OrdinalIgnoreCase));
            if (findByEmail != null)
                return res.OnError(Code.Success, SubCode.ErrorDuplicate, null, "Email đã tồn tại!");

            // TODO: Thực hiện kiểm tra độ mạnh của mật khẩu

            // Thêm mới dữ liệu người dùng đăng ký vào hệ thống
            var dbUser = new VdtUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                Password = Converter.CreateSHA256Hash(registerModel.Password),
                CreatedBy = registerModel.UserName,
                CreatedDate = DateTime.UtcNow,
            };

            _entity.Add(dbUser);
            var effectInsert = _dbContext.SaveChanges();
            if (effectInsert <= 0)
                return res.OnError(Code.Success, SubCode.ErrorInsert, null, "Đăng ký người dùng thất bại!");

            return res;
        }

        /// <summary>
        /// Đăng nhập người dùng
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        public ServiceResponse Login(UserLogin loginModel)
        {
            var res = new ServiceResponse();

            // Kiểm tra tên đăng nhập và mật khẩu đã chính xác chưa
            var findByUserName = _entity.FirstOrDefault(p => string.Equals(loginModel.UserName, p.UserName, StringComparison.OrdinalIgnoreCase));
            if (findByUserName == null)
                return res.OnError(Code.Success, SubCode.ErrorNotFound, null, "Tên đăng nhập không tồn tại!");

            if (!Converter.CreateSHA256Hash(loginModel.Password).Equals(findByUserName.Password, StringComparison.OrdinalIgnoreCase))
                return res.OnError(Code.Success, SubCode.ErrorInvalid, null, "Mật khẩu không chính xác!");

            // Khởi tạo token
            var userToken = JwtHelpers.GenAccessToken(
                new UserTokens()
                {
                    UserName = loginModel.UserName,
                    Email = findByUserName.Email,
                    Id = Guid.NewGuid()
                },
                _jwtSettings
            );

            // Thêm mới bản ghi session đăng nhập vào database
            var curentRequest = _httpContextService.GetCurrentRequest();
            var currentResponse = _httpContextService.GetCurrentResponse();
            var deviceID = Network.GetDeviceID(curentRequest, ref currentResponse);

            var sessionModel = new VdtUserSession()
            {
                UserId = findByUserName.Id,
                LoginDay = DateTime.UtcNow,
                DeviceID = Guid.Parse(deviceID),
                OS = Network.GetOSName(curentRequest),
                Browser = Network.GetBrowser(curentRequest),
                ClientIp = Network.GetClientIP(curentRequest),
                AccessToken = userToken.AccessToken,
                RefreshToken = userToken.RefreshToken,
                RefreshTokenExpireTime = userToken.RefreshExpiredTime
            };

            _dbContext.VdtUserSessions.Add(sessionModel);
            var effectInsert = _dbContext.SaveChanges();
            if (effectInsert <= 0)
                return res.OnError(Code.Success, SubCode.ErrorInsert, devMessage: "Không thêm mới được bản ghi session");

            // Gán cookie đăng nhập
#if DEBUG
            Converter.AddCookie(currentResponse, CookieKey.RefreshToken, Converter.EncryptAES(userToken.RefreshToken), domain: "http://localhost:3000");
            Converter.AddCookie(currentResponse, CookieKey.AccessToken, Converter.EncryptAES(userToken.AccessToken), domain: "http://localhost:3000");
            Converter.AddCookie(currentResponse, CookieKey.UserID, Converter.EncryptAES(findByUserName.Id.ToString()), domain: "http://localhost:3000");
            Converter.AddCookie(currentResponse, CookieKey.UserName, Converter.EncryptAES(findByUserName.UserName), domain: "http://localhost:3000");
            Converter.AddCookie(currentResponse, CookieKey.Email, Converter.EncryptAES(findByUserName.Email), domain: "http://localhost:3000");
#endif

            Converter.AddCookie(currentResponse, CookieKey.RefreshToken, Converter.EncryptAES(userToken.RefreshToken));
            Converter.AddCookie(currentResponse, CookieKey.AccessToken, Converter.EncryptAES(userToken.AccessToken));
            Converter.AddCookie(currentResponse, CookieKey.UserID, Converter.EncryptAES(findByUserName.Id.ToString()));
            Converter.AddCookie(currentResponse, CookieKey.UserName, Converter.EncryptAES(findByUserName.UserName));
            Converter.AddCookie(currentResponse, CookieKey.Email, Converter.EncryptAES(findByUserName.Email));

            return res;
        }
    }
}
