using System.Threading.Tasks;
using ContactMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oak.Events;

namespace ContactMe.Controllers
{
    [ApiController]
    [Route("contact")]
    public class ContactMeController : ControllerBase
    {
        private readonly ILogger<ContactMeController> _logger;
        private readonly IEventDispatcher _eventDispatcher;

        public ContactMeController(
            ILogger<ContactMeController> logger,
            IEventDispatcher eventDispatcher)
        {
            this._logger = logger;
            this._eventDispatcher = eventDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> ContactMe([FromBody] ContactMeBindingModel model)
        {
            await this._eventDispatcher.EmitAsync(new ContactMeEvent(this, model));   
            return Ok();
        }
    }
}
