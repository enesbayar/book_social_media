using System;
using System.Collections.Generic;
using System.Text;

namespace LoginUser.Domain
{
    public class CompositeBookComment
    {
        public Book book { get; set; }
        public string newComment { get; set; }
    }
}
