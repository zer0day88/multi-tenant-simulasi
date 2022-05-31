using System;
using System.Threading.Tasks;
using DapperPostgreSQL;
using multi_tenant.Models;

namespace multi_tenant.DAO
{
    public class AuthDao
    {
        private readonly SQLConn db;

        public AuthDao(SQLConn db)
        {
            this.db = db;
        }
        
        public async Task<user_account> Authentication(AuthenticationRequest param)
        {
            try
            {
                return await this.db.QuerySPtoSingle<user_account>(
                    "auth_check", 
                    new
                    {
                        _email=param.email,
                        _id_tenant = param.id_tenant
                    });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}