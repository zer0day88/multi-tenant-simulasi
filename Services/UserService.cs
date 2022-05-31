using System;
using System.Threading.Tasks;
using DapperPostgreSQL;
using multi_tenant.DAO;
using multi_tenant.Models;

namespace multi_tenant.Services
{
    public interface IUserService
    {
        Task<mn_user_get_by_id_user_raw> mn_userGetByIdUserRaw(long id_user);
    }
    
    public class UserService : IUserService
    {
        private readonly SQLConn _db;
        private readonly UserDao _dao;

        public UserService(SQLConn db, UserDao dao)
        {
            this._db = db;
            this._dao = dao;
        }
        
        public async Task<mn_user_get_by_id_user_raw> mn_userGetByIdUserRaw(long id_user)
        {
            try
            {
                return await _dao.mn_userGetByIdUserRaw(id_user);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}