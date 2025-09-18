using AutoMapper;
using InventarioAPI.Models;

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

    }
}
