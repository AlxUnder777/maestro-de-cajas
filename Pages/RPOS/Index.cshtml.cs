using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MaestroDeCajasWeb.Pages.RPOS;

public class IndexModel : PageModel
{
    private readonly RposServiceWeb _rposService;

    public IndexModel(RposServiceWeb rposService)
    {
        _rposService = rposService;
    }

    public IList<MaestroDeCajas.Models.Rpos> RposList { get; set; } = new List<MaestroDeCajas.Models.Rpos>();

    public void OnGet()
    {
        RposList = _rposService.GetAll().ToList();
    }
}


