using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.Application.Features.Sales.Commands
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null || sale.IsCancelled) return false;

            sale.Cancel();
            await _saleRepository.UpdateAsync(sale);
            return true;
        }
    }
}
