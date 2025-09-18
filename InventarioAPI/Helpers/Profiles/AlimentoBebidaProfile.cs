using AutoMapper;
using InventarioAPI.Dtos;
using InventarioAPI.Models;

namespace InventarioAPI.Helpers.Profiles
{
    /// <summary>
    /// AutoMaper profile for mapping between AlimentoBebida and AlimentoBebidaDTO.
    /// </summary>
    public class AlimentoBebidaProfile : Profile
    {
        public AlimentoBebidaProfile()
        {
            CreateMap<AlimentoBebida, AlimentoBebidaDTO>().ReverseMap();


        }
    }
}
