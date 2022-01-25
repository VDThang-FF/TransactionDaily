using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng lưu trữ thông tin người dùng
    /// </summary>
    public partial class VdtUser : BaseModel
    {
        public VdtUser()
        {
            VdtProductDictionaries = new HashSet<VdtProductDictionary>();
            VdtProducts = new HashSet<VdtProduct>();
            VdtTransactionDetails = new HashSet<VdtTransactionDetail>();
            VdtTransactions = new HashSet<VdtTransaction>();
            VdtUserSessions = new HashSet<VdtUserSession>();
        }

        [Key]
        public uint Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<VdtProductDictionary> VdtProductDictionaries { get; set; }
        public virtual ICollection<VdtProduct> VdtProducts { get; set; }
        public virtual ICollection<VdtTransactionDetail> VdtTransactionDetails { get; set; }
        public virtual ICollection<VdtTransaction> VdtTransactions { get; set; }
        public virtual ICollection<VdtUserSession> VdtUserSessions { get; set; }
    }
}
