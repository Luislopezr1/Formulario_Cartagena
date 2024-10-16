namespace Formulario_Cartagena.Lector_Document
{
    using OfficeOpenXml;
    using System.ComponentModel;
    using System.IO;

    public class Lector_Excel
    {
        public static List<string> ReadExcelByNIT(string filePath, string nit)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            List<string> result = new List<string>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[1]; // Asume que queremos la primera hoja

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Asume que la primera fila es de encabezados
                {
                    string currentNIT = worksheet.Cells[row, 3].Value?.ToString(); // Asume que el NIT está en la primera columna

                    if (currentNIT == nit)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            string cellValue = worksheet.Cells[row, col].Value?.ToString() ?? string.Empty;
                            result.Add(cellValue);
                        }
                        break; // Detiene la búsqueda después de encontrar el NIT
                    }
                }
            }

            return result;
        }
    }

}
