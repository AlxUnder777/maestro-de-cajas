using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.RPOS;

public class DeleteModel : PageModel
{
    private readonly RposServiceWeb _rposService;

    public DeleteModel(RposServiceWeb rposService)
    {
        _rposService = rposService;
    }

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

    public IActionResult OnPost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rpos = _rposService.GetById(id.Value);
        if (rpos != null)
        {
            _rposService.Delete(id.Value);
        }

        return RedirectToPage("./Index");
    }
}


