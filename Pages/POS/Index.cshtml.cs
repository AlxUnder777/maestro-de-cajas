using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.POS;

public class IndexModel : PageModel
{
    private readonly PosServiceWeb _posService;

    public IndexModel(PosServiceWeb posService)
    {
        _posService = posService;
    }

    public IList<MaestroDeCajas.Models.Pos> PosList { get; set; } = new List<MaestroDeCajas.Models.Pos>();

    public void OnGet()
    {
        PosList = _posService.GetAll().ToList();
    }
}


