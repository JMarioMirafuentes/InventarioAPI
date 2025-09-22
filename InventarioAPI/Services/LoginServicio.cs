using AutoMapper;
using InventarioAPI.Dtos;
using InventarioAPI.Helpers.Exceptions;
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
        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginDTO dto)
        {
            var usuario = await _inventarioDbContext.GetUsuarioPorLogin(dto.Login).FirstOrDefaultAsync();          
            if (usuario == null)
                throw new DataNotFoundException("Usuario no encontrado.");
            bool valido = PasswordHelper.VerifyPassword(dto.Contrasenia, usuario.ContraseniaHash, usuario.ContraseniaSalt);
            return valido;
        }
        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> CrearUsuarioAsync(LoginDTO dto)
        {

            var existeUsuario = await _inventarioDbContext.Usuarios
                .AnyAsync(u => u.Login == dto.Login);

            if (existeUsuario)
                throw new Exception("El usuario ya existe.");
            dto.Contrasenia = "1234";

            PasswordHelper.CreatePasswordHash(dto.Contrasenia, out string hash, out string salt);

            //var usuario = new Usuario
            //{
            //    Id = 0,
            //    Login = dto.Login,
            //    ContraseniaHash = hash,
            //    ContraseniaSalt = salt,
              
            //}; 
            var admin = new Usuario
            {
                Id = 0,
                Login = dto.Login,
                Nombre= "Administrador",
                ContraseniaHash = hash,
                ContraseniaSalt = salt,
              
            };

            _inventarioDbContext.Usuarios.Add(admin);
            await _inventarioDbContext.SaveChangesAsync();

            return true;
        }

    }
}
