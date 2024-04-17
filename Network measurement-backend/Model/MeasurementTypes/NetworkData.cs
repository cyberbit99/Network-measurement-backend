using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_database.Model.Requests
{
    public class NetworkData
    {
        public int StausId { get; set; }
        public string? Ssid { get; set; }
        public string? SsidName { get { return string.IsNullOrWhiteSpace(Ssid) ? "Unknown" : Ssid; } }
        public int IpAddress { get; set; }
        public double SignalStrengthDecibel { get; set; }  // EZT TETTEM HOZZÁ
        public string? GatewayAddress { get; set; }
        public object? NativeObject { get; set; }
        public object? Bssid { get; set; }
        public object? SignalStrength { get; set; }
        public object? SecurityType { get; set; }
    }
}
