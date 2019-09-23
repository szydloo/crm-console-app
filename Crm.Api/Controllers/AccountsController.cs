using Crm.ApiCore.Dtos;
using CrmEarlyBound;
using CrmIntfastructure.Repositories;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Crm.Api.Controllers
{

    public class AccountsController : ApiController
    {

        [HttpGet]
        [Route("api/accounts/{name}")]
        public IHttpActionResult Get(string name)
        {
            var credentials = new ClientCredentials();
            List<Account> accs;


            credentials.UserName.UserName = "mszydlo@mszydlo.onmicrosoft.com";
            credentials.UserName.Password = "MyHardSecret1.";
            var url = "https://mszydlo.api.crm4.dynamics.com/XRMServices/2011/Organization.svc";
            using (var proxy = new OrganizationServiceProxy(new Uri(url), null, credentials, null))
            {
                proxy.EnableProxyTypes();
                var rep = new AccountRepository(proxy);

                accs = rep.GetAllByName(name);
            }

            return Json(accs.Select((x) => new AccountDto {
                Email = x.EMailAddress1,
                Name = x.Name,
                Phone = x.Telephone1,
                City = x.Address1_City
            }));
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]AccountDto dto)
        {
            var credentials = new ClientCredentials();
            credentials.UserName.UserName = "mszydlo@mszydlo.onmicrosoft.com";
            credentials.UserName.Password = "MyHardSecret1.";
            var url = "https://mszydlo.api.crm4.dynamics.com/XRMServices/2011/Organization.svc";
            using (var proxy = new OrganizationServiceProxy(new Uri(url), null, credentials, null))
            {
                proxy.EnableProxyTypes();
                var rep = new AccountRepository(proxy);

                rep.Create(dto.Name, dto.Email, dto.Phone, dto.City);
            }
            return Ok();

        }

        [HttpDelete]
        [Route("api/accounts/{name}")]
        public IHttpActionResult Delete(string name)
        {
            var credentials = new ClientCredentials();
            credentials.UserName.UserName = "mszydlo@mszydlo.onmicrosoft.com";
            credentials.UserName.Password = "MyHardSecret1.";
            var url = "https://mszydlo.api.crm4.dynamics.com/XRMServices/2011/Organization.svc";
            using (var proxy = new OrganizationServiceProxy(new Uri(url), null, credentials, null))
            {
                proxy.EnableProxyTypes();
                var rep = new AccountRepository(proxy);

                var acc = rep.GetByName(name);
                rep.Delete(acc.Id);
            }

            return Ok();
        }
    }
}
