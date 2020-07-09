using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using BestillDemo.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestillDemo.Contract
{
    public class LaunchRequestHandler : ILaunchRequestHandler
    {
        public Task<SkillResponse> Launch(Session session)
        {
            string msg = $"Welcome to Be Still. How would you like to start?";
            Reprompt rp = new Reprompt(msg);
            var response = ResponseBuilder.Ask(msg, rp, session);
            return Task.FromResult(response);
        }
    }
}
