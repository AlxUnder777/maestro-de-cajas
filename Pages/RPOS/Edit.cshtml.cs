using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.RPOS;

public class EditModel : PageModel
{
    private readonly RposServiceWeb _rposService;

    public EditModel(RposServiceWeb rposService)
    {
        _rposService = rposService;
    }

    [BindProperty]
    public MaestroDeCajas.Models.Rpos Rpos { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rpos = _rposService.GetById(id.Value);

        if (rpos == null)
        {
            return NotFound();
        }

        Rpos = rpos;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Rpos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _rposService.Update(Rpos);

        return RedirectToPage("./Index");
    }
}


