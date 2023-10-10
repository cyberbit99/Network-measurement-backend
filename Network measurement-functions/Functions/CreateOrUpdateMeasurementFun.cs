using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Network_measurement_functions.Abstracts;
using Network_measurement_database.Repository;
using Network_measurement_database.Model;
using System.Linq;

namespace Network_measurement_functions.Functions
{
    public class CreateOrUpdateMeasurementFun:AFun
    {
        public CreateOrUpdateMeasurementFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("CreateOrUpdateMeasurementFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "measurement/{measurementId}")] HttpRequest req,
            int measurementId,
            int typeId,
            string data,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var measurement = _nMContext.Measurements.Where(x => x.MeasurementId.Equals(measurementId)).FirstOrDefault();
            if (measurement != null)
            {
                measurement.MeasurementId = measurementId;
                measurement.MeasuredData = data;
                measurement.MeasurementTypeId = typeId;
            }


            return new OkObjectResult(measurement);
        }
    }
}
