using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MaestroDeCajasWeb.Services;
using MaestroDeCajas.Models;

namespace MaestroDeCajasWeb.Pages.Autoserv
{
    public class DeleteModel : PageModel
    {
        private readonly AutoservicioServiceWeb _service;

        public DeleteModel(AutoservicioServiceWeb service)
        {
            _service = service;
        }

        public Autoservicio Registro { get; set; } = default!;

        public IActionResult OnGet(int? id)
        {
            if (id == null) return NotFound();

            var data = _service.GetById(id.Value);
            if (data == null) return NotFound();

            Registro = data;
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null) return NotFound();

            _service.Delete(id.Value);
            return RedirectToPage("/Autoserv/Index");
        }
    }
}
