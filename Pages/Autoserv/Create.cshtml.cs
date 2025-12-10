using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MaestroDeCajas.Models;
using MaestroDeCajasWeb.Services;

namespace MaestroDeCajasWeb.Pages.Autoserv
{
    public class CreateModel : PageModel
    {
        private readonly AutoservicioServiceWeb _service;

        [BindProperty]
        public Autoservicio Registro { get; set; } = new();

        public CreateModel(AutoservicioServiceWeb service)
        {
            _service = service;
        }

        public void OnGet() {}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Registro.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _service.Add(Registro);

            return RedirectToPage("/Autoserv/Index");
        }
    }
}
