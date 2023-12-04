using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_database.Model;
using Network_measurement_database.Repository;
using Network_measurement_functions.Abstracts;
using System.Linq;

namespace Network_measurement_functions.Functions
{
    public class CreateOrUpdateUserFun:AFun
    {
        public CreateOrUpdateUserFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("CreateOrUpdateUserFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cou/user")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create or update user function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            User data = JsonConvert.DeserializeObject<User>(requestBody);

            var user = _nMContext.Users.Where(x => x.UserId.Equals(data.UserId)).FirstOrDefault();
            if (user != null)
            {
                user.UserEmail = data.UserEmail;
                user.UserRoleId = data.UserRoleId;
                user.Username = data.Username;
                user.Password = data.Password;
                user.Firstname = data.Firstname;
                user.Lastname = data.Lastname;
            }
            else
            {
                User newitem = new User();
                newitem.UserRoleId = data.UserRoleId;
                newitem.Username = data.Username;
                newitem.Password = data.Password;
                newitem.Firstname = data.Firstname;
                newitem.Lastname = data.Lastname;


                _nMContext.Users.Add(newitem);

            }
            _nMContext.SaveChanges();

            return new OkObjectResult("OK");
        }
    }
}
