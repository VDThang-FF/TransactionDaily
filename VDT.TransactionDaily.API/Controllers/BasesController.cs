using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Responses;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase where T : BaseModel
    {
        protected IBaseBL<T> BL { get; set; }

        /// <summary>
        /// API thêm mới
        /// </summary>
        /// <param name="insertModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        [HttpPost]
        public ServiceResponse Insert([FromBody] object insertModel)
        {
            return new ServiceResponse();
        }

        /// <summary>
        /// API cập nhật
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        [HttpPut]
        public ServiceResponse Update([FromBody] object updateModel)
        {
            return new ServiceResponse();
        }

        /// <summary>
        /// API lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        [HttpGet]
        public ServiceResponse GetAll()
        {
            var res = new ServiceResponse();

            try
            {
                res.Data = BL.GetAll();
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }
    }
}
