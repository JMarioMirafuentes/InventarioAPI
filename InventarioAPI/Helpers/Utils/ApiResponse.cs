namespace InventarioAPI.Helpers.Utils
{
    /// <summary>
    /// Clase genérica para encapsular respuestas de API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Numero de código  de la respuesta
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Mensaje emitido del response
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Resultado del método
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Datos obtenidos del proceso
        /// </summary>
        public T? Data { get; set; } = default;
        public ApiResponse()
        {
            StatusCode = 200;
            Message = string.Empty;
            Success = true;
        }
        public ApiResponse(int statusCode, string message, bool success, T data)
        {
            StatusCode = statusCode;
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
