using System.ComponentModel.DataAnnotations.Schema;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Models
{
    public class BaseModel
    {
        [NotMapped]
        public uint Id { get; set; }
        [NotMapped]
        public uint UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [NotMapped]
        public ModelState ModelState { get; set; }
    }
}
