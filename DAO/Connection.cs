using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    class Connection
    {
        private SqlConnection conn;
        private static Connection instance = new Connection();
        private Connection()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(local);Initial Catalog=sale; User Id = sa; Password=sa";
        }

        public static Connection getInstance()
        {
            return instance;
        }

        public SqlConnection getSqlConnect()
        {
            if (conn != null)
            {
                return conn;
            }
            return null;

        }

        public bool isOpen()
        {
            return conn != null && conn.State == ConnectionState.Open;
        }


    }
}
