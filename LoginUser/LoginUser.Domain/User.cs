using System;

namespace LoginUser.Domain
{
    public class User
    {
        public string eMail { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
