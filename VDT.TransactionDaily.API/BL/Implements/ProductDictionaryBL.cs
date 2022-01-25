using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class ProductDictionaryBL : BaseBL<VdtProductDictionary>, IProductDictionayBL
    {
        public ProductDictionaryBL(CoreService coreService) : base(coreService)
        {
        }
    }
}
