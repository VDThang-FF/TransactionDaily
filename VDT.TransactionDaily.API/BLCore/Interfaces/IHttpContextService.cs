namespace VDT.TransactionDaily.API.BLCore.Interfaces
{
    public interface IHttpContextService
    {
        /// <summary>
        /// Lấy thông tin cookie qua name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        string GetCookieByName(string name);

        /// <summary>
        /// Lấy thông tin request hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        HttpRequest GetCurrentRequest();

        /// <summary>
        /// Lấy thông tin response hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        HttpResponse GetCurrentResponse();

        /// <summary>
        /// Lấy ngữ cảnh hiện tại
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        HttpContext GetCurrentContext();
    }
}
