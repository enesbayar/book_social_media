using System;
using System.Collections;
using System.Collections.Generic;

namespace LoginUser.Domain
{
    public class User
    {
        public string eMail { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string likesBookList { get; set; }
        public string readBookList { get; set; }
        public DateTime CreatedDate { get; set; }
        

    }
}
