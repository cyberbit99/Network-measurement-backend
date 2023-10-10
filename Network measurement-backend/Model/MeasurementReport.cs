using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_database.Model
{
    public partial class MeasurementReport
    {
        public int MeasurementReportId { get; set; }
        public int UserId { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime FinishDate { get; set; }

    }
}
