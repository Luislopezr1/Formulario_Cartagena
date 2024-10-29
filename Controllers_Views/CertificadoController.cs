using Formulario_Cartagena.Lector_Document;
using Formulario_Cartagena.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Formulario_Cartagena.Controllers_Views
{
    [Route("Home")]
    public class CertificadoController : Controller
    {
        // Variable para almacenar los datos
        private static Propiedad datosGuardados;

        [HttpGet]
        public IActionResult Certificado()
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
			string cedula = datosGuardados.Cc;
			// Obtener datos de la fila del Excel
			List<string> data = Lector_Excel.ReadExcelByNIT(excelPath, nit);
            string cedula_bd = Lector_CC.verificarCedula(data[14]);
            if (data != null)
            {
                if (cedula == cedula_bd)
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
						return Json(new { success = false, error = "Error al reemplazar datos en el documento Word: " + ex.Message });
					}
                }
                else
                {
					return Json(new { success = false, error = "La cedula no coincide "+cedula_bd});
				}
            }
            else
            {
                return Json(new { success = false, error = "No se encontró la fila con el NIT proporcionado." });
            }
        }
    }
}
