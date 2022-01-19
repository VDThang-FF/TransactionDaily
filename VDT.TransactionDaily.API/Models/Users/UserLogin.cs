using System.ComponentModel.DataAnnotations;

namespace VDT.TransactionDaily.API.Models
{
    public class UserLogin
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }

        [Required]
        public string Password
        {
            get;
            set;
        }
    }
}
