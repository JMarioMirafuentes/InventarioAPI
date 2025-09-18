namespace InventarioAPI.Models
{
    /// <summary>
    /// Enum de los estatus del alimento o bebida
    /// </summary>
    public class AlimentoBebida
    {
        /// <summary>
        /// Identificador  único del alimento o bebida
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del alimento o bebida
        /// </summary>
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
