using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Request
{
    public class PanicAlertResolutionRequest
    {
       public string alertResolutionId { get; set; }
       public string alertId { get; set; }
       public string supportId { get; set; }
       public string resolutionStatement { get; set; }      
    }
}
