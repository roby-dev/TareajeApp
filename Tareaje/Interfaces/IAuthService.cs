using Models;

namespace Tareaje.Interfaces {
    public interface IAuthService {
        Task<Usuario> login(Auth auth);
        Task<Usuario> isLogged();
        Task<bool> isValidKey(string key);
        Task<KeyResponse> asignarKey(AsignarLicenciaRequest request);
    }
}
