using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperPostgreSQL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using multi_tenant.DAO;
using multi_tenant.Models;
using PIS.API.Helper;

namespace multi_tenant.Services
{
    
    public interface IPersonService
    {
        Task<IEnumerable<Person>> PersonGetAll();
    }
    
    public class PersonService : IPersonService
    {
        private SQLConn _db;
        private readonly PersonDao _personDao;
        private readonly LayananTenantDao _layananTenantDao;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public PersonService(
            PersonDao personDao, LayananTenantDao layananTenantDao, IHttpContextAccessor httpContextAccessor)
        {
            this._personDao = personDao;
            this._layananTenantDao = layananTenantDao;
            this._httpContextAccessor = httpContextAccessor;

            long id_user = HttpContextHelper.GetCurrentUserId(this._httpContextAccessor.HttpContext);
            long id_tenant = HttpContextHelper.GetCurrenTenantId(this._httpContextAccessor.HttpContext);
            var dbConfig =
                _layananTenantDao.layanan_tenant_get_config(id_user,id_tenant);
            
            this._db = new SQLConn(dbConfig.Result.getConnectionString());
            this._personDao.db = this._db;
        }
        
        public async Task<IEnumerable<Person>> PersonGetAll()
        {
            try
            {
                return await _personDao.PersonGetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}