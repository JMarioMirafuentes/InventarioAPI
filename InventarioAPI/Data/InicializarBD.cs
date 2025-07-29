using InventarioAPI.Models;

namespace InventarioAPI.Data
{
    public class InicializarBD
    {
        public static void Initialize(IventarioDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.AlimentosBebidas.Any())
            {
                return;
            }

            context.AlimentosBebidas.AddRange(
                new AlimentoBebida { Id = 1, Nombre = "Pizza italiana", Descripcion = "Grande 8 rebanadas", Estatus = AlimentoBebeidaEstatus.Activo },
                new AlimentoBebida { Id = 2, Nombre = "Jugo naranja", Descripcion = "600 ml", Estatus = AlimentoBebeidaEstatus.Inactivo },
                new AlimentoBebida { Id = 3, Nombre = "Coca cola", Descripcion = "600 ml", Estatus = AlimentoBebeidaEstatus.Activo },
                new AlimentoBebida { Id = 4, Nombre = "Hamburguesa con queso", Descripcion = "Sencilla", Estatus = AlimentoBebeidaEstatus.Inactivo }
            );

            context.SaveChanges();
        }
    }
}
