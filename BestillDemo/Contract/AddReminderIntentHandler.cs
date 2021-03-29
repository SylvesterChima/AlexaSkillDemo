using Alexa.NET;
using Alexa.NET.Conversations;
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
    public class AddReminderIntentHandler: IAddReminderIntentHandler
    {
        public Task<SkillResponse> HandleIntent(SkillRequest input, Session session)
        {
            if (input.Request is DialogApiInvokedRequest)
            {
                DialogApiInvokedRequest request = (DialogApiInvokedRequest)input.Request;
                var res = ResponseBuilder.Empty();
                PlainTextOutputSpeech speech = new PlainTextOutputSpeech("Done");
                res.Response = new DialogConversationResponse
                {
                    ApiResponse = new Dictionary<string, object>
                {
                    { "title", "Done" },
                },
                    OutputSpeech = speech,
                    ShouldEndSession = false
                };
                return Task.FromResult(res);
            }
            else
            {
                Alexa.NET.Request.Type.IntentRequest request = (Alexa.NET.Request.Type.IntentRequest)input.Request;
                DialogInputRequest updatedRequest = new DialogInputRequest();
                if (updatedRequest.Input == null)
                {
                    updatedRequest.Input = new UpdatedRequestData();
                }
                updatedRequest.Input.Name = "addReminder";
                var response = ResponseBuilder.Empty();
                response.Response.ShouldEndSession = false;
                response.Response.Directives.Add(DialogDelegateRequestDirective.ToConversations(DelegatePeriod.ExplicitReturn, updatedRequest));
                return Task.FromResult(response);
            }
        }
    }
}
