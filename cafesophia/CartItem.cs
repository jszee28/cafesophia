using System;

namespace cafesophia
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }

        public decimal Subtotal
        {
            get { return UnitPrice * Quantity; }
        }

        public CartItem Clone()
        {
            return new CartItem
            {
                ItemId = this.ItemId,
                ItemName = this.ItemName,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity,
                Stock = this.Stock
            };
        }
    }
}
