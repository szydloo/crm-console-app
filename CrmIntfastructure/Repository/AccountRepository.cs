using CrmEarlyBound;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmIntfastructure.Repositories
{
    public class AccountRepository
    {
        private readonly IOrganizationService _service;
        public AccountRepository(IOrganizationService service)
        {
            _service = service;

        }
        public void Create(Account acc)
        {
            _service.Create(acc);
        }

        public void Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _service.Delete("account", id);
            }
            else
            {
                throw new Exception("Invalid guid");
            }
        }

        public Account GetByName(string name)
        {
            using (var context = new CrmOrganizationServiceContext(_service))
            {
                var acc = context.CreateQuery<Account>().Where(x => x.Name == name).FirstOrDefault();
                return acc;
            }

        }

        public List<Account> GetAllByName(string name)
        {
            using (var context = new CrmOrganizationServiceContext(_service))
            {
                var accs = context.CreateQuery<Account>().Where(x => x.Name.Contains(name)).ToList();

                return accs;
            }
        }
    }
}
