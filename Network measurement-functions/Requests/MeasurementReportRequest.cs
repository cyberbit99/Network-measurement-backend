using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_functions.Requests
{
    public class MeasurementReportRequest
    {
        public int MeasurementReportId { get; set; }
        public int UserId { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
