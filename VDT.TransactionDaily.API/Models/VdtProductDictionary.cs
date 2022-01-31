using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VDT.TransactionDaily.API.Models
{
    /// <summary>
    /// Bảng chứa danh mục sản phẩm
    /// </summary>
    public partial class VdtProductDictionary : BaseModel
    {
        public VdtProductDictionary()
        {
            VdtProducts = new HashSet<VdtProduct>();
        }

        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public override uint Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// Mã danh mục
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string? Name { get; set; }

        public virtual VdtUser User { get; set; } = null!;

        public virtual ICollection<VdtProduct> VdtProducts { get; set; }
    }
}
