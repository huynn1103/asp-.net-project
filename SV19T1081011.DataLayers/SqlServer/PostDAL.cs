using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV19T1081011.DomainModels;
using System.Data.SqlClient;
using System.Data;

namespace SV19T1081011.DataLayers.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class PostDAL : _BaseDAL, IPostDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public PostDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbReader"></param>
        /// <returns></returns>
        private Post CreatePostFromDbReader(SqlDataReader dbReader)
        {
            return new Post()
            {
                PostId = Convert.ToInt64(dbReader["PostId"]),
                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                Title = Convert.ToString(dbReader["Title"]),
                BriefContent = Convert.ToString(dbReader["BriefContent"]),
                FullContent = Convert.ToString(dbReader["FullContent"]),
                UrlTitle = Convert.ToString(dbReader["UrlTitle"]),
                Image = Convert.ToString(dbReader["Image"]),
                AllowComment = Convert.ToBoolean(dbReader["AllowComment"]),
                IsHidden = Convert.ToBoolean(dbReader["IsHidden"]),
                UserId = Convert.ToInt64(dbReader["UserId"]),
                CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                Creator = new Author()
                {
                    UserId = Convert.ToInt64(dbReader["UserId"]),
                    UserName = Convert.ToString(dbReader["UserName"]),
                    FirstName = Convert.ToString(dbReader["FirstName"]),
                    LastName = Convert.ToString(dbReader["LastName"]),
                    Email = Convert.ToString(dbReader["Email"]),
                    Phone = Convert.ToString(dbReader["Phone"])                    
                },
                Category = new PostCategory()
                {
                    CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                    CategoryName = Convert.ToString(dbReader["CategoryName"]),
                    CategoryUrlName = Convert.ToString(dbReader["CategoryUrlName"]),
                    CategoryDescriptions = Convert.ToString(dbReader["CategoryDescriptions"]),
                    DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int Count(string searchValue = "", int categoryId = 0)
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            int result = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	COUNT(*)
                                        FROM	Post as p
                                        WHERE	((@SearchValue = N'') OR (p.Title LIKE @SearchValue))
	                                        AND	((@CategoryId = 0) OR (p.CategoryId = @CategoryId))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlTitle"></param>
        /// <returns></returns>
        public Post Get(string urlTitle)
        {
            Post data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.*,
                                               u.UserName, u.FirstName, u.LastName, u.Email, u.Phone,
                                               c.CategoryName, c.CategoryUrlName, c.CategoryDescriptions, c.DisplayOrder 
                                        FROM Post as p
                                             LEFT JOIN UserAccount as u ON p.UserId = u.UserId
                                             LEFT JOIN PostCategory as c ON p.CategoryId = c.CategoryId     
                                        WHERE p.UrlTitle = @UrlTitle";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UrlTitle", urlTitle);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                            data = CreatePostFromDbReader(dbReader);
                        dbReader.Close();
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Post Get(long postId)
        {
            Post data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.*,
                                               u.UserName, u.FirstName, u.LastName, u.Email, u.Phone,
                                               c.CategoryName, c.CategoryUrlName, c.CategoryDescriptions, c.DisplayOrder 
                                        FROM Post as p
                                             LEFT JOIN UserAccount as u ON p.UserId = u.UserId
                                             LEFT JOIN PostCategory as c ON p.CategoryId = c.CategoryId
                                        WHERE p.PostId = @PostId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                            data = CreatePostFromDbReader(dbReader);
                        dbReader.Close();
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IList<Post> List(int page = 1, int pageSize = 20, string searchValue = "", int categoryId = 0)
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            List<Post> data = new List<Post>();
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  p.*,
                                                u.UserName, u.FirstName, u.LastName, u.Email, u.Phone,
                                                c.CategoryName, c.CategoryUrlName, c.CategoryDescriptions, c.DisplayOrder
                                        FROM
	                                        (
		                                        SELECT	p.PostId, p.CreatedTime, p.Title, p.BriefContent, N'' as FullContent,
				                                        p.UrlTitle, p.Image, p.AllowComment, p.IsHidden, p.UserId, p.CategoryId,
				                                        ROW_NUMBER() OVER (ORDER BY p.PostId DESC) AS RowNumber
		                                        FROM	Post as p
		                                        WHERE	((@SearchValue = N'') OR (p.Title LIKE @SearchValue))
			                                        AND	((@CategoryId = 0) OR (p.CategoryId = @CategoryId))
	                                        ) as p
                                            LEFT JOIN UserAccount as u ON p.UserId = u.UserId
                                            LEFT JOIN PostCategory as c ON p.CategoryId = c.CategoryId
                                        WHERE p.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize 
                                        ORDER BY p.RowNumber";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue ?? "");
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(CreatePostFromDbReader(dbReader));
                        }
                        dbReader.Close();
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long Add(Post data)
        {
            long postId = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Post(CreatedTime, Title, BriefContent, FullContent, UrlTitle, Image, AllowComment, IsHidden, UserId, CategoryId)
                                        VALUES(@CreatedTime, @Title, @BriefContent, @FullContent, @UrlTitle, @Image, @AllowComment, @IsHidden, @UserId, @CategoryId);
                                        SELECT SCOPE_IDENTITY()";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CreatedTime", data.CreatedTime);
                    cmd.Parameters.AddWithValue("@Title", data.Title ?? "");
                    cmd.Parameters.AddWithValue("@BriefContent", data.BriefContent ?? "");
                    cmd.Parameters.AddWithValue("@FullContent", data.FullContent ?? "");
                    cmd.Parameters.AddWithValue("@UrlTitle", data.UrlTitle ?? "");
                    cmd.Parameters.AddWithValue("@Image", data.Image ?? "");
                    cmd.Parameters.AddWithValue("@AllowComment", data.AllowComment);                    
                    cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                    cmd.Parameters.AddWithValue("@UserId", data.UserId);
                    cmd.Parameters.AddWithValue("@CategoryId", data.CategoryId);

                    postId = Convert.ToInt64(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return postId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Post data)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE  Post
                                        SET     Title = @Title, BriefContent = @BriefContent, FullContent = @FullContent,
                                                UrlTitle = @UrlTitle, Image = @Image, 
                                                AllowComment = @AllowComment, IsHidden = @IsHidden,
                                                CategoryId = @CategoryId
                                        WHERE   PostId = @PostId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PostId", data.PostId);
                    cmd.Parameters.AddWithValue("@Title", data.Title ?? "");
                    cmd.Parameters.AddWithValue("@BriefContent", data.BriefContent ?? "");
                    cmd.Parameters.AddWithValue("@FullContent", data.FullContent ?? "");
                    cmd.Parameters.AddWithValue("@UrlTitle", data.UrlTitle ?? "");
                    cmd.Parameters.AddWithValue("@Image", data.Image ?? "");
                    cmd.Parameters.AddWithValue("@AllowComment", data.AllowComment);
                    cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                    cmd.Parameters.AddWithValue("@CategoryId", data.CategoryId);

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public bool UpdateImage(long postId, string image)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE  Post
                                        SET     Image = @Image
                                        WHERE   PostId = @PostId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PostId", postId);                    
                    cmd.Parameters.AddWithValue("@Image", image);

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool Delete(long postId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Post WHERE PostId = @PostId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PostId", postId);                    

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
    }
}
