using Microsoft.Extensions.Primitives;
using System.Net;
using UAParser;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Helper
{
    public static class Network
    {
        /// <summary>
        /// Lấy địa chỉ IP máy client
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public static string GetClientIP(HttpRequest httpRequest)
        {
            try
            {
                var ip = string.Empty;
                if (httpRequest == null)
                    return ip;

                if (httpRequest.Headers != null && httpRequest.Headers.ContainsKey("X-Forwarded-For"))
                {
                    var forward = httpRequest.Headers["X-Forwarded-For"];
                    if (!StringValues.IsNullOrEmpty(forward))
                        ip = forward.FirstOrDefault();
                }

                if (string.IsNullOrWhiteSpace(ip) && httpRequest.HttpContext.Connection != null && httpRequest.HttpContext.Connection.RemoteIpAddress != null)
                    ip = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();

                // Nếu IP tồn tại dải IPV6 loopback thì thực hiện để lấy IPV4 như sau:
                // Tập hợp IP của client bao gồm IPV6 ở dải trên và IPV4 ở dải dưới
                // IP trên cùng là của card mạng thực
                // IP sau có thể là của card mạng ảo (máy ảo)
                // --> IPV4 = tổng số IP / 2
                if (!string.IsNullOrWhiteSpace(ip) && ip.Equals("::1", StringComparison.OrdinalIgnoreCase))
                {
                    var hostName = Dns.GetHostName();
                    var ipHostEntry = Dns.GetHostEntry(hostName);
                    if (ipHostEntry != null && ipHostEntry.AddressList.Length > 0)
                        ip = ipHostEntry.AddressList[Convert.ToInt32(Math.Floor(((double)(ipHostEntry.AddressList.Length / 2))))].ToString();
                }

                if (!string.IsNullOrWhiteSpace(ip) && ip.Contains(","))
                    ip = ip.Split(',').FirstOrDefault();

                return ip;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Lấy ID Device
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public static string GetDeviceID(HttpRequest httpRequest, ref HttpResponse httpResponse)
        {
            var deviceID = string.Empty;

            if (httpRequest != null && httpRequest.Headers != null && httpRequest.Headers.ContainsKey(CookieKey.DeviceID))
                deviceID = httpRequest.Headers[CookieKey.DeviceID];

            if (string.IsNullOrWhiteSpace(deviceID) && httpRequest != null && httpRequest.Cookies != null
                                                        && httpRequest.Cookies.ContainsKey(CookieKey.DeviceID))
                deviceID = httpRequest.Cookies[CookieKey.DeviceID];

            if (string.IsNullOrWhiteSpace(deviceID))
            {
                deviceID = Guid.NewGuid().ToString();
                if (httpResponse != null)
                    Converter.AddCookie(httpResponse, CookieKey.DeviceID, deviceID);
            }

            return deviceID;
        }

        /// <summary>
        /// Lấy tên Device
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public static string GetDeviceName(HttpRequest httpRequest, ref HttpResponse httpResponse)
        {
            try
            {
                var deviceName = string.Empty;

                if (httpRequest == null)
                    return deviceName;

                if (httpRequest.Headers != null && httpRequest.Headers.ContainsKey(CookieKey.DeviceName))
                    deviceName = httpRequest.Headers[CookieKey.DeviceName];

                if (string.IsNullOrWhiteSpace(deviceName) && httpRequest != null && httpRequest.Cookies != null
                                                        && httpRequest.Cookies.ContainsKey(CookieKey.DeviceName))
                    deviceName = httpRequest.Cookies[CookieKey.DeviceName];

                if (httpRequest.Headers != null && httpRequest.Headers.ContainsKey(CookieKey.UserAgent))
                {
                    var _parse = Parser.GetDefault();
                    var clientInfo = _parse.Parse(httpRequest.Headers[CookieKey.UserAgent]);
                    deviceName = clientInfo.Device.ToString();
                }

                if (httpResponse != null)
                    Converter.AddCookie(httpResponse, CookieKey.DeviceName, deviceName);

                return deviceName;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Lấy thông tin trình duyệt
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public static string GetBrowser(HttpRequest httpRequest)
        {
            try
            {
                var browser = string.Empty;

                if (httpRequest == null)
                    return browser;

                if (httpRequest.Headers != null && httpRequest.Headers.ContainsKey(CookieKey.UserAgent))
                {
                    var _parse = Parser.GetDefault();
                    var clientInfo = _parse.Parse(httpRequest.Headers[CookieKey.UserAgent]);
                    browser = clientInfo.UA.ToString();
                }

                return browser;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Lấy thông tin hệ điều hành
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public static string GetOSName(HttpRequest httpRequest)
        {
            try
            {
                var osName = string.Empty;

                if (httpRequest == null)
                    return osName;

                if (httpRequest.Headers != null && httpRequest.Headers.ContainsKey(CookieKey.UserAgent))
                {
                    var _parse = Parser.GetDefault();
                    var clientInfo = _parse.Parse(httpRequest.Headers[CookieKey.UserAgent]);
                    osName = clientInfo.OS.ToString();
                }

                return osName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
