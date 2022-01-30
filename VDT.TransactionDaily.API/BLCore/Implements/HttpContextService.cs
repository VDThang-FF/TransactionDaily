using VDT.TransactionDaily.API.BLCore.Interfaces;

namespace VDT.TransactionDaily.API.BLCore.Implements
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Lấy thông tin cookie qua tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public string GetCookieByName(string name)
        {
            try
            {
                if (GetCurrentRequest() != null)
                    return Convert.ToString(GetCurrentRequest().Cookies[name]);
            }
            catch (Exception)
            {

            }

            return null;
        }

        /// <summary>
        /// Lấy thông tin header qua name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// created by vdthang 27.01.2022
        public string GetHeaderByName(string name)
        {
            try
            {
                if (GetCurrentRequest() != null)
                    return Convert.ToString(GetCurrentRequest().Headers[name]);
            }
            catch (Exception)
            {

            }

            return null;
        }

        /// <summary>
        /// Lấy thông tin ngữ cảnh hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public HttpContext GetCurrentContext()
        {
            return _httpContextAccessor?.HttpContext;
        }

        /// <summary>
        /// Lấy thông tin request hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public HttpRequest GetCurrentRequest()
        {
            return _httpContextAccessor?.HttpContext?.Request;
        }

        /// <summary>
        /// Lấy thông tin response hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public HttpResponse GetCurrentResponse()
        {
            return _httpContextAccessor?.HttpContext?.Response;
        }
    }
}
