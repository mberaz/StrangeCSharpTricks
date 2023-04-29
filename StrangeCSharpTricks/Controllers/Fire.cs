using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrangeCSharpTricks.Firebase;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.Controllers
{
    [Route("Fire")]
    public class Fire : Controller
    {
        [HttpGet("Observe/{userId}/Message")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Create([FromRoute] int userId)
        {
            await FireBaseProvider.Observe<MessageModel>($"{userId}/messages", d =>
            {
                Trace.WriteLine($"new message key {d.Key} from user {d.Object.FromUserId}");
            });

            return Ok();
        }

        [HttpPost("{userId}/Message")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Create([FromRoute] int userId, [FromBody] MessageModel messageModel)
        {
            return Ok(await FireBaseProvider.Post(messageModel, $"{userId}/messages"));
        }

        [HttpGet("{userId}/Message/{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageModel>> Get([FromRoute] int userId, [FromRoute] string key)
        {
            return Ok(await FireBaseProvider.Get<MessageModel>($"{userId}/messages/{key}"));
        }

        [HttpGet("{userId}/Message")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageModel>> GetList([FromRoute] int userId)
        {
            return Ok(await FireBaseProvider.GetList<MessageModel>($"{userId}/messages"));
        }
    }
}
