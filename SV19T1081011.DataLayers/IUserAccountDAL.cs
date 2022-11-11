using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến tài khoản người dùng
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Danh sách tài khoản (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        IList<UserAccount> List(int page, int pageSize, string searchValue = "");
        /// <summary>
        /// Đếm số lượng tài khoản theo điều kiện tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Lấy thông tin tài khoản theo id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserAccount Get(long userId);
        /// <summary>
        /// Lấy thông tin tài khoản theo userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        UserAccount Get(string userName);
        /// <summary>
        /// Xác thực thông tin tài khoản theo userName và password
        /// (trả về thông tin tài khoản nếu xác thực thành công, ngược lại trả về null)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Get(string userName, string password);
        /// <summary>
        /// Thay đổi mật khẩu của tài khoản
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string password);
        /// <summary>
        /// Bổ sung tài khoản (hàm trả về id của tài khoản được bổ sung nếu thành công, ngược lại trả về giá trị nhỏ hơn hoặc bằng 0 nếu không
        /// bổ sung được tài khoản mới)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Add(UserAccount data);
        /// <summary>
        /// Cập nhật tên, email và phone của tài khoản dựa vào userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        bool Update(long userId, string firstName, string lastName, string email, string phone);
        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Delete(long userId);
        /// <summary>
        /// Kiểm tra xem userName đã tồn tại hay chưa (true: đã tồn tại, false: chưa tồn tại)
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool ExistsUserName(string userName);
    }
}
