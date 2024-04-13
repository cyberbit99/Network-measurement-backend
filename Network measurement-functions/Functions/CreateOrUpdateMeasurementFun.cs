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
using Network_measurement_functions.Requests;

namespace Network_measurement_functions.Functions
{
    public class CreateOrUpdateMeasurementFun:AFun
    {
        public CreateOrUpdateMeasurementFun(NMContext nMContext) : base(nMContext)
        {
        }

        [FunctionName("CreateOrUpdateMeasurementFun")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "cou/measurement")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create or update mesurement function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Measurement data = JsonConvert.DeserializeObject<Measurement>(requestBody);

            if (data.MeasurementId != 0)
            {

                var measurement = _nMContext.Measurements.Where(x => x.MeasurementId.Equals(data.MeasurementId)).FirstOrDefault();
                measurement.MeasurementId = data.MeasurementId;
                measurement.MeasuredData = data.MeasuredData;
                measurement.MeasurementTypeId = data.MeasurementTypeId;
                measurement.MeasurementReportId = data.MeasurementReportId;
            }
            else
            {
                Measurement newmeasurement  = new Measurement();
                newmeasurement.MeasuredData = data.MeasuredData;
                newmeasurement.MeasurementTypeId = data.MeasurementTypeId;
                newmeasurement.MeasurementReportId = data.MeasurementReportId;

                _nMContext.Measurements.Add(newmeasurement);

            }
            _nMContext.SaveChanges();

            return new OkObjectResult("OK");
        }
    }
}
