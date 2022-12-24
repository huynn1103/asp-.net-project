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
    public class UserAccountDAL : _BaseDAL, IUserAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public UserAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long Add(UserAccount data)
        {
            long result = 0;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserAccount(UserName, FirstName, LastName, Email, Phone, Password, GroupName, RegisteredTime, IsLocked)
                                        VALUES (@UserName, @FirstName, @LastName, @Email, @Phone, @Password, @GroupName, @RegisteredTime, @IsLocked);
                                        SELECT SCOPE_IDENTITY();";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", data.UserName);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                    cmd.Parameters.AddWithValue("@Phone", data.Phone);
                    cmd.Parameters.AddWithValue("@Password", data.Password);
                    cmd.Parameters.AddWithValue("@GroupName", data.GroupName);
                    cmd.Parameters.AddWithValue("@RegisteredTime", data.RegisteredTime);
                    cmd.Parameters.AddWithValue("@IsLocked", data.IsLocked);

                    result = Convert.ToInt64(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChangePassword(string userName, string password)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserAccount SET Password=@Password WHERE UserName=@UserName";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
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
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) FROM UserAccount
                                        WHERE (@SearchValue = '') 
                                           OR ((UserName LIKE @SearchValue) OR (FirstName + ' ' + LastName LIKE @SearchValue) OR (Email LIKE @SearchValue) OR (Phone LIKE @SearchValue))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Delete(long userId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserAccount WHERE UserId = @UserId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUserName(string userName)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) FROM UserAccount WHERE UserName = @UserName";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    result = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserAccount Get(long userId)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM UserAccount WHERE UserId = @UserId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbreader.Read())
                        {
                            data = new UserAccount()
                            {
                                UserId = Convert.ToInt64(dbreader["UserId"]),
                                UserName = Convert.ToString(dbreader["UserName"]),
                                FirstName = Convert.ToString(dbreader["FirstName"]),
                                LastName = Convert.ToString(dbreader["LastName"]),
                                Email = Convert.ToString(dbreader["Email"]),
                                Phone = Convert.ToString(dbreader["Phone"]),
                                Password = "*", //Do not show user's password
                                GroupName = Convert.ToString(dbreader["GroupName"]),
                                RegisteredTime = Convert.ToDateTime(dbreader["RegisteredTime"]),
                                IsLocked = Convert.ToBoolean(dbreader["IsLocked"])
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
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserAccount Get(string userName)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM UserAccount WHERE UserName = @UserName";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbreader.Read())
                        {
                            data = new UserAccount()
                            {
                                UserId = Convert.ToInt64(dbreader["UserId"]),
                                UserName = Convert.ToString(dbreader["UserName"]),
                                FirstName = Convert.ToString(dbreader["FirstName"]),
                                LastName = Convert.ToString(dbreader["LastName"]),
                                Email = Convert.ToString(dbreader["Email"]),
                                Phone = Convert.ToString(dbreader["Phone"]),
                                Password = "*", //Do not show user's password
                                GroupName = Convert.ToString(dbreader["GroupName"]),
                                RegisteredTime = Convert.ToDateTime(dbreader["RegisteredTime"]),
                                IsLocked = Convert.ToBoolean(dbreader["IsLocked"])
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
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Get(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM UserAccount WHERE UserName = @UserName AND Password = @Password";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (SqlDataReader dbreader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (dbreader.Read())
                        {
                            data = new UserAccount()
                            {
                                UserId = Convert.ToInt64(dbreader["UserId"]),
                                UserName = Convert.ToString(dbreader["UserName"]),
                                FirstName = Convert.ToString(dbreader["FirstName"]),
                                LastName = Convert.ToString(dbreader["LastName"]),
                                Email = Convert.ToString(dbreader["Email"]),
                                Phone = Convert.ToString(dbreader["Phone"]),
                                Password = "*", //Do not show user's password
                                GroupName = Convert.ToString(dbreader["GroupName"]),
                                RegisteredTime = Convert.ToDateTime(dbreader["RegisteredTime"]),
                                IsLocked = Convert.ToBoolean(dbreader["IsLocked"])
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
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<UserAccount> List(int page, int pageSize, string searchValue = "")
        {
            if (searchValue != "")
                searchValue = $"%{searchValue}%";

            List<UserAccount> data = new List<UserAccount>();
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  * 
                                        FROM    UserAccount
                                        WHERE   (@SearchValue = N'')
                                            OR  ((UserName LIKE @SearchValue) OR (FirstName + ' ' + LastName LIKE @SearchValue) OR (Email LIKE @SearchValue) OR (Phone LIKE @SearchValue))
                                        ORDER BY UserId
                                        OFFSET (@Page -1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                    using (SqlDataReader dbreader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbreader.Read())
                        {
                            data.Add(new UserAccount()
                            {
                                UserId = Convert.ToInt64(dbreader["UserId"]),
                                UserName = Convert.ToString(dbreader["UserName"]),
                                FirstName = Convert.ToString(dbreader["FirstName"]),
                                LastName = Convert.ToString(dbreader["LastName"]),
                                Email = Convert.ToString(dbreader["Email"]),
                                Phone = Convert.ToString(dbreader["Phone"]),
                                Password = "*", //Do not show user's password
                                GroupName = Convert.ToString(dbreader["GroupName"]),
                                RegisteredTime = Convert.ToDateTime(dbreader["RegisteredTime"]),
                                IsLocked = Convert.ToBoolean(dbreader["IsLocked"])
                            });
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
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool Update(long userId, string firstName, string lastName, string email, string phone)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserAccount SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone WHERE UserId = @UserId";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    result = cmd.ExecuteNonQuery() > 0;
                }
                connection.Close();
            }
            return result;
        }
    }
}
