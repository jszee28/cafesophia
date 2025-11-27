using System;
using System.Collections.Generic;

namespace cafesophia
{
    public class ReceiptData
    {
        public List<CartItem> Items { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Change { get; set; }
        public DateTime Date { get; set; }

        // New: include order type/service type so receipts can show it
        public string OrderType { get; set; }
    }
}
