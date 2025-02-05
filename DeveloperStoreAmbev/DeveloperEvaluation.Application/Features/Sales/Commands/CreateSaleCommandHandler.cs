using AutoMapper;
using DeveloperEvaluation.Application.Events;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.Data.Entity.Infrastructure;

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator; 
    }

    public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("O campo 'customerId' não pode estar vazio.");

           
            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("A venda deve conter pelo menos um item.");

            foreach (var item in request.Items)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException($"Quantidade inválida para o produto {item.ProductId}. Deve ser maior que zero.");

                if (item.UnitPrice < 0)
                    throw new ArgumentException($"Preço inválido para o produto {item.ProductId}. O valor deve ser maior ou igual a zero.");
            }

            // Criando a venda
            var saleItems = _mapper.Map<List<SaleItem>>(request.Items);
            var sale = new Sale(request.CustomerId, saleItems);

            await _saleRepository.AddAsync(sale);
            await _mediator.Publish(new SaleCreatedEvent(sale), cancellationToken);

            return sale.Id;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"[ERROR] DbUpdateException: {ex.InnerException?.Message ?? ex.Message}");
            throw new Exception($"Erro ao salvar no banco de dados: {ex.InnerException?.Message ?? ex.Message}", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Exception: {ex.InnerException?.Message ?? ex.Message}");
            throw new Exception($"Erro inesperado ao processar a venda: {ex.InnerException?.Message ?? ex.Message}", ex);
        }
    }




}
