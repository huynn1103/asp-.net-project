using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DataLayers
{
    /// <summary>
    /// Định nghĩa các chức năng xử lý dữ liệu liên quan đến phân loại tin
    /// </summary>
    public interface IPostCategoryDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (nếu pageSize = 0 thì không phân trang)</param>
        /// <param name="searchValue">Tên loại tin cần tìm (chuỗi rỗng nếu lấy toàn bộ/không tìm kiếm)</param>
        /// <returns></returns>
        IList<PostCategory> List(int page = 1, int pageSize = 20, string searchValue = "");
        /// <summary>
        /// Đếm số lượng phân loại tin thoải điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue">Tên loại tin cần tìm (chuỗi rỗng nếu lấy toàn bộ/không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Lấy 1 phân loại tin theo categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        PostCategory Get(int categoryId);
        /// <summary>
        /// Lấy 1 phân loại tin theo tên hiển thị trên URL
        /// </summary>
        /// <param name="categoryUrlName"></param>
        /// <returns></returns>
        PostCategory Get(string categoryUrlName);
        /// <summary>
        /// Bổ sung 1 phân loại tin mới. Hàm trả về id của phân loại tin được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(PostCategory data);
        /// <summary>
        /// Cập nhật phân loại tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(PostCategory data);
        /// <summary>
        /// Xóa phân loại tin
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool Delete(int categoryId);
        /// <summary>
        /// Kiểm tra xem 1 phân loại tin hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        bool InUsed(int categoryId);
        /// <summary>
        /// Kiểm tra xem URL name có bị trùng hay không
        /// </summary>
        /// <param name="categoryUrlName">Giá trị cần kiểm tra</param>
        /// <param name="categoryId">
        /// Nếu bằng 0: kiểm tra khi bổ sung. 
        /// Ngược lại: kiểm tra khi update phân loại tin có mã là categoryId
        /// </param>
        /// <returns></returns>
        bool ExistsUrlName(string categoryUrlName, int categoryId = 0);
        /// <summary>
        /// Thay đổi thứ tự hiển thị của phân loại tin
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="moveUp">true: di chuyển lên trên, false: di chuyển xuống dưới</param>
        void ChangeDisplayOrder(int categoryId, bool moveUp);
    }
}