using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MaestroDeCajas.Models;
using MaestroDeCajasWeb.Services;

namespace MaestroDeCajasWeb.Pages.Autoserv
{
    public class EditModel : PageModel
    {
        private readonly AutoservicioServiceWeb _service;

        [BindProperty]
        public Autoservicio Registro { get; set; } = default!;

        public EditModel(AutoservicioServiceWeb service)
        {
            _service = service;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var data = _service.GetById(id.Value);
            if (data == null) return NotFound();

            Registro = data;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Registro.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _service.Update(Registro);
            return RedirectToPage("/Autoserv/Index");
        }
    }
}
