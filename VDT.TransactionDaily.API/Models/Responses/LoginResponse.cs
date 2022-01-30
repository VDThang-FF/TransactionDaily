namespace VDT.TransactionDaily.API.Models.Responses
{
    public class LoginResponse
    {
        public string SessionID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DeviceID { get; set; }
    }
}
