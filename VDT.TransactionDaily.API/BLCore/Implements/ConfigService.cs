using VDT.TransactionDaily.API.BLCore.Interfaces;

namespace VDT.TransactionDaily.API.BLCore.Implements
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _appSettings;
        private readonly IConfigurationSection _connectionStrings;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
            _appSettings = configuration.GetSection("AppSettings");
            _connectionStrings = configuration.GetSection("ConnectionStrings");
        }

        /// <summary>
        /// Lấy thông appsetting qua key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public string GetAppSetting(string key, string defaultValue)
        {
            var value = _appSettings[key];
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(defaultValue))
                value = defaultValue;
            return value;
        }

        /// <summary>
        /// Lấy thông tin connection string
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 17.01.2022
        public string GetConnectionString(string key)
        {
            return _connectionStrings[key];
        }
    }
}
