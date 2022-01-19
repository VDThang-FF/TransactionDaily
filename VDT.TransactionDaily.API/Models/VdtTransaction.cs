using System;
using System.Collections.Generic;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng thực hiện lưu trữ thông tin đơn hàng
    /// </summary>
    public partial class VdtTransaction
    {
        public VdtTransaction()
        {
            VdtTransactionDetails = new HashSet<VdtTransactionDetail>();
        }

        /// <summary>
        /// Khóa chính
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// ID người dùng
        /// </summary>
        public uint UserId { get; set; }
        /// <summary>
        /// Mã giao dịch đơn hàng
        /// </summary>
        public string Code { get; set; } = null!;
        /// <summary>
        /// Tên giao dịch đơn hàng
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Người tạo giao dịch
        /// </summary>
        public string Author { get; set; } = null!;
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }

        public virtual VdtUser User { get; set; } = null!;
        public virtual ICollection<VdtTransactionDetail> VdtTransactionDetails { get; set; }
    }
}
