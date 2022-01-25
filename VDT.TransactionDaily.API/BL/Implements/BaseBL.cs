using Microsoft.EntityFrameworkCore;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class BaseBL<T> : IBaseBL<T> where T : BaseModel
    {
        protected string SubsystemCode { get; set; }
        protected uint? _userID { get; set; }
        protected string _userName { get; set; }
        protected string _userEmail { get; set; }

        protected readonly VdtContext _dbContext;

        protected DbSet<T> _entity;

        protected readonly IConfigService _configService;
        protected readonly IHttpContextService _httpContextService;
        protected readonly IAuthService _authService;

        public BaseBL(CoreService coreService)
        {
            SubsystemCode = string.Empty;
            _dbContext = coreService.VdtContext;
            _entity = _dbContext.Set<T>();
            _configService = coreService.ConfigService;
            _httpContextService = coreService.HttpContextService;
            _authService = coreService.AuthService;
            _userID = _authService.GetUserId();
            _userName = _authService.GetUserName();
            _userEmail = _authService.GetUserEmail();
        }

        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable();
        }

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public virtual PagingResponse GetPaging(int pageIndex, int pageSize, string query)
        {
            throw new NotImplementedException("Chưa ghi đè hàm lấy theo base phân trang");
        }

        /// <summary>
        /// Lấy danh sách theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetByID(uint id)
        {
            throw new NotImplementedException("Chưa ghi đè hàm lấy theo base id uint");
        }

        /// <summary>
        /// Lấy danh sách theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetByID(Guid id)
        {
            throw new NotImplementedException("Chưa ghi đè hàm lấy theo base id guid");
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public virtual ServiceResponse Insert(T entity)
        {
            var res = ValidateEntity(entity);
            if (!res.Success)
                return res;

            BeforeSave(entity);
            _entity.Add(entity);
            var effect = _dbContext.SaveChanges();
            if (effect <= 0)
                return res.OnError(Code.Success, SubCode.ErrorInsert);

            return res;
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by vdthang 24.01.2022
        public virtual ServiceResponse Update(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// created by vdthang 24.01.2022
        public virtual ServiceResponse Delete(List<string> ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public virtual ServiceResponse ValidateEntity(object data)
        {
            return new ServiceResponse();
        }

        /// <summary>
        /// Chuẩn bị dữ liệu trước khi thêm/cập nhật
        /// </summary>
        /// <param name="entity"></param>
        /// created by vdthang 24.01.2022
        public virtual void BeforeSave(T entity)
        {
            if (entity.ModelState == ModelState.Insert)
            {
                entity.CreatedBy = _userName;
                entity.CreatedDate = DateTime.UtcNow;

                // Nếu type khóa chính là Guid thì gen dữ liệu
                if (entity.GetPrimaryKeyType() == typeof(Guid))
                    entity.SetPrimaryKey(Guid.NewGuid());
            }

            if (entity.ModelState == ModelState.Update)
            {
                entity.ModifiedBy = _userName;
                entity.ModifiedDate = DateTime.UtcNow;
            }
        }
    }
}
