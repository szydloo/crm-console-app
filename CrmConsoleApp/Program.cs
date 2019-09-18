using System;
using System.Configuration;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace CrmConsoleApp
{
    class Program
    {

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

                    var appService = new AppService(proxy);

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
                                appService.CreateAccount();
                                break;
                            case "2":
                                appService.DeleteAccount();
                                break;
                            case "3":
                                var accs = appService.QueryAccounts();
                                appService.DisplayAccounts(accs);
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
