using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Request
{
    public class RegistrationRequestViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string profileId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string status { get; set; }      
    }
}
