using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    public class PersonaDA {
        private readonly string _ConnectionString;

        public PersonaDA(string connectionString) {
            _ConnectionString = connectionString;
        }

        public async Task<Persona> CreatePersona(Persona persona) {
            Persona newPersona = new();
            string consulta = @"INSERT INTO Persona(NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,DOCUMENTO) 
                                OUTPUT Inserted.* 
                                VALUES(@pNombres,@pApPaterno,@pApMaterno,@pDocumento)";
            try {
                using (var con = new SqlConnection(_ConnectionString)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@pNombres", persona.Nombres);
                    query.Parameters.AddWithValue("@pApPaterno", persona.ApellidoPaterno);
                    query.Parameters.AddWithValue("@pApMaterno", persona.ApellidoMaterno);
                    query.Parameters.AddWithValue("@pDocumento", persona.Documento);
                    query.CommandTimeout = 0;
                    using (SqlDataReader drGTS = await query.ExecuteReaderAsync()) {
                        if (drGTS.HasRows) {
                            while (drGTS.Read()) {
                                newPersona.Id = Convert.ToInt32(drGTS["ID"]);
                                newPersona.Nombres = Convert.ToString(drGTS["NOMBRES"]);
                                newPersona.ApellidoPaterno = Convert.ToString(drGTS["APELLIDO_PATERNO"]);
                                newPersona.ApellidoMaterno = Convert.ToString(drGTS["APELLIDO_MATERNO"]);
                                newPersona.Documento = Convert.ToString(drGTS["DOCUMENTO"]);
                            }
                        }
                    }
                    con.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return newPersona;
        }
    }
}
