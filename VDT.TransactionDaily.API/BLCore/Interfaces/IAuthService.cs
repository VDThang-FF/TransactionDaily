using VDT.TransactionDaily.API.Models;

namespace VDT.TransactionDaily.API.BLCore.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Lấy thông tin người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        VdtUser GetUserInfo();

        /// <summary>
        /// Lấy ID người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        uint? GetUserId();

        /// <summary>
        /// Lấy tên đăng nhập người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        string GetUserName();

        /// <summary>
        /// Lấy email người dùng
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        string GetUserEmail();
    }
}
