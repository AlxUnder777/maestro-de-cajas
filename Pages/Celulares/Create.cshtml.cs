using MaestroDeCajas.Models;
using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.Celulares;

public class CreateModel : PageModel
{
    private readonly CelularGuardiaServiceWeb _celularGuardiaService;

    public CreateModel(CelularGuardiaServiceWeb celularGuardiaService)
    {
        _celularGuardiaService = celularGuardiaService;
    }

    [BindProperty]
    public CelularGuardia CelularGuardia { get; set; } = new CelularGuardia();

    public IActionResult OnGet()
    {
        CelularGuardia.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        CelularGuardia.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _celularGuardiaService.Insert(CelularGuardia);

        return RedirectToPage("./Index");
    }
}


