namespace Formulario_Cartagena.Lector_Document
{
    using Xceed.Words.NET;
    using System.Collections.Generic;
    using System.IO;
    public static class Remplazar_Campos
    {
        public static void ReplaceTextInWord(string docPath, List<string> data)
        {
            // Asegúrate de que tenemos suficientes datos para reemplazar
            if (data == null || data.Count < 5)
            {
                throw new ArgumentException("No hay suficientes datos para reemplazar en el documento.");
            }

            // Crea una copia temporal del documento
            string tempPath = Path.GetTempFileName();
            File.Copy(docPath, tempPath, true);

            using (var doc = DocX.Load(tempPath))
            {
                // Definir los reemplazos
                var replacements = new Dictionary<string, string>
                {
                    {"«DENOMINACIÓN_DE_LA_PROPIEDADHORIZONTAL»", data[1]},
                    {"«DIRECCION_DE_LA_COPROPIEDAD__CONFORME_A»", data[3]},
                    {"«NITDE_COPROPIEDAD»", data[2]},
                    {"«NUMERO_DE_RESOLUCIÓN_INSCRIBE_COPROPIEDA»", data[5]},
                    {"«FECHA_DE_RESOLUCIÓN_DE_INSCRIPCION_DE_CO»", data[6]},
                    {"«QUIEN_EXPIDIO_LA_RESOLUCION_DE_INSCRIPCI»", data[7]},
                    {"«NOMBRE_ULTIMO_ADIMINISTRADOR_YO_REPRESE»", data[12]},
                    {"«NUMERO_DE_IDENTIFICACIÓN_DEL_ÚLTIMO_ADMI»", data[14]}
                    // Añade más reemplazos según sea necesario
                };

                // Realizar los reemplazos
                foreach (var replacement in replacements)
                {
                    doc.ReplaceText(replacement.Key, replacement.Value);
                }
                string wordPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "prueba.docx");
                // Guardar el documento
                doc.SaveAs(wordPath);
            }

            // Eliminar el archivo temporal
            File.Delete(tempPath);
        }
    }
}



