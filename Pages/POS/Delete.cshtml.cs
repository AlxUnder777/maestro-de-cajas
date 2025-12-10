using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.POS;

public class DeleteModel : PageModel
{
    private readonly PosServiceWeb _posService;

    public DeleteModel(PosServiceWeb posService)
    {
        _posService = posService;
    }

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

    public IActionResult OnPost(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pos = _posService.GetById(id.Value);
        if (pos != null)
        {
            _posService.Delete(id.Value);
        }

        return RedirectToPage("./Index");
    }
}


