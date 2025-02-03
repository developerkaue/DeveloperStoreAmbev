﻿using MediatR;
using System;

namespace DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : INotification
    {
        public Guid SaleId { get; }

        public SaleCancelledEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
