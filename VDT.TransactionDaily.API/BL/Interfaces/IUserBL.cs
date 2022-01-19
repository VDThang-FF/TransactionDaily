using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Responses;

namespace VDT.TransactionDaily.API.BL.Interfaces
{
    public interface IUserBL : IBaseBL<VdtUser>
    {
        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        ServiceResponse Register(User registerModel);

        /// <summary>
        /// Đăng nhập người dùng
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        ServiceResponse Login(UserLogin loginModel);
    }
}
