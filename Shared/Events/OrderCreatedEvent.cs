﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderCreatedEvent(int orderId, string customerName, decimal totalAmount)
        {
            OrderId = orderId;
            CustomerName = customerName;
            TotalAmount = totalAmount;
        }
    }
}

