using System;
using System.Collections.Generic;

namespace Email.Models
{
    public class ReplyViewModel
    {
        public EmailMessage email {get; set;}

        public Reply reply {get; set;}

        public List<Reply> replies {get; set;}
    }
}