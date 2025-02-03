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
        public decimal TotalAmount => Items.Sum(i => i.Total);
        public List<SaleItem> Items { get; private set; } = new List<SaleItem>();
        public bool IsCancelled { get; private set; }

        // 🔹 Construtor vazio necessário para o EF Core
        private Sale() { }

        public Sale(Guid customerId, List<SaleItem> items)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            CustomerId = customerId;
            Items = items ?? new List<SaleItem>();
            ApplyDiscounts();
        }

        private void ApplyDiscounts()
        {
            foreach (var item in Items)
            {
                item.ApplyDiscount();
            }
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }
    }
}
