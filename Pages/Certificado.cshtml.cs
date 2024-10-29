using Formulario_Cartagena.Controllers_Views;
using Formulario_Cartagena.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace Formulario_Cartagena.Pages
{
    public class CertificadoModel : PageModel
    {
        private readonly ILogger<CertificadoModel> _logger;

        [BindProperty]
        public Propiedad Propiedad { get; set; }

        public CertificadoModel(ILogger<CertificadoModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
