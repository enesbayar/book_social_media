using System;

namespace LoginUser.Domain
{
    public class UserOperationResult : ResultMessage
    {
        public DateTime createdDate { get; set; }
        public string message { get; set; }

    }
}
