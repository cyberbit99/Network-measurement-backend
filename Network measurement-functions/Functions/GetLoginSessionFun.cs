using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_database.Repository;
using Network_measurement_functions.Abstracts;
using System.Linq;
using Network_measurement_functions.Requests;

namespace Network_measurement_functions.Functions
{
    public class GetLoginSessionFun : AFun
    {
        public GetLoginSessionFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("GetLoginSessionFun")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "loginsession")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            LoginData data = JsonConvert.DeserializeObject<LoginData>(requestBody);

            var user = _nMContext.Users.Where(x => x.Username == data.Email && x.Password == data.Password);

            if (!user.Any())
            {
                return new OkObjectResult(JsonConvert.SerializeObject( user ));
            }
            else
            {
                return new BadRequestObjectResult("User not found");
            }


        }
    }
}
