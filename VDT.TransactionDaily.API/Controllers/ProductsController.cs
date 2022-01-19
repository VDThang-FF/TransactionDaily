using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDT.TransactionDaily.API.Authorize;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;

namespace VDT.TransactionDaily.API.Controllers
{
    [VDTAuthorize]
    public class ProductsController : BasesController<VdtProduct>
    {
        public ProductsController(IProductBL productBL)
        {
            this.BL = productBL;
        }
    }
}
