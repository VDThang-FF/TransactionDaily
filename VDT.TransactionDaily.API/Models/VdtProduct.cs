using System;
using System.Collections.Generic;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng chứa thông tin sản phẩm/hàng hóa
    /// </summary>
    public partial class VdtProduct : BaseModel
    {
        public VdtProduct()
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
        /// Mã sản phẩm
        /// </summary>
        public string Code { get; set; } = null!;
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Giá sản phẩm
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
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
