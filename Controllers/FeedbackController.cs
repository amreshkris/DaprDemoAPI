using Dapr;
using Dapr.Client;
using DaprDemoAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestClient.Net;
using System.Threading.Tasks;

namespace DaprDemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
      
        private const string StoreName = "statestore";

        private readonly ILogger _logger;

        public FeedbackController(ILogger<FeedbackController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            _logger.LogInformation("Ping OK");
            return Ok();
        }

        /// <summary>
        /// Receive Feeback from Topic- Feedback
        /// Save the State into State Store
        /// </summary>
        /// <param name="feedback"></param>
        /// <param name="daprClient"></param>
        /// <returns></returns>        
        [Topic("messagebus", "feedback")]
        public async Task<ActionResult<UserFeedback>> ReceiveFeedback(UserFeedback feedback, [FromServices] DaprClient daprClient)
        {
             _logger.LogInformation($"Feedback received {feedback.Id}");

            /*Read from state store and update name if key - first name matches
              Uses .NET SDK Method to retrieve from the state store */
            //Equivalent to call - http://localhost:3500/v1.0/state/<store-name>/<value>

            var state = await daprClient.GetStateEntryAsync<UserFeedback>(StoreName, feedback.FirstName);

            state.Value ??= new UserFeedback() { Id = feedback.FirstName };
            state.Value.EmailId = feedback.EmailId;
            state.Value.Id = feedback.Id;   
            state.Value.Message = feedback.Message;
            state.Value.FirstName = feedback.FirstName;
            state.Value.LastName = feedback.LastName;
            state.Value.DoesLikeSession = feedback.DoesLikeSession;

            _logger.LogInformation($"Feedback received {feedback.Id} - {feedback.FirstName} {feedback.LastName}");

            //save in the state store
            await state.SaveAsync();
            return state.Value; 
        }

        /// <summary>
        /// Gets the account information as specified by the id.
        /// </summary>
        /// <param name="account">Account information for the id from Dapr state store.</param>
        /// <returns>Account information.</returns>
        [HttpGet("{feedback}")]
        public async Task<ActionResult<UserFeedback>> Get([FromState(StoreName)] StateEntry<UserFeedback> feedback)
        {
            _logger.LogInformation($"Account Value is {feedback.Value.EmailId}");

            if (feedback.Value is null)
            {
                return this.NotFound();
            }            
            return feedback.Value;
        }
    }
}
