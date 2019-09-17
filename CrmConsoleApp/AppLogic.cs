using CrmEarlyBound;
using CrmIntfastructure.Repositories;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmConsoleApp
{
    public class AppLogic
    {
        private AccountRepository _accRepository;

        public AppLogic(OrganizationServiceProxy proxy)
        {
            this._accRepository = new AccountRepository(proxy);
        }

        public void DeleteAccount()
        {
            try
            {
                Console.Write("Enter account name to delete: ");

                var name = Console.ReadLine();
                var acc = _accRepository.GetByName(name);

                if (acc is null)
                {
                    Console.WriteLine("Can't find user with this name");
                }
                else
                {
                    _accRepository.Delete(acc.Id);
                    Console.WriteLine("Success \n");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Account> QueryAccounts()
        {
            try
            {
                Console.Write("Enter account name to query by: ");
                var name = Console.ReadLine();
                var accs = _accRepository.GetAllByName(name);

                return accs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateAccount()
        {
            try
            {
                var acc = new Account();
                Console.WriteLine("Create account...");
                Console.Write("Enter Name: ");
                acc.Name = Console.ReadLine();
                Console.Write("Email: ");
                acc.EMailAddress1 = Console.ReadLine();
                Console.Write("Telephone: ");
                acc.Telephone1 = Console.ReadLine();
                Console.Write("City: ");
                acc.Address1_City = Console.ReadLine();

                _accRepository.Create(acc);
                Console.WriteLine("Success \n");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DisplayAccounts(List<Account> accs)
        {
            Console.WriteLine("-----------------------------------------------");
            foreach (var item in accs)
            {
                Console.WriteLine(String.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", item.Name, item.EMailAddress1, item.Telephone1, item.Address1_City));
            }
            Console.WriteLine("-----------------------------------------------");

        }
    }
}
