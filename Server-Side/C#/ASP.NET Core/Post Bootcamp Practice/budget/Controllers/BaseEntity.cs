using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace budget.Controllers
{
    public class BaseEntity : Controller 
    {
        public bool IsUserInSession()
        {
            if(HttpContext.Session.GetInt32("loggedUser") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SendText(long sentTo, string messageContent)
        {
            const string accountSid = "ACc4fa038eedca363d0651905c37bcf9ac";
            const string authToken = "484751994e76ea4d899fa5efc8b20614";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"{messageContent}",
                from: new Twilio.Types.PhoneNumber("+15403025243"),
                to: new Twilio.Types.PhoneNumber($"+1{sentTo}")
            );
        }
    }
}