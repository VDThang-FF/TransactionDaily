using System.ComponentModel.DataAnnotations;

namespace VDT.TransactionDaily.API.Models
{
    public class User
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }

        [Required]
        public string Email
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
