using System;
using System.Collections.Generic;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng lưu trữ phiên đăng nhập người dùng
    /// </summary>
    public partial class VdtUserSession
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public DateTime LoginDay { get; set; }
        public Guid DeviceID { get; set; }
        public string? OS { get; set; }
        public string? Browser { get; set; }
        public string? ClientIp { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }

        public virtual VdtUser User { get; set; } = null!;
    }
}
