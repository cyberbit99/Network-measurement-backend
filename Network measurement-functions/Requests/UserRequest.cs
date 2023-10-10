using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network_measurement_functions.Requests
{
    public class UserRequest
    {
        [JsonProperty(Required = Required.Always)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
