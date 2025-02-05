using System;

namespace DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        private decimal _total;

        public decimal Total
        {
            get => Quantity * UnitPrice; 
            private set => _total = value; // Setter privado para o EF Core
        }

        // Construtor padrão para evitar erro no AutoMapper
        private SaleItem() { }

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            _total = Total; 
        }

        public void ApplyDiscount()
        {
            if (Quantity >= 10) Discount = (UnitPrice * Quantity) * 0.2m;
            else if (Quantity >= 4) Discount = (UnitPrice * Quantity) * 0.1m;
        }
    }
}
