using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.POS;

public class EditModel : PageModel
{
    private readonly PosServiceWeb _posService;

    public EditModel(PosServiceWeb posService)
    {
        _posService = posService;
    }

    [BindProperty]
    public MaestroDeCajas.Models.Pos Pos { get; set; } = default!;

    public IActionResult OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pos = _posService.GetById(id.Value);

        if (pos == null)
        {
            return NotFound();
        }

        Pos = pos;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Pos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _posService.Update(Pos);

        return RedirectToPage("./Index");
    }
}


