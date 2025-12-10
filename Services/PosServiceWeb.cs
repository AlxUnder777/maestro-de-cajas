using Dapper;
using MaestroDeCajas.Data;
using MaestroDeCajas.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace MaestroDeCajasWeb.Services
{
    public class PosServiceWeb
    {
        public IEnumerable<Pos> GetAll()
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.Query<Pos>("SELECT * FROM POS ORDER BY Id");
            }
        }

        public Pos? GetById(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Pos>("SELECT * FROM POS WHERE Id = @Id", new { Id = id });
            }
        }

        public void Insert(Pos pos)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    INSERT INTO POS 
                    (COD_SUCURSAL, NRO_CAJA, DEPARTAMENTO, TIPO_POOL, PISO, ACTIVO, IP, MODELO, UltimaActualizacion)
                    VALUES (@COD_SUCURSAL, @NRO_CAJA, @DEPARTAMENTO, @TIPO_POOL, @PISO, @ACTIVO, @IP, @MODELO, @UltimaActualizacion);";

                connection.Execute(sql, pos);
            }
        }

        public void Update(Pos pos)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                string sql = @"
                    UPDATE POS 
                    SET COD_SUCURSAL = @COD_SUCURSAL, 
                        NRO_CAJA = @NRO_CAJA, 
                        DEPARTAMENTO = @DEPARTAMENTO, 
                        TIPO_POOL = @TIPO_POOL, 
                        PISO = @PISO, 
                        ACTIVO = @ACTIVO, 
                        IP = @IP, 
                        MODELO = @MODELO, 
                        UltimaActualizacion = @UltimaActualizacion
                    WHERE Id = @Id;";

                connection.Execute(sql, pos);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                connection.Execute("DELETE FROM POS WHERE Id = @Id", new { Id = id });
            }
        }
    }
}


