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
    public class AppService
    {
        private AccountRepository _accRepository;

        public AppService(OrganizationServiceProxy proxy)
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
                    Console.WriteLine($"Can't find user with name: {name}");
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
                Console.WriteLine("Create account...");
                Console.Write("Enter Name: ");
                var name = Console.ReadLine();
                Console.Write("Email: ");
                var email = Console.ReadLine();
                Console.Write("Telephone: ");
                var phone = Console.ReadLine();
                Console.Write("City: ");
                var city = Console.ReadLine();

                _accRepository.Create(name, email, phone, city);
                Console.WriteLine("Success \n");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DisplayAccounts(List<Account> accs)
        {
            if (accs != null && accs.Count > 0)
            {
                foreach (var item in accs)
                {
                    Console.WriteLine(String.Format("|{0,15}|{1,15}|{2,15}|{3,15}|", item.Name, item.EMailAddress1, item.Telephone1, item.Address1_City));
                }
            }
            else
            {
                Console.WriteLine("No items to display...");
            }

        }
    }
}
