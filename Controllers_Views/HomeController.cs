using Formulario_Cartagena.Lector_Document;
using Formulario_Cartagena.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Formulario_Cartagena.Controllers_Views
{
    [Route("Home")]
    public class HomeController : Controller
    {
        // Variable para almacenar los datos
        private static Propiedad datosGuardados;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GuardarDatos")]
        public IActionResult GuardarDatos([FromForm] Propiedad model)
        {

            if (ModelState.IsValid)
            {
                // Guardar los datos en la variable
                datosGuardados = model;

                // Devolver los datos como JSON para que se muestren en la vista sin recargar la página
                return Integrar();
            }

            // Si el modelo no es válido, devolver un error
            return BadRequest("No valido");
        }

        public IActionResult DatosGuardados()
        {
            // Mostrar los datos guardados (puedes pasar los datos al modelo si lo prefieres)
            return View(datosGuardados);
        }

        public JsonResult Integrar()
        {
            string excelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "excel_Datos.xlsx");
            string wordPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Certificado.docx");

            string nit = datosGuardados.Nit;
            // Obtener datos de la fila del Excel
            List<string> data = Lector_Excel.ReadExcelByNIT(excelPath, nit);

            if (data != null)
            {
                try
                {
                    Remplazar_Campos.ReplaceTextInWord(wordPath, data);
                    List<string> prev = new List<string>();
                    prev.Add(data[1]);
                    prev.Add(data[3]);
                    prev.Add(data[12]);
                    //data = new List<string>();
                    return Json(prev);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al reemplazar datos en el documento Word: " + ex.Message });
                }
            }
            else
            {
                return Json(new { error = "No se encontró la fila con el NIT proporcionado." });
            }
        }
    }
}
