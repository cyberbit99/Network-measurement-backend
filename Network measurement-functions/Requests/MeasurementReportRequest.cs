﻿using Network_measurement_database.Model;
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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public List<Measurement>? Data { get; set; }
    }
}
