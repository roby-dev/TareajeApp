using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Configuration;

namespace Tareaje.Api.Controllers {
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly ConnectionDb _ConnectionDb;
        private readonly UsuarioDA usuarioDA;
        private readonly LicenciaDA licenciaDA;

        public AuthController(IOptions<ConnectionDb> options) {
            _ConnectionDb = options.Value;
            usuarioDA = new UsuarioDA(_ConnectionDb.ConnectionString);
            licenciaDA = new LicenciaDA(_ConnectionDb.ConnectionString);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Auth auth) {
            return Ok(await usuarioDA.LoginUsuario(auth));
        }

        [HttpGet("renew/{id}")]
        public async Task<IActionResult> login(string id) {
            return Ok(await usuarioDA.GetUsuarioById(Convert.ToInt32(id)));
        }

        [HttpPost("valid-key")]
        public async Task<IActionResult> login([FromBody] ValidKeyRequest request) {
            return Ok(await licenciaDA.GetLicenciaByKeyByUser(request.key,request.UserId));
        }
    }
}
