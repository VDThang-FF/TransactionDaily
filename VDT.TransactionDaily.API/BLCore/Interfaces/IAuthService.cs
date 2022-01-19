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
    }
}
