using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Domain.Entities;

namespace DeveloperEvaluation.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaleItemDto, SaleItem>();
        }
    }
}
