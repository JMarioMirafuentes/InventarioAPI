using AutoMapper;
using InventarioAPI.Dtos;
using InventarioAPI.Helpers.Utils;
using InventarioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioAPI.Services
{
    /// <summary>
    /// Sewrvicio para manejar operaciones de Login.
    /// </summary>
    public class LoginServicio
    {
        private readonly InventarioDbContext _inventarioDbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor del servicio de Login.
        /// </summary>
        /// <param name="inventarioDbContext"></param>
        /// <param name="mapper"></param>
        public LoginServicio(InventarioDbContext inventarioDbContext, IMapper mapper)
        {
            _inventarioDbContext = inventarioDbContext;
            _mapper = mapper;
        }
        public async Task<bool> ActualizarAlimentoBebida(LoginDTO dto)
        {
            var usuario = await _inventarioDbContext
           .GetUsuarioPorLogin(dto.Login)   
           .FirstOrDefaultAsync();          
            if (usuario == null)
                return false;
            bool valido = PasswordHelper.VerifyPassword(dto.Contrasenia, usuario.ContraseniaHash, usuario.ContraseniaSalt);
            return valido;
        }
    }
}
