using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Response
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string  profileId { get; set; }
        public string lastname { get; set; }
        public string userId { get; set; }
    }
}
