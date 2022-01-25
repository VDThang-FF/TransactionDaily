using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDT.TransactionDaily.API.Authorize;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.BLCore.Interfaces;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.JWT;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public AccountsController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        /// <summary>
        /// API đăng ký người dùng
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        [HttpPost("Register")]
        public ServiceResponse Register([FromBody] User registerModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = _userBL.Register(registerModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API đăng nhập người dùng
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        [HttpPost("Login")]
        public ServiceResponse Login([FromBody] UserLogin loginModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = _userBL.Login(loginModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API lấy thông tin người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        [HttpGet("Info")]
        [VDTAuthorize]
        public ServiceResponse UserInfo()
        {
            var res = new ServiceResponse();

            try
            {
                res.Data = _userBL.UserInfo();
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }
    }
}
