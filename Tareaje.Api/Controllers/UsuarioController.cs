using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Configuration;
using Models;

namespace Tareaje.Api.Controllers {
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        private readonly ConnectionDb _ConnectionDb;
        private readonly UsuarioDA usuarioDA;

        public UsuarioController(IOptions<ConnectionDb> options) {
            _ConnectionDb = options.Value;
            usuarioDA = new UsuarioDA(_ConnectionDb.ConnectionString);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario) {
            if (!await usuarioDA.CreateUsuario(usuario))
                return Problem("No se pudo agregar usuario");

            return Ok("Usuario creado");
        }
    }
}
