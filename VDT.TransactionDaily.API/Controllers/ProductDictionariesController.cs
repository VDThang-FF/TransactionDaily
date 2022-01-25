using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDT.TransactionDaily.API.Authorize;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;

namespace VDT.TransactionDaily.API.Controllers
{
    [VDTAuthorize]
    public class ProductDictionariesController : BasesController<VdtProductDictionary>
    {
        private readonly IProductDictionayBL _productDictionayBL;

        public ProductDictionariesController(IProductDictionayBL productDictionayBL)
        {
            BL = productDictionayBL;
        }
    }
}
