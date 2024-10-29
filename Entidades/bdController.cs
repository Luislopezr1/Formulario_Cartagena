using Microsoft.EntityFrameworkCore;

namespace Formulario_Cartagena.Entidades
{
    public class bdController : DbContext
    {
        public DbSet<Conector> Ciudadanos { get; set; }

        public bdController(DbContextOptions<bdController> options)
            : base(options)
        {
        }
    }


    public class Conector
    {
        public string DenominacionPropiedad { get; set; }
        public string DireccionCopropiedad { get; set; }
        public string Nit { get; set; }
        public string NumeroResolucion { get; set; }
        public string FechaResolucion { get; set; }
        public string QuienExpidio { get; set; }
        public string cargo_pj { get; set; }
        public string nombre_pj { get; set; }
        public string vocativo { get; set; }
        public string Nit_pj { get; set; }
        public string tipo_documento { get; set; }
        public string Numero_CC { get; set; }

        public string lugar_expedicion { get; set; }

        public string codigo_auto { get; set; }
        public string fecha_resolucion { get; set; }


    }
}
