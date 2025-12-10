using Dapper;
using MaestroDeCajas.Data;
using MaestroDeCajas.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace MaestroDeCajasWeb.Services
{
    public class CelularGuardiaServiceWeb
    {
        public IEnumerable<CelularGuardia> GetAll()
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.Query<CelularGuardia>("SELECT * FROM CELULARES_DE_GUARDIA ORDER BY Id");
            }
        }

        public CelularGuardia? GetById(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.QueryFirstOrDefault<CelularGuardia>(
                    "SELECT * FROM CELULARES_DE_GUARDIA WHERE Id = @Id",
                    new { Id = id }
                );
            }
        }

        public void Insert(CelularGuardia cel)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    INSERT INTO CELULARES_DE_GUARDIA 
                    (COD_SUCURSAL, MAC_WIFI, USO, FUNCIONANDO, ESPECIFICAR_ERROR, UltimaActualizacion)
                    VALUES
                    (@COD_SUCURSAL, @MAC_WIFI, @USO, @FUNCIONANDO, @ESPECIFICAR_ERROR, @UltimaActualizacion);";

                connection.Execute(sql, cel);
            }
        }

        public void Update(CelularGuardia cel)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    UPDATE CELULARES_DE_GUARDIA 
                    SET COD_SUCURSAL = @COD_SUCURSAL, 
                        MAC_WIFI = @MAC_WIFI, 
                        USO = @USO, 
                        FUNCIONANDO = @FUNCIONANDO, 
                        ESPECIFICAR_ERROR = @ESPECIFICAR_ERROR, 
                        UltimaActualizacion = @UltimaActualizacion
                    WHERE Id = @Id;";

                connection.Execute(sql, cel);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                connection.Execute("DELETE FROM CELULARES_DE_GUARDIA WHERE Id = @Id", new { Id = id });
            }
        }
    }
}


