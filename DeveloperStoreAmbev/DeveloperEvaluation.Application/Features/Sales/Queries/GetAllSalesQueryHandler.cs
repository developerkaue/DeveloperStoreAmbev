using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Application.Features.Sales.Queries;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;

public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SaleDto>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetAllSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<List<SaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync();
        return _mapper.Map<List<SaleDto>>(sales); 
    }
}
