using Dapper;
using MaestroDeCajas.Data;
using MaestroDeCajas.Models;
using Microsoft.Data.Sqlite;

namespace MaestroDeCajasWeb.Services
{
    public class UserService
    {
        public User? Login(string username, string password)
        {
            using var con = new SqliteConnection(Database.ConnectionString);
            return con.QueryFirstOrDefault<User>(
                "SELECT * FROM USERS WHERE Username=@u AND Password=@p",
                new { u = username, p = password }
            );
        }
    }
}
