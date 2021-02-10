using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Request
{
    public class CreatePanicAlertRequest
    {
        public string alertType { get; set; }
        public string panicStatus { get; set; }
        public bool isActive { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
