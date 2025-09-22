using AutoMapper;
using InventarioAPI.Dtos;
using InventarioAPI.Helpers.Exceptions;
using InventarioAPI.Helpers.Utils;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginServicio _loginServicio;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(LoginServicio loginServicio, ILogger<LoginController> logger, IMapper mapper)
        {
            _loginServicio = loginServicio;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<bool>> Login([FromBody] LoginDTO dto)
        {
            try
            {
                bool valido = await _loginServicio.Login(dto);
                return Ok(new ApiResponse<bool>(200, "OK", true, valido));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning(ex, "Intento de login fallido. Usuario: {Login}", dto.Login);
                return Ok(new ApiResponse<bool>(200, ex.Message, false, false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Login para usuario: {Login}", dto.Login);
                return Ok(new ApiResponse<bool>(200, "Error inesperado", false, false));
            }
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("crear")]
        public async Task<ActionResult<bool>> Crear([FromBody] LoginDTO dto)
        {
            try
            {
                bool creado = await _loginServicio.CrearUsuarioAsync(dto);
                return Ok(new ApiResponse<bool>(200, "OK", true, creado));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario: {Login}", dto.Login);
                return Ok(new ApiResponse<bool>(200, ex.Message, false, false));
            }
        }
    }
}
