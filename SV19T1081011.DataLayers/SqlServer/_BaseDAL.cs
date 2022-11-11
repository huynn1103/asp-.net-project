using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SV19T1081011.DataLayers.SqlServer
{
    /// <summary>
    /// Lớp cơ sở cho các lớp giao tiếp với CSDL SQL Server
    /// </summary>
    public abstract class _BaseDAL
    {
        protected string _connectionString;
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString">Chuỗi tham số kết nối đến CSDL</param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tạo và mở kết nối đến CSDL
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
