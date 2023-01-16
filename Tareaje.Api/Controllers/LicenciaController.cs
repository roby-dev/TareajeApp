using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.Configuration;

namespace Tareaje.Api.Controllers {
    [Route("api/licencia")]
    [ApiController]
    public class LicenciaController : ControllerBase {

        private readonly ConnectionDb _ConnectionDb;
        private readonly LicenciaDA licenciaDA;
        private readonly UsuarioDA usuarioDA;

        public LicenciaController(IOptions<ConnectionDb> options) {
            _ConnectionDb = options.Value;
            licenciaDA = new LicenciaDA(_ConnectionDb.ConnectionString);
            usuarioDA = new UsuarioDA(_ConnectionDb.ConnectionString);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateLicencia() {
            var licencia = Licencia.Create(Guid.NewGuid().ToString().ToUpper(), 1);
            var licenciaCreada = await licenciaDA.CreateLicencia(licencia);
            if(licenciaCreada.Id == 0) {
                return Problem("No se pudo crear licencia");
            }
            
            return Ok(licenciaCreada);
        }

        [HttpPut()]
        public async Task<IActionResult> AsignarLicencia([FromBody] AsignarLicenciaRequest request) {
            var licencia = await licenciaDA.GetLicenciaByKey(request.Key);
            DefaultResponse response = new();
            if (String.IsNullOrEmpty(licencia.Codigo)) {
                response.message = "Licencia no válida.";             
                return Ok(response);
            }

            if (licencia.Estado == 0) {
                response.message = "Licencia ya asignada.";
                return Ok(response);
            }

            int estado = 0;

            if (await licenciaDA.UpdateLicencia(licencia.Id,estado) && await usuarioDA.AsignarLicenciaUsuario(licencia.Id, request.UsuarioId)) {
                response.status = true;
                response.message = "Licencia asignada correctamente";
            } else {
                response.message = "No se pudo asignar licencia";
            }
                
            return Ok(response);
        }
    }
}
