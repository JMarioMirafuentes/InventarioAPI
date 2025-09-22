using InventarioAPI.Dtos;
using InventarioAPI.Models;

namespace InventarioAPI.Helpers.Interfaces
{
    /// <summary>
    /// Interface para manejar operaciones CRUD de Alimentos y Bebidas.
    /// </summary>
    public interface IAlimentoBebida
    {
        Task<IEnumerable<AlimentoBebidaDTO>> All();
        Task<AlimentoBebidaDTO?> GetId(int id);
        Task<AlimentoBebidaDTO> Save(AlimentoBebida dto);
        Task<bool> Update(AlimentoBebida dto);
        Task<bool> Delete(int id);
        Task<bool> Change(int id, bool nuevoEstatu);
    }
}
