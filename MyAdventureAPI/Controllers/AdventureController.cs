using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyAdventureAPI.DatabaseContext;
using MyAdventureAPI.models;
using MyAdventureAPI.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAdventureAPI.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class AdventureController : Controller
    {

        private readonly ILogger<AdventureController> _logger;

        private readonly AdventureService _service;

        public AdventureController(AdventureService service,ILogger<AdventureController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service;
        }

        [HttpGet]
        public async Task<List<Adventure>> Get() => await _service.GetAsync();


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Adventure>> Get(string id)
        {
            var Adventure = await _service.GetAsync(id);

            if (Adventure is null)
            {
                return NotFound();
            }

            return Adventure;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Adventure newAdventure)
        {
            await _service.CreateAsync(newAdventure);

            return CreatedAtAction(nameof(Get), new { id = newAdventure.Id }, newAdventure);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Adventure updatedAdventure)
        {
            var Adventure = await _service.GetAsync(id);

            if (Adventure is null)
            {
                return NotFound();
            }

            updatedAdventure.Id = Adventure.Id;

            await _service.UpdateAsync(id, updatedAdventure);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Adventure = await _service.GetAsync(id);

            if (Adventure is null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
