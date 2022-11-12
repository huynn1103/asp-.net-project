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
    public class PostCommentDAL : _BaseDAL, IPostCommentDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public PostCommentDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbReader"></param>
        /// <returns></returns>
        private PostComment CreatePostCommentFromDbReader(SqlDataReader dbReader)
        {
            return new PostComment()
            {
                CommentId = Convert.ToInt64(dbReader["CommentId"]),
                CreatedTime = Convert.ToDateTime(dbReader["CreatedTime"]),
                CommentContent = Convert.ToString(dbReader["CommentContent"]),
                IsAccepted = Convert.ToBoolean(dbReader["IsAccepted"]),
                UserId = Convert.ToInt64(dbReader["UserId"]),
                PostId = Convert.ToInt64(dbReader["PostId"]),
                Creator = new Author()
                {
                    UserId = Convert.ToInt64(dbReader["UserId"]),
                    UserName = Convert.ToString(dbReader["UserName"]),
                    FirstName = Convert.ToString(dbReader["FirstName"]),
                    LastName = Convert.ToString(dbReader["LastName"]),
                    Email = Convert.ToString(dbReader["Email"]),
                    Phone = Convert.ToString(dbReader["Phone"])                    
                },
                Post = new Post()
                {
                    PostId = Convert.ToInt64(dbReader["PostId"]),
                    CreatedTime = Convert.ToDateTime(dbReader["PostCreatedTime"]),
                    Title = Convert.ToString(dbReader["Title"]),
                    BriefContent = Convert.ToString(dbReader["BriefContent"]),
                    UrlTitle = Convert.ToString(dbReader["UrlTitle"])
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int Count(string searchValue = "", int postId = 0)
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            int result = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	COUNT(*)
                                        FROM	PostComment as p
                                        WHERE	((@SearchValue = N'') OR (p.CommentContent LIKE @SearchValue))
	                                        AND	((@PostId = 0) OR (p.PostId = @PostId))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    result = Convert.ToInt32(cmd.ExecuteScalar());
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
        public PostComment Get(long commentId)
        {
            PostComment data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.*,
                                               u.UserName, u.FirstName, u.LastName, u.Email, u.Phone,
                                               p.CreatedTime AS 'PostCreatedTime', p.Title, p.BriefContent, p.UrlTitle
                                        FROM PostComment as c
                                             LEFT JOIN UserAccount as u ON c.UserId = u.UserId
                                             LEFT JOIN Post as p ON c.PostId = p.PostId
                                        WHERE c.CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", commentId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                            data = CreatePostCommentFromDbReader(dbReader);
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
        public IList<PostComment> List(int page = 1, int pageSize = 20, string searchValue = "", int postId = 0)
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            List<PostComment> data = new List<PostComment>();
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  c.*,
                                                u.UserName, u.FirstName, u.LastName, u.Email, u.Phone,
                                                p.CreatedTime AS 'PostCreatedTime', p.Title, p.BriefContent, p.UrlTitle
                                        FROM
	                                        (
		                                        SELECT	c.CommentId, c.CreatedTime, c.CommentContent, c.IsAccepted, c.UserId, c.PostId
				                                        ROW_NUMBER() OVER (ORDER BY c.@CommentId DESC) AS RowNumber
		                                        FROM	PostComment as c
		                                        WHERE	((@SearchValue = N'') OR (c.CommentContent LIKE @SearchValue))
			                                        AND	((@PostId = 0) OR (p.PostId = @PostId))
	                                        ) as c
                                            LEFT JOIN UserAccount as u ON c.UserId = u.UserId
                                            LEFT JOIN Post as p ON c.PostId = p.PostId
                                        WHERE c.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize 
                                        ORDER BY c.RowNumber";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue ?? "");
                    cmd.Parameters.AddWithValue("@PostId", postId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(CreatePostCommentFromDbReader(dbReader));
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
        public long Add(PostComment data)
        {
            long postCommentId = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostComment(CreatedTime, CommentContent, IsAccepted, UserId, PostId)
                                        VALUES(@CreatedTime, @CommentContent, @IsAccepted, @UserId, @PostId);
                                        SELECT SCOPE_IDENTITY()";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CreatedTime", data.CreatedTime);
                    cmd.Parameters.AddWithValue("@CommentContent", data.CommentContent ?? "");                 
                    cmd.Parameters.AddWithValue("@IsAccepted", data.IsAccepted);
                    cmd.Parameters.AddWithValue("@UserId", data.UserId);
                    cmd.Parameters.AddWithValue("@PostId", data.PostId);

                    postCommentId = Convert.ToInt64(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return postCommentId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(PostComment data)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE  PostComment
                                        SET     CommentContent = @CommentContent, IsAccepted = @IsAccepted
                                        WHERE   CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", data.CommentId);
                    cmd.Parameters.AddWithValue("@CommentContent", data.CommentContent ?? "");
                    cmd.Parameters.AddWithValue("@IsAccepted", data.IsAccepted);

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
        public bool Delete(long commentId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostComment WHERE CommentId = @CommentId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CommentId", commentId);                    

                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
    }
}
