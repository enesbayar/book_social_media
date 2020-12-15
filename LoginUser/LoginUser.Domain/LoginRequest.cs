using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginUser.Domain
{
    public class LoginRequest{
        public string userName { get; set; }
        public string password { get; set; }
    }
}
