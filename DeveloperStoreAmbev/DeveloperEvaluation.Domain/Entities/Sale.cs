using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<SaleItem> Items { get; private set; }
        public bool IsCancelled { get; private set; }

        
        public decimal TotalAmount => Items.Sum(item => item.Total);

        private Sale() { }

        public Sale(Guid customerId, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            CustomerId = customerId;
            Items = items ?? new List<SaleItem>(); 
            IsCancelled = false;
        }

        public void UpdateItems(List<SaleItem> updatedItems)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Não é possível modificar uma venda cancelada.");
            Items = updatedItems;
        }

        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("A venda já está cancelada.");
            IsCancelled = true;
        }
    }
}
