using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAdventureAPI.models;
using MyAdventureAPI.Service;

namespace MyAdventureAPI.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class GameController : Controller
    {
        private readonly ILogger<AdventureController> _logger;

        private readonly AdventureService _service;

        public GameController(AdventureService service, ILogger<AdventureController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        public async Task<List<Adventure>> Get() => await _service.GetAsync();

        [HttpPost]
        public async Task<IActionResult> StartNewAdventure(AdventureStartRequest newAdventure)
        {
            var Adventure = await _service.GetAsync(newAdventure.AdventureId);

            if (Adventure is null)
            {
                return NotFound();
            }
            AdventureSession session = new AdventureSession();
            session.AdventureId = Adventure.Id;
            session.StepsTaken = new List<AdventureStepRecord>();
            session.IsComplete = false;
            await _service.CreateAsync(session);

            return CreatedAtAction(nameof(Get), new { id = session.Id }, session);
        }


        [HttpGet("/v1/[controller]/{id:length(24)}")]
        public async Task<ActionResult<AdventureSession>> GetSession(string id)
        {
            var session = await _service.GetAsyncSession(id);

            if (session is null)
            {
                return NotFound();
            }

            return session;
        }

        [HttpPut]
        public async Task<ActionResult<AdventureSession>> UpdateSession(AdventureSessionStep sessionStep)
        {
            var session = await _service.GetAsyncSession(sessionStep.SessionId);

            if (session is null)
            {
                return NotFound();
            }

            if(session.IsComplete)
            {
                return NotFound();
            }

            var Adventure = await _service.GetAsync(session.AdventureId);

            if (Adventure is null)
            {
                return NotFound();
            }

           var step =  Adventure.Steps.FirstOrDefault(a => a.Id == sessionStep.StepId);

            if (step is null)
            {
                return NotFound();
            }

            AdventureStepRecord record = new AdventureStepRecord();
            record.StepId = step.Id;
            record.OptionTaken = sessionStep.Choice;

            if(step.Options.Count == 0)
            {
                session.IsComplete = true;
            }
            session.StepsTaken.Add(record);

            await _service.UpdateSessionAsync(session.Id,session);

            return session;
        }
    }
}
