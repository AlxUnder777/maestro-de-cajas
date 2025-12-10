using Dapper;
using MaestroDeCajas.Data;
using MaestroDeCajas.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace MaestroDeCajasWeb.Services
{
    public class RposServiceWeb
    {
        public IEnumerable<Rpos> GetAll()
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.Query<Rpos>("SELECT * FROM RPOS ORDER BY Id");
            }
        }

        public Rpos? GetById(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Rpos>("SELECT * FROM RPOS WHERE Id = @Id", new { Id = id });
            }
        }

        public void Insert(Rpos rpos)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    INSERT INTO RPOS 
                    (COD_SUCURSAL, NRO_CAJA, TIPO_DISPOSITIVO, MODELO, MAC_WIFI, CODIGO_ACTIVACION,
                     FUNCIONANDO, NUMERO_SERIE_PINPAD, DDLL, PINPAD_FUNCIONA, ESPECIFICAR_ERROR, UltimaActualizacion)
                    VALUES
                    (@COD_SUCURSAL, @NRO_CAJA, @TIPO_DISPOSITIVO, @MODELO, @MAC_WIFI, @CODIGO_ACTIVACION,
                     @FUNCIONANDO, @NUMERO_SERIE_PINPAD, @DDLL, @PINPAD_FUNCIONA, @ESPECIFICAR_ERROR,
                     @UltimaActualizacion);";

                connection.Execute(sql, rpos);
            }
        }

        public void Update(Rpos rpos)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    UPDATE RPOS 
                    SET COD_SUCURSAL = @COD_SUCURSAL, 
                        NRO_CAJA = @NRO_CAJA, 
                        TIPO_DISPOSITIVO = @TIPO_DISPOSITIVO, 
                        MODELO = @MODELO, 
                        MAC_WIFI = @MAC_WIFI, 
                        CODIGO_ACTIVACION = @CODIGO_ACTIVACION,
                        FUNCIONANDO = @FUNCIONANDO, 
                        NUMERO_SERIE_PINPAD = @NUMERO_SERIE_PINPAD, 
                        DDLL = @DDLL, 
                        PINPAD_FUNCIONA = @PINPAD_FUNCIONA, 
                        ESPECIFICAR_ERROR = @ESPECIFICAR_ERROR, 
                        UltimaActualizacion = @UltimaActualizacion
                    WHERE Id = @Id;";

                connection.Execute(sql, rpos);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                connection.Execute("DELETE FROM RPOS WHERE Id = @Id", new { Id = id });
            }
        }
    }
}


