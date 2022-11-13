using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1081011.DomainModels;
using SV19T1081011.DataLayers;
using System.Configuration;

namespace SV19T1081011.BusinessLayers
{
    /// <summary>
    /// Lớp cung cấp các chức năng tác nghiệp liên quan đến bài viết,...
    /// </summary>
    public static class ContentService
    {
        private static readonly IPostCategoryDAL postCategoryDB;
        private static readonly IPostDAL postDB;
        private static readonly IPostCommentDAL postCommentDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static ContentService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            postCategoryDB = new DataLayers.SqlServer.PostCategoryDAL(connectionString);
            postDB = new DataLayers.SqlServer.PostDAL(connectionString);
            postCommentDB = new DataLayers.SqlServer.PostCommentDAL(connectionString);
        }

        #region Post Category

        /// <summary>
        /// Bổ sung category mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// </returns>
        public static int AddCategory(PostCategory data)
        {
            if (postCategoryDB.ExistsUrlName(data.CategoryUrlName, 0))
                return -1;
            return postCategoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(PostCategory data)
        {
            if (postCategoryDB.ExistsUrlName(data.CategoryUrlName, data.CategoryId))
                return false;
            return postCategoryDB.Update(data);
        }
        /// <summary>
        /// Xóa category 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryId)
        {
            if (postCategoryDB.InUsed(categoryId))
                return false;
            return postCategoryDB.Delete(categoryId);
        }
        /// <summary>
        /// Xóa nhiều category
        /// </summary>
        /// <param name="categoryIds"></param>
        public static void DeleteCategories(int[] categoryIds)
        {
            foreach (int categoryId in categoryIds)
                if (!postCategoryDB.InUsed(categoryId))
                    postCategoryDB.Delete(categoryId);                
        }
        /// <summary>
        /// Thay đổi thứ tự hiển thị của category
        /// </summary>
        /// <param name="categoryId">Id</param>
        /// <param name="moveUp">true: chuyển lên, false: đẩy xuống</param>
        public static void ChangeCategoryOrder(int categoryId, bool moveUp)
        {
            postCategoryDB.ChangeDisplayOrder(categoryId, moveUp);
        }
        /// <summary>
        /// Lấy category theo id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static PostCategory GetCategory(int categoryId)
        {
            return postCategoryDB.Get(categoryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryUrlName"></param>
        /// <returns></returns>
        public static PostCategory GetCategory(string categoryUrlName)
        {
            return postCategoryDB.Get(categoryUrlName);
        }
        /// <summary>
        /// Tìm kiếm, hiển thị category dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<PostCategory> ListCategories(int page, 
                                                        int pageSize, 
                                                        string searchValue, 
                                                        out int rowCount)
        {
            rowCount = postCategoryDB.Count(searchValue);
            return postCategoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy toàn bộ Category
        /// </summary>
        /// <returns></returns>
        public static List<PostCategory> ListCategories()
        {
            return postCategoryDB.List(1, 0, "").ToList();
        }
        /// <summary>
        /// Kiểm tra xem category hiện đang có dữ liệu liên quan không
        /// </summary>
        /// <param name="categoryId"></param>
        public static bool InUsedCategory(int categoryId)
        {
            return postCategoryDB.InUsed(categoryId);
        }
        /// <summary>
        /// Kiểm tra urlName bị trùng hay không
        /// </summary>
        /// <param name="urlName"></param>
        /// <param name="categoryId">0: kiểm tra cho bổ sung, khác 0: kiểm tra cho cập nhật</param>
        /// <returns></returns>
        public static bool ExistsCategoryUrlName(string urlName, int categoryId)
        {
            return postCategoryDB.ExistsUrlName(urlName, categoryId);
        }

        #endregion

        #region Post

        /// <summary>
        /// Tìm kiếm, hiển thị danh sách bài viết dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<Post> ListPosts(int page, int pageSize, string searchValue, int categoryId, out int rowCount)
        {
            rowCount = postDB.Count(searchValue, categoryId);
            return postDB.List(page, pageSize, searchValue, categoryId).ToList();
        }
        /// <summary>
        /// Lấy toàn bộ bài viết
        /// </summary>
        /// <returns></returns>
        public static List<Post> ListPosts()
        {
            return postDB.List(1, 0, "").ToList();
        }
        /// <summary>
        /// Lấy bài viết theo id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static Post GetPost(long postId)
        {
            return postDB.Get(postId);
        }
        /// <summary>
        /// Lấy bài viết theo UrlTitle
        /// </summary>
        /// <param name="urlTitle"></param>
        /// <returns></returns>
        public static Post GetPost(string urlTitle)
        {
            return postDB.Get(urlTitle);
        }
        /// <summary>
        /// Bổ sung bài viết
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddPost(Post data)
        {
            return postDB.Add(data);
        }
        /// <summary>
        /// Cập nhật bài viết
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdatePost(Post data)
        {
            return postDB.Update(data);
        }
        /// <summary>
        /// Cập nhật ảnh minh họa của bài viết
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool UpdatePostImage(long postId, string image)
        {
            return postDB.UpdateImage(postId, image);
        }
        /// <summary>
        /// Xóa bài viết
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static bool DeletePost(long postId)
        {
            return postDB.Delete(postId);
        }
        #endregion

        #region Post Comment
        /// <summary>
        /// Tìm kiếm, hiển thị danh sách comment dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<PostComment> ListComments(int page, int pageSize, string searchValue, long postId, out int rowCount)
        {
            rowCount = postCommentDB.Count(searchValue, postId);
            return postCommentDB.List(page, pageSize, searchValue, postId).ToList();
        }
        /// <summary>
        /// Lấy comment theo id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static PostComment GetComment(long commentId)
        {
            return postCommentDB.Get(commentId);
        }
        /// <summary>
        /// Bổ sung comment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddComment(PostComment data)
        {
            return postCommentDB.Add(data);
        }
        /// <summary>
        /// Cập nhật comment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateComment(PostComment data)
        {
            return postCommentDB.Update(data);
        }
        /// <summary>
        /// Xóa comment
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public static bool DeleteComment(long commentId)
        {
            return postCommentDB.Delete(commentId);
        }
        #endregion
    }
}
