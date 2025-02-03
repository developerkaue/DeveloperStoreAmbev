using System;

namespace DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }

        // 🔹 Campo privado para o EF Core
        private decimal _total;

        public decimal Total
        {
            get => (UnitPrice * Quantity) - Discount;
            private set => _total = value; // EF Core precisa de um setter para mapear
        }

        private SaleItem() { } // 🔹 Construtor privado para EF Core

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = 0;
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 10) Discount = (UnitPrice * Quantity) * 0.2m;
            else if (Quantity >= 4) Discount = (UnitPrice * Quantity) * 0.1m;
        }
    }
}
