using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DBAccess
{
    public class RepositoryBase<T> : IRepositoryBase<T>
    {

        protected DBAccess.IDatabase _db = DBAccess.DbFactory.Create(DBAccess.DbFactory.GetDataProvider());

        public async Task<int> Delete(int Id, string deleteSql)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                return await conn.ExecuteAsync(deleteSql, new { Id = Id });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="detailSql">SELECT Id, UserName, ... FROM dbo.Users WHERE Id=@Id</param>
        /// <returns></returns>
        public async Task<T> Detail(int Id, string detailSql)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(detailSql, new { Id = Id });
            }
        }

        public async Task<List<T>> ExecQuerySP(string SPName)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                return await Task.Run(() => conn.Query<T>(SPName, null, null, true, null, CommandType.StoredProcedure).ToList());
            }
        }



        public async Task Insert(T entity, string insertSql)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                await conn.ExecuteAsync(insertSql, entity);
            }
        }

        public async Task<List<T>> Select(string selectSql)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                return await Task.Run(() => conn.Query<T>(selectSql).ToList());
            }
        }

        public async Task Update(T entity, string updateSql)
        {
            using (IDbConnection conn = _db.CreateConnection())
            {
                await conn.ExecuteAsync(updateSql, entity);
            }
        }
    }
}
