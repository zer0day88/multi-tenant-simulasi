using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperPostgreSQL;
using multi_tenant.Models;

namespace multi_tenant.DAO
{
    public class PersonDao
    {
        public SQLConn db;
        public async Task<IEnumerable<Person>> PersonGetAll()
        {
            try
            {
                return await this.db.QuerySPtoIEnumerable<Person>(
                    "person_getall");

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}