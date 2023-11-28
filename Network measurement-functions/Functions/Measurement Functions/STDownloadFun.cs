using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace Network_measurement_functions.Functions
{
    public static class STDownloadFun
    {
        [FunctionName("STDownloadFun")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "speedtest/download")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger ST download function processed a request.");
            string path = "randomfile.bin";
            int fileSizeInMB = 10;
            byte[] randomBytes;
            if (File.Exists(path))
            {
                randomBytes = File.ReadAllBytes("randomfile.bin");
            }
            else
            {
                // Size of the random file in MB (adjust as needed)
                randomBytes = GenerateRandomBytes(fileSizeInMB); // Convert MB to bytes
                File.WriteAllBytes("randomfile.bin", randomBytes);
            }
             

            var response = new FileContentResult(randomBytes, "application/octet-stream")
            {
                FileDownloadName = "randomfile.bin"
            };

            return response;

        }

        private static byte[] GenerateRandomBytes(int size)
        {
            byte[] randomBytes = new byte[size * 1024 * 1024];
            new Random().NextBytes(randomBytes);
            return randomBytes;
        }
    }
}
