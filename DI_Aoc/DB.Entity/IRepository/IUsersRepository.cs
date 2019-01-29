using DB.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity.IRepository
{
    public interface IUsersRepository //: DBAccess.IRepositoryBase<Users>
    {
        #region 扩展的dapper操作
         
        
        int AddEntity(Users users);

        Task<List<Users>> GetUsersAsync();

        Task PostUserAsync(Users entity);

        Task PutUserAsync(Users entity);

        Task<int> DeleteUserAsync(int Id);

        Task<Users> GetUserDetailAsync(int Id);

        #endregion
    }
}
