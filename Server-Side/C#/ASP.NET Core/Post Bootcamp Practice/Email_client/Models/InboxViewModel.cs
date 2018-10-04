using System;
using System.Collections.Generic;

namespace Email.Models
{
    public class InboxViewModel
    {
        public List<EmailMessage> emails {get; set;}

        public User loggedUser {get; set;}

    }
}