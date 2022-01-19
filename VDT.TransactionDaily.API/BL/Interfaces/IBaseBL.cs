using VDT.TransactionDaily.API.Models;

namespace VDT.TransactionDaily.API.BL.Interfaces
{
    public interface IBaseBL<T> where T : BaseModel
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Insert(T entity);

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);
        
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(List<int> id);
    }
}
