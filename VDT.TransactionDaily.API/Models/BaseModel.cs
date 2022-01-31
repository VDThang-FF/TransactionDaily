using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using VDT.TransactionDaily.API.Helper;
using static VDT.TransactionDaily.API.Models.Enums.Enumarations;

namespace VDT.TransactionDaily.API.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [NotMapped]
        public virtual uint Id { get; set; }

        /// <summary>
        /// ID người dùng
        /// </summary>
        public virtual uint UserId { get; set; }

        /// <summary>
        /// State model
        /// </summary>
        [NotMapped]
        public ModelState ModelState { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public virtual DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public virtual string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public virtual string? ModifiedBy { get; set; }

        /// <summary>
        /// Lấy loại khóa chính
        /// </summary>
        /// <returns></returns>
        public Type GetPrimaryKeyType()
        {
            return this.GetFieldType(typeof(KeyAttribute));
        }

        /// <summary>
        /// Lấy giá trị khóa chính
        /// </summary>
        /// <returns></returns>
        public object GetPrimaryKeyValue()
        {
            return this.GetFieldValue(typeof(KeyAttribute));
        }

        /// <summary>
        /// Lấy tên khóa chính
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        public string GetPrimaryKeyName()
        {
            return this.GetFieldName(typeof(KeyAttribute));
        }

        /// <summary>
        /// Gán giá trị khóa chính
        /// </summary>
        /// <param name="value"></param>
        /// created by vdthang 24.01.2022
        public void SetPrimaryKey(object value)
        {
            this.SetValue(GetPrimaryKeyName(), value);
        }

        /// <summary>
        /// Lấy khóa chính
        /// </summary>
        /// <returns></returns>
        public PropertyInfo GetPrimaryKey()
        {
            return this.GetType().GetField(typeof(KeyAttribute));
        }
    }
}
