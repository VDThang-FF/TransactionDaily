using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng thực hiện lưu trữ detail đơn hàng
    /// </summary>
    public partial class VdtTransactionDetail : BaseModel
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public uint Id { get; set; }
        /// <summary>
        /// ID người dùng
        /// </summary>
        public uint UserId { get; set; }
        /// <summary>
        /// Khóa ngoại đơn hàng
        /// </summary>
        public uint MasterId { get; set; }
        /// <summary>
        /// Khóa ngoại sản phẩm
        /// </summary>
        public uint ProductId { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public uint Quantity { get; set; }
        /// <summary>
        /// Tổng tiền đơn hàng
        /// </summary>
        public decimal? TotalPrice { get; set; }
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

        public virtual VdtTransaction Master { get; set; } = null!;
        public virtual VdtProduct Product { get; set; } = null!;
        public virtual VdtUser User { get; set; } = null!;
    }
}
