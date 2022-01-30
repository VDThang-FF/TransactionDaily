using Newtonsoft.Json;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace VDT.TransactionDaily.API.Helper
{
    public static class Converter
    {
        /// <summary>
        /// Hash với thuật toán SHA256
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string CreateSHA256Hash(string input)
        {
            var algorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] hashValue = algorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(hashValue);
        }

        /// <summary>
        /// Hash với thuật toán MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string CreateMD5Hash(string input)
        {
            var hash = new StringBuilder();
            var mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashByte = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(input));
            for (int i = 0; i < hashByte.Length; i++)
            {
                hash.Append(hashByte[i].ToString("x2"));
            }
            return Convert.ToString(hash);
        }

        /// <summary>
        /// Convert dữ liệu từ DBNull Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        /// created by vdthang 21.01.2022
        public static T ConvertFromDBVal<T>(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)value;
            }
        }

        /// <summary>
        /// Thực hiện serialize object
        /// </summary>
        /// <param name="originObj"></param>
        /// <param name="igNoreNull"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string Serialize(object originObj, bool igNoreNull = false)
        {
            var jsonSetting = GetJsonSerializerSettings();
            if (igNoreNull)
                jsonSetting.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(originObj, jsonSetting);
        }

        /// <summary>
        /// Thực hiện deserialize object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static T Deserialize<T>(string jsonObj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonObj, GetJsonSerializerSettings());
            }
            catch (Exception ex)
            {
                if (typeof(T) == typeof(string))
                    return (T)((object)jsonObj);
                throw ex;
            }
        }

        /// <summary>
        /// Thực hiện deserialize object theo type chỉ định
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static object DeserializeObject(string jsonObj, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(jsonObj, type, GetJsonSerializerSettings());
            }
            catch (Exception ex)
            {
                if (type == typeof(string))
                    return jsonObj;
                throw ex;
            }
        }

        /// <summary>
        /// Khởi tạo cấu hình json setting
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffK",
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        /// <summary>
        /// Thực hiện nén dữ liệu
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string Compress(string origin)
        {
            var byteVal = Encoding.UTF8.GetBytes(origin);
            using (var originStream = new MemoryStream(byteVal))
            {
                using (var compressStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(compressStream, CompressionMode.Compress))
                    {
                        originStream.CopyTo(gzip);
                    }
                    return Convert.ToBase64String(compressStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Thực hiện giải nén dữ liệu
        /// </summary>
        /// <param name="compress"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string Decompress(string compress)
        {
            var byteVal = Convert.FromBase64String(compress);
            using (var originStream = new MemoryStream(byteVal))
            {
                using (var decompressStream = new MemoryStream())
                {
                    using (var gzip = new GZipStream(originStream, CompressionMode.Decompress))
                    {
                        gzip.CopyTo(decompressStream);
                    }
                    return Encoding.UTF8.GetString(decompressStream.ToArray());
                }
            }
        }

        public static string ENCRYPT_PASSWORD = "VDThangFPT";
        public static string ENCRYPT_SALT = "VDThangCompany";

        /// <summary>
        /// Mã hóa AES
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string EncryptAES(string plain)
        {
            if (string.IsNullOrWhiteSpace(plain))
                return plain;

            try
            {
                using (var aes = new AesManaged())
                {
                    var deriveByte = new Rfc2898DeriveBytes(ENCRYPT_PASSWORD, Encoding.UTF8.GetBytes(ENCRYPT_SALT));
                    aes.Key = deriveByte.GetBytes(128 / 8);
                    aes.IV = aes.Key;

                    using (var stream = new MemoryStream())
                    {
                        using (var encrypt = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            var utf8 = UTF8Encoding.UTF8.GetBytes(plain);
                            encrypt.Write(utf8, 0, utf8.Length);
                            encrypt.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Thực hiện giải mã AES
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public static string DecryptAES(string encrypt)
        {
            if (string.IsNullOrWhiteSpace(encrypt))
                return encrypt;

            try
            {
                using (var aes = new AesManaged())
                {
                    var deriveByte = new Rfc2898DeriveBytes(ENCRYPT_PASSWORD, Encoding.UTF8.GetBytes(ENCRYPT_SALT));
                    aes.Key = deriveByte.GetBytes(128 / 8);
                    aes.IV = aes.Key;

                    using (var stream = new MemoryStream())
                    {
                        using (var decrypt = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            var encryptData = Convert.FromBase64String(encrypt);
                            decrypt.Write(encryptData, 0, encryptData.Length);
                            decrypt.Flush();
                        }
                        var decryptData = stream.ToArray();
                        return UTF8Encoding.UTF8.GetString(decryptData, 0, decryptData.Length);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Thực hiện gán thêm cookie
        /// </summary>
        /// <param name="response"></param>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="httpOnly"></param>
        /// <param name="sameSiteMode"></param>
        /// <param name="expire"></param>
        /// <param name="domain"></param>
        /// <param name="secure"></param>
        /// created by vdthang 17.01.2022
        public static void AddCookie(ref HttpResponse response, string cookieName, string cookieValue, bool httpOnly = true, SameSiteMode sameSiteMode = SameSiteMode.Lax, DateTime? expire = null, string domain = null, bool secure = true)
        {
            var option = new CookieOptions()
            {
                HttpOnly = httpOnly,
                Secure = secure,
                SameSite = sameSiteMode
            };
            if (expire.HasValue)
                option.Expires = expire.Value;

            if (!string.IsNullOrEmpty(domain))
                option.Domain = domain;

            response.Cookies.Append(cookieName, cookieValue, option);
        }

        /// <summary>
        /// Thực hiện xóa cookie
        /// </summary>
        /// <param name="response"></param>
        /// <param name="cookieName"></param>
        /// created by vdthang 17.01.2022
        public static void DeleteCookie(HttpResponse response, string cookieName)
        {
            response.Cookies.Delete(cookieName);
        }
    }
}
