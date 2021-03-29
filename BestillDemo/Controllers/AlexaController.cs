using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using BestillDemo.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BestillDemo.Controllers
{
    [Route("api/[controller]")]
    public class AlexaController : Controller
    {
        private readonly IAddPrayerIntentHandler _addPrayerIntentHandler;
        private readonly ILaunchRequestHandler _launchRequest;
        private readonly IArchivePrayerIntentHandler _archivePrayerIntentHandler;
        private readonly IAddReminderIntentHandler _reminderIntentHandler;
        readonly ILogger<AlexaController> _log;
        public AlexaController(IAddPrayerIntentHandler addPrayerIntentHandler, ILaunchRequestHandler launchRequest, IArchivePrayerIntentHandler archivePrayerIntentHandler, IAddReminderIntentHandler reminderIntentHandler, ILogger<AlexaController> log)
        {
            _addPrayerIntentHandler = addPrayerIntentHandler;
            _launchRequest = launchRequest;
            _archivePrayerIntentHandler = archivePrayerIntentHandler;
            _reminderIntentHandler = reminderIntentHandler;
            _log = log;
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<SkillResponse> bestil()
        {
            var dd = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                dd = await reader.ReadToEndAsync();
            }

            SkillRequest input = JsonConvert.DeserializeObject<SkillRequest>(dd, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

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
                        response = _addPrayerIntentHandler.HandleIntent(input, session).Result;
                        break;
                    case "s":
                        response = _archivePrayerIntentHandler.HandleIntent(input, session).Result;
                        break;
                    case "AddReminderIntent":
                        response = _reminderIntentHandler.HandleIntent(input, session).Result;
                        break;
                }
            }

            return response;
        }

    }
}