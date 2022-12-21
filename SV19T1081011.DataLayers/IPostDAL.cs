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
    public interface IPostDAL
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IList<Post> MostRecentList();
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IList<Post> TrendingList();
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IList<Post> FeaturedList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        IList<Post> List(int page = 1, int pageSize = 20, string searchValue = "", int categoryId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        int Count(string searchValue = "", int categoryId = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Post Get(long postId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlTitle"></param>
        /// <returns></returns>
        Post Get(string urlTitle);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long Add(Post data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        bool Update(Post data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="image"></param>
        bool UpdateImage(long postId, string image);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        bool Delete(long postId);
    }
}
