﻿using Alexa.NET;
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
    public class ArchivePrayerIntentHandler : IArchivePrayerIntentHandler
    {
        public Task<SkillResponse> HandleIntent(SkillRequest input, Session session)
        {
            string msg = $"Archived successfully";
            Reprompt rp = new Reprompt(msg);
            var response = ResponseBuilder.Ask(msg, rp, session);
            return Task.FromResult(response);
        }
    }
}
