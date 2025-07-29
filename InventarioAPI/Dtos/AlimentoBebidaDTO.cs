using System.ComponentModel.DataAnnotations;

namespace InventarioAPI.Dtos
{

    public class AlimentoBebidaDTO
    {
        /// <summary>
        /// Identificador  único del alimento o bebida
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del alimento o bebida
        /// </summary>
        [Required]
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del alimento o bebida
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estatus del alimento o bebida
        /// </summary>
        public bool Estatus { get; set; } 
    }
}
