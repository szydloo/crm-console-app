using CrmConsoleApp.Models;
using Microsoft.Xrm.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmConsoleApp.Repositories
{
    public class AccountRepository
    {
        private readonly CrmOrganizationServiceContext _context;

        public AccountRepository(CrmOrganizationServiceContext context)
        {
            _context = context;
        }

        public void Create(Account acc)
        {
            _context.Create(acc);
        }

    }
}
