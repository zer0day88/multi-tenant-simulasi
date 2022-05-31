using System;
using System.Threading.Tasks;
using DapperPostgreSQL;
using multi_tenant.Models;

namespace multi_tenant.DAO
{
    public class UserDao
    {
        private readonly SQLConn db;

        public UserDao(SQLConn db)
        {
            this.db = db;
        }
        
        public async Task<mn_user_get_by_id_user_raw> mn_userGetByIdUserRaw(long id_user)
        {
            try
            {   
                var result =
                    await this.db.QuerySPtoSingle<mn_user_get_by_id_user_raw>(
                        "mn_user_get_by_id_user_raw", 
                        new { _id_user=id_user });
                
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}