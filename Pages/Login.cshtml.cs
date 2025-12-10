using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        // =========================
        //  DATOS DEL FORMULARIO
        // =========================

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        // =========================
        //  ESTADO DE LA VISTA
        // =========================

        public string? ErrorMessage { get; set; }

        // Flags para efectos visuales
        public bool LoginError { get; set; }
        public bool LoginOk { get; set; }

        public string? RedirectUrl { get; set; } = "/Index";

        public void OnGet()
        {
            // Estado limpio al cargar la página
            LoginError = false;
            LoginOk = false;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reseteamos flags en cada POST
            LoginError = false;
            LoginOk = false;
            ErrorMessage = null;

            // Normalizamos un poco
            Username = Username?.Trim() ?? string.Empty;
            Password = Password ?? string.Empty;

            // Validación básica por si vienen vacíos
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Debes ingresar usuario y contraseña.";
                LoginError = true;
                return Page();
            }

            var user = _userService.Login(Username, Password);

            if (user == null)
            {
                ErrorMessage = "Usuario o clave incorrecta";
                LoginError = true;   // dispara animaciones de error en el .cshtml
                return Page();
            }

            // =========================
            //  AUTENTICACIÓN
            // =========================

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("Sucursal", user.Sucursal),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            // Activamos efectos de éxito
            LoginOk = true;
            RedirectUrl = Url.Page("/Index");

            // Volvemos a la misma página para que se vean las animaciones
            return Page();
        }
    }
}
