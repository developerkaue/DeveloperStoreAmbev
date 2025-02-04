using System.Collections.Generic;
using MediatR;
using DeveloperEvaluation.Application.DTOs;

namespace DeveloperEvaluation.Application.Features.Sales.Queries
{
    public class GetAllSalesQuery : IRequest<List<SaleDto>> { }
}
