using MaestroDeCajas.Models;
using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.RPOS;

public class CreateModel : PageModel
{
    private readonly RposServiceWeb _rposService;

    public CreateModel(RposServiceWeb rposService)
    {
        _rposService = rposService;
    }

    [BindProperty]
    public Rpos Rpos { get; set; } = new Rpos();

    public IActionResult OnGet()
    {
        Rpos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Rpos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _rposService.Insert(Rpos);

        return RedirectToPage("./Index");
    }
}


