using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using BestillDemo.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BestillDemo.Controllers
{
    [Route("api/[controller]")]
    public class AlexaController : Controller
    {
        private readonly IAddPrayerIntentHandler _addPrayerIntentHandler;
        private readonly ILaunchRequestHandler _launchRequest;
        private readonly IArchivePrayerIntentHandler _archivePrayerIntentHandler;
        readonly ILogger<AlexaController> _log;
        public AlexaController(IAddPrayerIntentHandler addPrayerIntentHandler, ILaunchRequestHandler launchRequest, IArchivePrayerIntentHandler archivePrayerIntentHandler, ILogger<AlexaController> log)
        {
            _addPrayerIntentHandler = addPrayerIntentHandler;
            _launchRequest = launchRequest;
            _archivePrayerIntentHandler = archivePrayerIntentHandler;
            _log = log;
        }

        [HttpPost]
        public SkillResponse bestil([FromBody]SkillRequest input)
        {

            Session session = input.Session;
            SkillResponse response = ResponseBuilder.Empty();
            _log.LogInformation("Hello, world!");
            if (input.GetRequestType() == typeof(LaunchRequest))
            {
                response = _launchRequest.Launch(session).Result;
            }
            else if (input.GetRequestType() == typeof(IntentRequest))
            {
                var intentRequest = (IntentRequest)input.Request;
                switch (intentRequest.Intent.Name)
                {
                    case "AddPrayerIntent":
                        response = _addPrayerIntentHandler.HandleIntent(intentRequest, session).Result;
                        break;
                    case "ArchivePrayerIntent":
                        response = _archivePrayerIntentHandler.HandleIntent(intentRequest, session).Result;
                        break;
                }
            }

            return response;
        }

    }
}