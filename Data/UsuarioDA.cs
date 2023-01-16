using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public class UsuarioDA {
        private readonly string _ConnectionString;

        public UsuarioDA(string connectionString) {
            _ConnectionString = connectionString;
        }
        public async Task<bool> CreateUsuario(Usuario usuario) {
            string consulta = @"INSERT INTO Usuario(USUARIO,PASS,ID_PERSONA) VALUES(@pUsuario,@pPass,@pPersona)";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pUsuario", usuario.User);
                    query.Parameters.AddWithValue("@pPass", usuario.Password);
                    query.Parameters.AddWithValue("@pPersona", usuario.PersonaId);
                    query.CommandTimeout = 0;
                    await query.ExecuteNonQueryAsync();
                    con.Close();
                }
                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> AsignarLicenciaUsuario(long licenciaId, long usuarioId) {
            string consulta = @"INSERT INTO UsuarioLicencia(USUARIO_ID,LICENCIA_ID,FECHA) VALUES(@pUsuario,@pLicencia,@pFecha)";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pUsuario", usuarioId);
                    query.Parameters.AddWithValue("@pLicencia", licenciaId);
                    query.Parameters.AddWithValue("@pFecha", DateTime.UtcNow.AddHours(-5));
                    query.CommandTimeout = 0;
                    await query.ExecuteNonQueryAsync();
                    con.Close();
                }
                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Usuario> LoginUsuario(Auth auth) {
            Usuario usuario = new();
            string consulta = @"SELECT ID,USUARIO, ID_PERSONA FROM Usuario WHERE USUARIO = @pUsuario AND PASS = @pPass";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pUsuario", auth.user);
                    query.Parameters.AddWithValue("@pPass", auth.password);
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                usuario.Id = Convert.ToInt32(drGTS["ID"]);
                                usuario.User = Convert.ToString(drGTS["USUARIO"]);
                                usuario.PersonaId = Convert.ToInt32(drGTS["ID_PERSONA"]);
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }

        public async Task<Usuario> GetUsuarioById(long id) {
            Usuario usuario = new();
            string consulta = @"SELECT ID,USUARIO, ID_PERSONA FROM Usuario WHERE ID = @pId";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pId", id);
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                usuario.Id = Convert.ToInt32(drGTS["ID"]);
                                usuario.User = Convert.ToString(drGTS["USUARIO"]);
                                usuario.PersonaId = Convert.ToInt32(drGTS["ID_PERSONA"]);
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return usuario;
        }
    }
}
