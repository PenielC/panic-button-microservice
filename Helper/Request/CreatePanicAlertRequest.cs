using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Request
{
    public class CreatePanicAlertRequest
    {
        public string alertId { get; set; }
        public string alertType { get; set; }
        public string profileId { get; set; }
        public string panicStatus { get; set; }
        public bool isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string supportId { get; set; }
    }
}
