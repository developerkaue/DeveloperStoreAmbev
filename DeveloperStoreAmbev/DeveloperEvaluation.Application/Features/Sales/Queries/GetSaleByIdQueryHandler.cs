using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Application.DTOs;

namespace DeveloperEvaluation.Application.Features.Sales.Queries
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, SaleDto>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            return _mapper.Map<SaleDto>(sale);
        }
    }
}
