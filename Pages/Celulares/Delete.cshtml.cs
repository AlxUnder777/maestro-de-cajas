using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.Celulares;

public class DeleteModel : PageModel
{
    private readonly CelularGuardiaServiceWeb _celularGuardiaService;

    public DeleteModel(CelularGuardiaServiceWeb celularGuardiaService)
    {
        _celularGuardiaService = celularGuardiaService;
    }

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

    public IActionResult OnPost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var celularGuardia = _celularGuardiaService.GetById(id.Value);
        if (celularGuardia != null)
        {
            _celularGuardiaService.Delete(id.Value);
        }

        return RedirectToPage("./Index");
    }
}


