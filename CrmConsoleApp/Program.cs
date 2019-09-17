using System;
using System.Configuration;
using CrmIntfastructure.Repositories;
using System.Collections.Generic;
using CrmEarlyBound;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace CrmConsoleApp
{
    class Program
    {
        private static AccountRepository _accRepository;

        static void Main(string[] args)
        {
            try
            {
                var credentials = new ClientCredentials();
                credentials.UserName.UserName = ConfigurationManager.AppSettings["username"];
                credentials.UserName.Password = ConfigurationManager.AppSettings["password"];
                using (var proxy = new OrganizationServiceProxy(new Uri("https://mszydlo.api.crm4.dynamics.com/XRMServices/2011/Organization.svc"), null, credentials, null))
                {
                    proxy.EnableProxyTypes();

                    _accRepository = new AccountRepository(proxy);
                    var appLogic = new AppLogic(proxy);

                    Console.WriteLine("Press 1 to create account");
                    Console.WriteLine("Press 2 to delete account");
                    Console.WriteLine("Press 3 to query accounts");
                    Console.WriteLine("Press x to exit");

                    string line = "";
                    while ((line = Console.ReadLine()) != "x")
                    {
                        switch (line)
                        {
                            case "1":
                                appLogic.CreateAccount();
                                break;
                            case "2":
                                appLogic.DeleteAccount();
                                break;
                            case "3":
                                var accs = appLogic.QueryAccounts();
                                appLogic.DisplayAccounts(accs);
                                break;
                            default:
                                Console.WriteLine("Invalid character, try again...");
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught - " + ex.Message);
            }
            Console.ReadKey();
        }
    }
}
