using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Events;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeveloperEvaluation.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public SaleRepository(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales.Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }



        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales.Include(s => s.Items).ToListAsync();
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();

            await _mediator.Publish(new SaleModifiedEvent(sale.Id));
        }

        public async Task CancelSaleAsync(Guid saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                sale.Cancel();
                await _context.SaveChangesAsync();

                await _mediator.Publish(new SaleCancelledEvent(sale.Id));
            }
        }

        public async Task CancelItemAsync(Guid saleId, Guid productId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale != null)
            {
                var item = sale.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    sale.Items.Remove(item);
                    await _context.SaveChangesAsync();

                    await _mediator.Publish(new ItemCancelledEvent(sale.Id, productId));
                }
            }
        }


        public async Task DeleteAsync(Guid id)
        {
            var sale = await GetByIdAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}
