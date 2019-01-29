using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace DBAccess
{
   public class DbFactory
    {
        //通过工厂方式获取对应的数据库对象
        public static IDatabase Create(string sCacheKey)
        {
            object obj = Activator.CreateInstance(Type.GetType(sCacheKey));
            return (IDatabase)obj;
        }

        private static string _dataprovider = "DBAccess.SqlServer";
        /// <summary>
        /// 得到推广数据库连接信息
        /// </summary>
        public static string GetDataProvider()
        {
            if (_dataprovider == "")
            {
                _dataprovider = ConfigurationManager.AppSettings["DataProvider"];  //从哪里取的数据
            }
            return _dataprovider;
        }
    }
}
