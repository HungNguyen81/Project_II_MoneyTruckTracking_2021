using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTruckTrackingAPI.Responses
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Unit { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        public string Token { get; set; }
    }
}
