using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public class LicenciaDA {
        private readonly string _ConnectionString;

        public LicenciaDA(string connectionString) {
            _ConnectionString = connectionString;
        }

        public async Task<Licencia> GetLicenciaByKey(string key) {
            Licencia licencia = new();
            string consulta = @"SELECT ID,CODIGO,ESTADO FROM Licencia WHERE CODIGO = @pCodigo";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pCodigo", key);
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                licencia.Id = Convert.ToInt32(drGTS["ID"]);
                                licencia.Codigo = Convert.ToString(drGTS["CODIGO"]);
                                licencia.Estado = Convert.ToInt32(drGTS["ESTADO"]);                                        
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return licencia;
        }
        public async Task<Licencia> GetLicenciaByKeyByUser(string key,long userId) {
            Licencia licencia = new();
            string consulta = @"SELECT lic.ID,lic.CODIGO,lic.ESTADO FROM Licencia lic 
                                INNER JOIN UsuarioLicencia ulic ON lic.ID = ulic.LICENCIA_ID
                                WHERE ulic.USUARIO_ID = @pUserId AND lic.CODIGO = @pCodigo";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pUserId", userId);
                    query.Parameters.AddWithValue("@pCodigo", key);
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                licencia.Id = Convert.ToInt32(drGTS["ID"]);
                                licencia.Codigo = Convert.ToString(drGTS["CODIGO"]);
                                licencia.Estado = Convert.ToInt32(drGTS["ESTADO"]);
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return licencia;
        }

        public async Task<Licencia> CreateLicencia(Licencia licencia) {
            Licencia newLicencia = new();
            string consulta = @"INSERT INTO Licencia(CODIGO,ESTADO) OUTPUT Inserted.* VALUES(@pCodigo,@pEstado)";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pCodigo", licencia.Codigo);
                    query.Parameters.AddWithValue("@pEstado", licencia.Estado);
                    query.CommandTimeout = 0;
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                newLicencia.Id = Convert.ToInt32(drGTS["ID"]);
                                newLicencia.Codigo = Convert.ToString(drGTS["CODIGO"]);
                                newLicencia.Estado = Convert.ToInt32(drGTS["ESTADO"]);
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return newLicencia;
        }

        public async Task<bool> UpdateLicencia(long id, int estado) {
            string consulta = @"UPDATE Licencia SET ESTADO = @pEstado WHERE ID = @pId";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pId", id);
                    query.Parameters.AddWithValue("@pEstado", estado);
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
    }
}
