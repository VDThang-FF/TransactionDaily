using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Others;

namespace VDT.TransactionDaily.API.BL.Implements
{
    public class ProductBL : BaseBL<VdtProduct>, IProductBL
    {
        public ProductBL(CoreService coreService) : base(coreService)
        {
            SubsystemCode = "Product";
        }
    }
}
