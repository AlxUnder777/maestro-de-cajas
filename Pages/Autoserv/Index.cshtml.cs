using MaestroDeCajasWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MaestroDeCajas.Models;

namespace MaestroDeCajasWeb.Pages.Autoserv
{
    public class IndexModel : PageModel
    {
        private readonly AutoservicioServiceWeb _service;

        public IndexModel(AutoservicioServiceWeb service)
        {
            _service = service;
        }

        public IList<Autoservicio> Lista { get; set; } = new List<Autoservicio>();

        public void OnGet()
        {
            Lista = _service.GetAll().ToList();
        }
    }
}
