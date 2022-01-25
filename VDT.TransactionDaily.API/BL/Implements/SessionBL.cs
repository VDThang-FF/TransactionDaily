using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Helper;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class SessionBL : BaseBL<VdtUserSession>, ISessionBL
    {
        public SessionBL(CoreService coreService) : base(coreService)
        {
        }

        /// <summary>
        /// Ghi đè hàm base lấy theo guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        public override VdtUserSession GetByID(Guid id)
        {
            return _entity.AsEnumerable().FirstOrDefault(p => p.Id == id);
        }
    }
}
