using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Network_measurement_functions.Functions
{
    public static class STUploadFun
    {
        [FunctionName("STUploadFun")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "speedtest/upload")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger ST upload function processed a request.");
            using (var ms = new MemoryStream())
            {
                await req.Body.CopyToAsync(ms);
                byte[] byteData = ms.ToArray();

                // Process the byte data here

                return new OkObjectResult("Byte data received and processed successfully.");
            }

            //if (file != null)
            //{
            //    return new OkObjectResult(DateTime.Now + "&" + file.Length);
            //}
            //else
            //{
            //    return new BadRequestResult();

            //}

        }
    }
}
