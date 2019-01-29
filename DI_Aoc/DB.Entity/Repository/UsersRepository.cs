using Dapper;
using DB.Entity.IRepository;
using DB.Entity.Model;
using DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity.Repository
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public async Task<int> DeleteUserAsync(int Id)
        {
            string deleteSql = "DELETE FROM [dbo].[Users] WHERE Id=@Id";
            return await base.Delete(Id, deleteSql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int AddEntity( Users users)
        {
            string spName = "cp_Users_AddRecord";
            using (IDbConnection conn = _db.CreateConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", users.UserName, DbType.String, ParameterDirection.Input, 100);
                parameters.Add("@Password", users.Password, DbType.String, ParameterDirection.Input, 64);
                parameters.Add("@Gender", users.Gender, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Birthday", users.Birthday, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@CreateDate", users.CreateDate, DbType.DateTime, ParameterDirection.Input);
                parameters.Add("@IsDelete", users.IsDelete, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@ID", null, DbType.Int32, ParameterDirection.Output); 
                conn.Execute(spName, parameters, null, null, CommandType.StoredProcedure);
                int iID = parameters.Get<int>("@ID");
                return iID;
            }
        }
        

        public async Task<Users> GetUserDetailAsync(int Id)
        {
            string detailSql = @"SELECT ID, UserName, Password, Gender, Birthday, CreateDate, IsDelete FROM [dbo].[Users] WHERE Id=@Id";
            return await base.Detail(Id, detailSql);
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            string selectSql = @"SELECT  userName,Password,Gender ,Birthday ,CreateDate ,IsDelete FROM [dbo].[Users]";
            return await base.Select(selectSql);
        }

        public async Task PostUserAsync(Users entity)
        {
            string insertSql = @"INSERT INTO [dbo].[Users](Id, UserName, Password, Gender, Birthday, CreateDate, IsDelete) VALUES(@Id, @UserName, @Password, @Gender, @Birthday, @CreateDate, @IsDelete)";
            await base.Insert(entity, insertSql);
        }

        public async Task PutUserAsync(Users entity)
        {
            string updateSql = "UPDATE [dbo].[Users] SET UserName=@UserName, Password=@Password, Gender=@Gender, Birthday=@Birthday, CreateDate=@CreateDate, IsDelete=@IsDelete WHERE Id=@Id";
            await base.Update(entity, updateSql);
        }
    }
}
