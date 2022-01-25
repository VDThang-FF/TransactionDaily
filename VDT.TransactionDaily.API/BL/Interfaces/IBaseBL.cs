using VDT.TransactionDaily.API.Models;
using VDT.TransactionDaily.API.Models.Responses;

namespace VDT.TransactionDaily.API.BL.Interfaces
{
    public interface IBaseBL<T> where T : BaseModel
    {
        /// <summary>
        /// Lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// created by vdthang 24.01.2022
        PagingResponse GetPaging(int pageIndex, int pageSize, string query);

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        T GetByID(uint id);

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        T GetByID(Guid id);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        ServiceResponse Insert(T entity);

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        ServiceResponse Update(T entity);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// created by vdthang 20.01.2022
        ServiceResponse Delete(List<string> ids);
    }
}
