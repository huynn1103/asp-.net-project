using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến bài viết
    /// </summary>
    public interface IPostCommentDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "", int postId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        int Count(string searchValue = "", int postId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        PostComment Get(long commentId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Add(PostComment data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        bool Update(PostComment data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        bool Delete(long commentId);
    }
}
