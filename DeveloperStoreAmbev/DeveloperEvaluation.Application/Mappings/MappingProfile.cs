using AutoMapper;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map SaleDto to Sale, ensuring correct instantiation
        CreateMap<SaleDto, Sale>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ConstructUsing(src => new Sale(src.CustomerId, new List<SaleItem>()));

        // Map Sale to SaleDto, ensuring proper mapping of items
        CreateMap<Sale, SaleDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        // Map SaleItemDto to SaleItem and vice versa
        CreateMap<SaleItemDto, SaleItem>()
            .ReverseMap()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
    }
}
