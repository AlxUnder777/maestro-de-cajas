using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.Celulares;

public class IndexModel : PageModel
{
    private readonly CelularGuardiaServiceWeb _celularGuardiaService;

    public IndexModel(CelularGuardiaServiceWeb celularGuardiaService)
    {
        _celularGuardiaService = celularGuardiaService;
    }

    public IList<MaestroDeCajas.Models.CelularGuardia> CelularGuardiaList { get; set; } = new List<MaestroDeCajas.Models.CelularGuardia>();

    public void OnGet()
    {
        CelularGuardiaList = _celularGuardiaService.GetAll().ToList();
    }
}


