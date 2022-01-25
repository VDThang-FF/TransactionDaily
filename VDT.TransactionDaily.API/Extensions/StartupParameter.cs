namespace VDT.TransactionDaily.API.Extensions
{
    public static class StartupParameter
    {
        public static string Environment { get; set; }
        public static string AccessToken { get; set; }

        /// <summary>
        /// Có phải là môi trường phát triển hay không
        /// </summary>
        /// <returns></returns>
        public static bool IsDevelopment()
        {
            if (Environment.Equals("Development", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
