using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DBAccess
{
    public interface IDatabase
    {
        IDbConnection CreateConnection();

        IDbConnection CreateConnection(string sConnectString);
    }

    public class SqlServer : IDatabase
    {
        public IDbConnection CreateConnection()
        {
            string sConnectString = DBAccess.ConfigTools.GetConnection();
            return CreateConnection(sConnectString);
        }

        public IDbConnection CreateConnection(string sConnectString)
        {
            if (string.IsNullOrEmpty(sConnectString))
            {
                throw new NotImplementedException("数据库地址为空");
            }

            IDbConnection conn = new SqlConnection(sConnectString);
            conn.Open();
            return conn;
        }
    }

    public class MySql : IDatabase
    {
        public IDbConnection CreateConnection(string sConnectString)
        {
            throw new NotImplementedException();
        }

        public IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }

    public class Oracle : IDatabase
    {
        public IDbConnection CreateConnection(string sConnectString)
        {
            throw new NotImplementedException();
        }

        public IDbConnection CreateConnection()
        {
            throw new NotImplementedException();
        }
    }

}
