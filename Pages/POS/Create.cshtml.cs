using MaestroDeCajas.Models;
using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MaestroDeCajasWeb.Pages.POS;

public class CreateModel : PageModel
{
    private readonly PosServiceWeb _posService;

    public CreateModel(PosServiceWeb posService)
    {
        _posService = posService;
    }

    [BindProperty]
    public Pos Pos { get; set; } = new Pos();

    public IActionResult OnGet()
    {
        Pos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Pos.UltimaActualizacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _posService.Insert(Pos);

        return RedirectToPage("./Index");
    }
}


