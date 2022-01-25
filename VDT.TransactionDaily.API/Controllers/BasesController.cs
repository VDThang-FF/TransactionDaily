using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VDT.TransactionDaily.API.BL.Interfaces;
using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Enums;
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
        public ServiceResponse Insert([FromBody] T insertModel)
        {
            var res = new ServiceResponse();

            try
            {
                insertModel.ModelState = Enumarations.ModelState.Insert;
                res = BL.Insert(insertModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API cập nhật
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        /// created by vdthang 19.01.2022
        [HttpPut]
        public ServiceResponse Update([FromBody] T updateModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = BL.Update(updateModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API xóa
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        [HttpDelete]
        public ServiceResponse Update([FromBody] List<string> ids)
        {
            var res = new ServiceResponse();

            try
            {
                res = BL.Delete(ids);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
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

        /// <summary>
        /// API lấy dữ liệu phân trang
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        [HttpGet("Paging")]
        public ServiceResponse GetPaging([FromQuery] int pageIndex, int pageSize, string query)
        {
            var res = new ServiceResponse();

            try
            {
                res.Data = BL.GetPaging(pageIndex, pageSize, query);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }
    }
}
