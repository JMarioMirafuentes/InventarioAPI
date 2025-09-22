using AutoMapper;
using InventarioAPI.Dtos;
using InventarioAPI.Helpers.Exceptions;
using InventarioAPI.Helpers.Utils;
using InventarioAPI.Models;
using InventarioAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    /// <summary>
    /// Manejo de Alimentos y Bebidas en el Inventario.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentosBebidasController : ControllerBase
    {
        private readonly AlimentoBebidaServicio _alimentoBebidaServicio;
        private readonly ILogger<AlimentosBebidasController> _logger;
        private readonly IMapper _mapper;

        public AlimentosBebidasController(AlimentoBebidaServicio alimentoBebidaServicio, ILogger<AlimentosBebidasController> logger, IMapper mapper)
        {
            _alimentoBebidaServicio = alimentoBebidaServicio;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los alimentos y bebidas del inventario.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AlimentoBebidaDTO>>>> All()
        {
            try
            {
                _logger.LogInformation("Se solicitó obtener todos los alimentos y bebidas.");
                var lista = await _alimentoBebidaServicio.All();
                return Ok(new ApiResponse<IEnumerable<AlimentoBebidaDTO>>(200, "OK", true, lista));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener todos los alimentos y bebidas.");
                return BadRequest(new ApiResponse<IEnumerable<AlimentoBebidaDTO>>(500, "Error inesperado", false, null));
            }
        }

        /// <summary>
        /// Obtiene un alimento o bebida por su ID.
        /// </summary>
        /// <param name="idAlimentoBebida">Identificador del alimendo o bebida</param>
        /// <returns>Objeto dto</returns>
        [HttpGet("{idAlimentoBebida}")]
        public async Task<ActionResult<ApiResponse<AlimentoBebidaDTO>>> GetId(int idAlimentoBebida)
        {
            try
            {
                _logger.LogInformation("Se solicitó obtener el alimento/bebida con ID {idAlimentoBebida}", idAlimentoBebida);
                var item = await _alimentoBebidaServicio.GetId(idAlimentoBebida);
                return Ok(new ApiResponse<AlimentoBebidaDTO>(200, "OK", true, item));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al obtener alimento/bebida por ID.");
                return Ok(new ApiResponse<AlimentoBebidaDTO>(202, ex.Message, false, null));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning(ex, "No se encontró alimento/bebida con ID {idAlimentoBebida}", idAlimentoBebida);
                return Ok(new ApiResponse<AlimentoBebidaDTO>(202, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener alimento/bebida por ID.");
                return BadRequest(new ApiResponse<AlimentoBebidaDTO>(500, "Error inesperado", false, null));
            }
        }

        /// <summary>
        /// Guarda un nuevo alimento o bebida.
        /// </summary>
        /// <param name="dto">Objeto nuevo de alimento o bebida</param>
        /// <returns>Objeto dto</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AlimentoBebidaDTO>>> post([FromBody] AlimentoBebidaDTO dto)
        {
            try
            {
                _logger.LogInformation("Se solicitó guardar un nuevo alimento/bebida: {Nombre}", dto.Nombre);
                var nuevo = await _alimentoBebidaServicio.Save(_mapper.Map<AlimentoBebida>(dto));
                return Ok(new ApiResponse<AlimentoBebidaDTO>(200, "Guardado correctamente", true, nuevo));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al guardar alimento/bebida.");
                return Ok(new ApiResponse<AlimentoBebidaDTO>(202, ex.Message, false, null));
            }
            catch (AlreadyExistsException ex)
            {
                _logger.LogWarning(ex, "Intento de guardar alimento/bebida que ya existe: {Nombre}", dto.Nombre);
                return Ok(new ApiResponse<AlimentoBebidaDTO>(202, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al guardar alimento/bebida.");
                return BadRequest(new ApiResponse<AlimentoBebidaDTO>(500, "Error inesperado", false, null));
            }
        }

        /// <summary>
        /// Actualiza un alimento o bebida existente.
        /// </summary>
        /// /// <param name="dto">Objeto nuevo de alimento o bebida</param>
        /// <returns>Objeto dto</returns>
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> put([FromBody] AlimentoBebidaDTO dto)
        {
            try
            {
                _logger.LogInformation("Se solicitó actualizar alimento/bebida con ID {Id}", dto.Id);
                var resultado = await _alimentoBebidaServicio.Update(_mapper.Map<AlimentoBebida>(dto));
                return Ok(new ApiResponse<bool>(200, "Actualizado correctamente", true, resultado));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar alimento/bebida.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning(ex, "No se encontró el alimento/bebida para actualizar.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar alimento/bebida.");
                return BadRequest(new ApiResponse<bool>(500, "Error inesperado", false, false));
            }
        }

        /// <summary>
        /// Elimina un alimento o bebida por su ID.
        /// </summary>
        /// <param name="idAlimentoBebida">Identificador del alimendo o bebida</param>
        /// <returns>Resultado del método</returns>
        [HttpDelete("{idAlimentoBebida}")]
        public async Task<ActionResult<ApiResponse<bool>>> delete(int idAlimentoBebida)
        {
            try
            {
                _logger.LogInformation("Se solicitó eliminar alimento/bebida con ID {idAlimentoBebida}", idAlimentoBebida);
                var resultado = await _alimentoBebidaServicio.Delete(idAlimentoBebida);
                return Ok(new ApiResponse<bool>(200, "Eliminado correctamente", true, resultado));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar alimento/bebida.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning(ex, "No se encontró el alimento/bebida para eliminar.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar alimento/bebida.");
                return BadRequest(new ApiResponse<bool>(500, "Error inesperado", false, false));
            }
        }

        /// <summary>
        /// Cambia el estatus de un alimento o bebida por su ID.
        /// </summary>
        /// <param name="dto">Objeto para actualizar estatus</param>
        /// <returns>Resultado del método</returns>
        [HttpPatch("CambiarEstatus")]
        public async Task<ActionResult<ApiResponse<bool>>> Change([FromBody] AlimentoBebidaDTO dto)
        {
            try
            {
                _logger.LogInformation("Se solicitó cambiar estatus del alimento/bebida con ID {idAlimentoBebida} a {nuevoEstatus}", dto.Id, dto.Estatus);
                var resultado = await _alimentoBebidaServicio.Change(dto.Id, dto.Estatus);
                return Ok(new ApiResponse<bool>(200, "Estatus actualizado correctamente", true, resultado));
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al cambiar estatus.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning(ex, "No se encontró el alimento/bebida para cambiar estatus.");
                return Ok(new ApiResponse<bool>(202, ex.Message, false, false));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al cambiar estatus del alimento/bebida.");
                return BadRequest(new ApiResponse<bool>(500, "Error inesperado", false, false));
            }
        }
    }
}
