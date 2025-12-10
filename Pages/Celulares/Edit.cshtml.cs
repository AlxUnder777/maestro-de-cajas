using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.Celulares;

public class EditModel : PageModel
{
    private readonly CelularGuardiaServiceWeb _celularGuardiaService;

    public EditModel(CelularGuardiaServiceWeb celularGuardiaService)
    {
        _celularGuardiaService = celularGuardiaService;
    }

    [BindProperty]
    public MaestroDeCajas.Models.CelularGuardia CelularGuardia { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var celularGuardia = _celularGuardiaService.GetById(id.Value);

        if (celularGuardia == null)
        {
            return NotFound();
        }

        CelularGuardia = celularGuardia;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CelularGuardia.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _celularGuardiaService.Update(CelularGuardia);

        return RedirectToPage("./Index");
    }
}


