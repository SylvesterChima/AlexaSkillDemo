using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using BestillDemo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestillDemo.Contract
{
    public class AddPrayerIntentHandler : IAddPrayerIntentHandler
    {
        public Task<SkillResponse> HandleIntent(IntentRequest input, Session session)
        {
            string itm = input.Intent.Slots["item"].Value;
            string msg = "";
            if (string.IsNullOrEmpty(itm))
            {
                msg = "I didn't get that. Would you like to ask again.";
                Reprompt er = new Reprompt(msg);
                var resp = ResponseBuilder.Ask(msg, er, session);
                return Task.FromResult(resp);
            }
            msg = $"What is your {itm} request?";
            Reprompt rp = new Reprompt(msg);
            var response = ResponseBuilder.Ask(msg, rp, session);
            return Task.FromResult(response);
        }
    }
}
