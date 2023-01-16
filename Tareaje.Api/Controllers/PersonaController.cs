using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Configuration;

namespace Tareaje.Api.Controllers {
    [Route("api/persona")]
    [ApiController]
    public class PersonaController : ControllerBase {
        private readonly ConnectionDb _ConnectionDb;
        private readonly PersonaDA personaDA;

        public PersonaController(IOptions<ConnectionDb> options) {
            _ConnectionDb = options.Value;
            personaDA = new PersonaDA(_ConnectionDb.ConnectionString);
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePersona([FromBody] Persona persona) {
            if (!await personaDA.CreatePersona(persona))
                return Problem("No se pudo agregar persona");

            return Ok("Persona agregada correctamente");
        }
    }
}
