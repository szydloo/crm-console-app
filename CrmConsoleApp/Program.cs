using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel.Description;
using System;
using CrmConsoleApp.Models;
using Microsoft.Xrm.Client;
using System.Linq;
using System.Windows;
using System.Configuration;

namespace CrmConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService organizationService = null;

            try
            {
                organizationService = EstablishConnection();
                CrmOrganizationServiceContext context = new CrmOrganizationServiceContext(organizationService);
                string line = "";
                while ((line = Console.ReadLine()) != "x")
                {
                    switch (line)
                    {
                        case "1":
                            CreateAccount(context);
                            break;
                        case "2":
                            DeleteAccount(context);
                            break;
                        case "3":
                            QueryAccounts(context);
                            break;
                        default:
                            Console.WriteLine("Invalid character");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught - " + ex.Message);
            }
            Console.ReadKey();
        }

        private static IOrganizationService EstablishConnection()
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = ConfigurationManager.AppSettings["username"];
            clientCredentials.UserName.Password = ConfigurationManager.AppSettings["password"];

            // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://mszydlo.api.crm4.dynamics.com/XRMServices/2011/Organization.svc"),
             null, clientCredentials, null);

            if (organizationService != null)
            {
                Guid userid = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).UserId;

                if (userid != Guid.Empty)
                {
                    Console.WriteLine("Connection Established Successfully...");
                }
            }
            else
            {
                Console.WriteLine("Failed to establish connection!!!");
                Console.WriteLine("Press any key to exit application...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            return organizationService;

        }

        private static void DeleteAccount(CrmOrganizationServiceContext context)
        {
            try
            {
                Console.WriteLine("Enter account name to delete:");
                var name = Console.ReadLine();
                var accId = context.CreateQuery<Account>().Where(x => x.Name == name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void QueryAccounts(CrmOrganizationServiceContext context)
        {
        }

        private static void CreateAccount(CrmOrganizationServiceContext service)
        {
            try
            {
                var acc = new Account();
                acc.Name = Console.ReadLine();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
