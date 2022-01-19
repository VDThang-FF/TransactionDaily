namespace VDT.TransactionDaily.API.BLCore.Interfaces
{
    public interface IConfigService
    {
        /// <summary>
        /// Lấy thông appsetting qua key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        string GetAppSetting(string key, string defaultValue = null);

        /// <summary>
        /// Lấy thông tin connection string
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        string GetConnectionString(string key);
    }
}
