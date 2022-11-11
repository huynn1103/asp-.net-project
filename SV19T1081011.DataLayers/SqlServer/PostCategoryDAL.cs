using SV19T1081011.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV19T1081011.DataLayers.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class PostCategoryDAL : _BaseDAL, IPostCategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public PostCategoryDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(PostCategory data)
        {
            int result = 0;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DECLARE @DisplayOrder int;
                                        SELECT @DisplayOrder = ISNULL(MAX(DisplayOrder), 0) + 1 FROM PostCategory;

                                        INSERT INTO PostCategory(CategoryName, CategoryUrlName, CategoryDescriptions, DisplayOrder)
                                        VALUES (@CategoryName, @CategoryUrlName, @CategoryDescriptions, @DisplayOrder);

                                        SELECT SCOPE_IDENTITY();";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryUrlName", data.CategoryUrlName);
                    cmd.Parameters.AddWithValue("@CategoryDescriptions", data.CategoryDescriptions);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="moveUp"></param>
        public void ChangeDisplayOrder(int categoryId, bool moveUp)
        {
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"declare @currentDisplayOrder int,
		                                @newDisplayOrder int,
		                                @relatedCategoryId int;

                                        select	@currentDisplayOrder = DisplayOrder from PostCategory where CategoryId = @categoryId;
                                        if (@moveUp = 1)
	                                        begin
		                                        select	top 1 @relatedCategoryId = CategoryId,	@newDisplayOrder = DisplayOrder 
		                                        from	PostCategory
		                                        where	DisplayOrder < @currentDisplayOrder and CategoryId != @categoryId
		                                        order by DisplayOrder desc
	                                        end
                                        else
	                                        begin
		                                        select	top 1 @relatedCategoryId = CategoryId,	@newDisplayOrder = DisplayOrder 
		                                        from	PostCategory
		                                        where	DisplayOrder > @currentDisplayOrder and CategoryId != @categoryId
		                                        order by DisplayOrder
	                                        end
                                        if (@relatedCategoryId is not null)
	                                        begin
		                                        update	PostCategory set DisplayOrder = @newDisplayOrder where CategoryId = @categoryId;
		                                        update	PostCategory set DisplayOrder = @currentDisplayOrder where CategoryId = @relatedCategoryId;
	                                        end";
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@moveUp", moveUp);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            int count = 0;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) 
                                        FROM PostCategory 
                                        WHERE (@searchValue = '') OR (CategoryName LIKE @searchValue)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool Delete(int categoryId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostCategory WHERE CategoryId = @categoryId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryUrlName"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool ExistsUrlName(string categoryUrlName, int categoryId = 0)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	COUNT(*)
                                        FROM	PostCategory
                                        WHERE	((@CategoryId = 0) AND (CategoryUrlName = @CategoryUrlName))
	                                        OR	((@CategoryId != 0) AND (CategoryUrlName = @CategoryUrlName) AND (CategoryId != @CategoryId))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryUrlName", categoryUrlName ?? "");
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public PostCategory Get(int categoryId)
        {
            PostCategory data = null;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM PostCategory WHERE CategoryId = @CategoryId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PostCategory()
                            {
                                CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                CategoryUrlName = Convert.ToString(dbReader["CategoryUrlName"]),
                                CategoryDescriptions = Convert.ToString(dbReader["CategoryDescriptions"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryUrlName"></param>
        /// <returns></returns>
        public PostCategory Get(string categoryUrlName)
        {
            PostCategory data = null;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM PostCategory WHERE CategoryUrlName = @CategoryUrlName";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryUrlName", categoryUrlName);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbReader.Read())
                        {
                            data = new PostCategory()
                            {
                                CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                CategoryUrlName = Convert.ToString(dbReader["CategoryUrlName"]),
                                CategoryDescriptions = Convert.ToString(dbReader["CategoryDescriptions"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                            };
                        }
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool InUsed(int categoryId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT	CASE 
			                                WHEN EXISTS(SELECT * FROM Post WHERE CategoryId = @CategoryId) THEN 1
			                                ELSE 0
		                                END AS InUsed";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    result = Convert.ToBoolean(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<PostCategory> List(int page = 1, int pageSize = 20, string searchValue = "")
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            List<PostCategory> data = new List<PostCategory>();
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT *
                                        FROM (
		                                        SELECT	*, ROW_NUMBER() OVER(ORDER BY DisplayOrder) AS RowNumber
		                                        FROM	PostCategory
		                                        WHERE	(@searchValue = N'') OR (CategoryName LIKE @searchValue)
	                                        ) AS c
                                        WHERE (@pageSize = 0)
                                           OR (c.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)
                                        ORDER BY c.RowNumber;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new PostCategory()
                            {
                                CategoryId = Convert.ToInt32(dbReader["CategoryId"]),
                                CategoryName = Convert.ToString(dbReader["CategoryName"]),
                                CategoryUrlName = Convert.ToString(dbReader["CategoryUrlName"]),
                                CategoryDescriptions = Convert.ToString(dbReader["CategoryDescriptions"]),
                                DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                            });
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
        public bool Update(PostCategory data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE PostCategory 
                                        SET CategoryName = @CategoryName, CategoryUrlName = @CategoryUrlName, CategoryDescriptions = @CategoryDescriptions 
                                        WHERE CategoryId = @CategoryId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@CategoryId", data.CategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryUrlName", data.CategoryUrlName);
                    cmd.Parameters.AddWithValue("@CategoryDescriptions", data.CategoryDescriptions);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
    }
}
