using Alexa.NET;
using Alexa.NET.Assertions;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using BestillDemo.Abstract;
using BestillDemo.Contract;
using NUnit.Framework;

namespace BestillDemo.Test
{
    public class LaunchRequestHandlerTest
    {
        private readonly ILaunchRequestHandler _launch;

        public LaunchRequestHandlerTest()
        {
            _launch = new LaunchRequestHandler();
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void LaunchRequestHandler_Launch_True()
        {
            var request = new SkillRequest()
            {
                Version = "1.0",
                Request = new LaunchRequest
                {
                    Type = "LaunchRequest"
                }
            };

            // act
            var response = _launch.Launch(request.Session).Result;

            // assert
            Assert.NotNull((response.Response.OutputSpeech as PlainTextOutputSpeech)?.Text);
            //SkillResponse response = ResponseBuilder.Ask("hello world", new Reprompt("hello world"));
            Assert.True((response.Response.OutputSpeech as PlainTextOutputSpeech)?.Text == "Welcome to Be Still. How would you like to start?");
            //response.Asks();
        }
    }
}