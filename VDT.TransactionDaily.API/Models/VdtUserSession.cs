using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng lưu trữ phiên đăng nhập người dùng
    /// </summary>
    public partial class VdtUserSession : BaseModel
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// ID người dùng
        /// </summary>
        public uint UserId { get; set; }
        /// <summary>
        /// Thời gian đăng nhập
        /// </summary>
        public DateTime LoginDay { get; set; }
        /// <summary>
        /// ID thiết bị
        /// </summary>
        public Guid DeviceId { get; set; }
        /// <summary>
        /// Hệ điều hành
        /// </summary>
        public string? Os { get; set; }
        /// <summary>
        /// Tên trình duyệt
        /// </summary>
        public string? Browser { get; set; }
        /// <summary>
        /// IP client
        /// </summary>
        public string? ClientIp { get; set; }
        /// <summary>
        /// AccessToken
        /// </summary>
        public string AccessToken { get; set; } = null!;
        /// <summary>
        /// Token refresh
        /// </summary>
        public string RefreshToken { get; set; } = null!;
        /// <summary>
        /// Hạn Token refresh
        /// </summary>
        public DateTime RefreshTokenExpireTime { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        public virtual VdtUser User { get; set; } = null!;
    }
}
