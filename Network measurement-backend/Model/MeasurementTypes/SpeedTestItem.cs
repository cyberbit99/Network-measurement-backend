using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_database.Model.Requests
{
    public class SpeedTestItem
    {
        public double Down { get; set; }
        public double Up { get; set; }
        public double Ping { get; set; }
    }
}
