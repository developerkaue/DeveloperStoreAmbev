﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DeveloperEvaluation.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
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
