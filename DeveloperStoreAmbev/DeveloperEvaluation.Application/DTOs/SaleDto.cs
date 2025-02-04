using System;
using System.Collections.Generic;

namespace DeveloperEvaluation.Application.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> Items { get; set; } = new(); 
        public bool IsCancelled { get; set; }
    }
}
