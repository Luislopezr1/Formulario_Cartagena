namespace Formulario_Cartagena.Lector_Document
{
	public class Lector_CC
	{
		public static string verificarCedula(string cedula)
		{
			string numero = "";
            for (int i = 0; i < cedula.Length; i++)
            {
				if (char.IsDigit(cedula[i]))
				{
					numero += cedula[i];
				}
            }
            return numero;
		}
	}
}
