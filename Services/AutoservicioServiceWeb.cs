using Dapper;
using MaestroDeCajas.Models;
using MaestroDeCajas.Data;
using Microsoft.Data.Sqlite;

namespace MaestroDeCajasWeb.Services
{
    public class AutoservicioServiceWeb
    {
        public IEnumerable<Autoservicio> GetAll()
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            return con.Query<Autoservicio>("SELECT * FROM AUTOSERVICIO ORDER BY Id");
        }

        public Autoservicio? GetById(int id)
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            return con.QueryFirstOrDefault<Autoservicio>(
                "SELECT * FROM AUTOSERVICIO WHERE Id=@Id", new { Id = id });
        }

        public void Add(Autoservicio a)
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            string sql = @"INSERT INTO AUTOSERVICIO
                (COD_SUCURSAL, NRO_CAJA, PISO, DEPARTAMENTO, MAC_WIFI, CODIGO_ACTIVACION,
                 NUMERO_SERIE_PINPAD, DDLL, IP, UltimaActualizacion)
                VALUES (@COD_SUCURSAL, @NRO_CAJA, @PISO, @DEPARTAMENTO, @MAC_WIFI,
                        @CODIGO_ACTIVACION, @NUMERO_SERIE_PINPAD, @DDLL, @IP, @UltimaActualizacion)";
            con.Execute(sql, a);
        }

        public void Update(Autoservicio a)
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            string sql = @"UPDATE AUTOSERVICIO SET
                COD_SUCURSAL=@COD_SUCURSAL, NRO_CAJA=@NRO_CAJA, PISO=@PISO,
                DEPARTAMENTO=@DEPARTAMENTO, MAC_WIFI=@MAC_WIFI,
                CODIGO_ACTIVACION=@CODIGO_ACTIVACION,
                NUMERO_SERIE_PINPAD=@NUMERO_SERIE_PINPAD,
                DDLL=@DDLL, IP=@IP, UltimaActualizacion=@UltimaActualizacion
                WHERE Id=@Id";
            con.Execute(sql, a);
        }

        public void Delete(int id)
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            con.Execute("DELETE FROM AUTOSERVICIO WHERE Id=@Id", new { Id = id });
        }
    }
}

