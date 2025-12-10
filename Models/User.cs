namespace MaestroDeCajas.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Sucursal { get; set; } = "";
        public string Rol { get; set; } = "";
    }
}
