using System;
using System.Threading.Tasks;
using DapperPostgreSQL;
using multi_tenant.Models;

namespace multi_tenant.DAO
{
    public class LayananTenantDao
    {
        private readonly SQLConn db;

        public LayananTenantDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<layanan_tenant_get_config> layanan_tenant_get_config(long id_user, long id_tenant)
        {
            try
            {
                return await this.db.QuerySPtoSingle<layanan_tenant_get_config>(
                    "layanan_tenant_get_config",
                    new
                    {
                        _id_user = id_user,
                        _id_tenant = id_tenant
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}