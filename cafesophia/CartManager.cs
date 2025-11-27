using System;
using System.Collections.Generic;
using System.Linq;

namespace cafesophia
{
    public class CartManager
    {
        private readonly List<CartItem> _items = new List<CartItem>();

        public event EventHandler CartChanged;

        public List<CartItem> Items
        {
            get { return _items; }
        }

        public int Count => _items.Count;

        public void AddItem(int itemId, string name, decimal price, int stock)
        {
            var existing = _items.FirstOrDefault(x => x.ItemId == itemId);
            if (existing != null)
            {
                if (existing.Quantity + 1 > existing.Stock)
                    return; // silently ignore (UI level shows message)
                existing.Quantity += 1;
            }
            else
            {
                if (stock <= 0)
                    return;
                _items.Add(new CartItem { ItemId = itemId, ItemName = name, UnitPrice = price, Quantity = 1, Stock = stock });
            }
            OnChanged();
        }

        public void IncreaseQuantity(int itemId)
        {
            var it = _items.FirstOrDefault(x => x.ItemId == itemId);
            if (it == null) return;
            if (it.Quantity + 1 > it.Stock) return;
            it.Quantity += 1;
            OnChanged();
        }

        public void DecreaseQuantity(int itemId)
        {
            var it = _items.FirstOrDefault(x => x.ItemId == itemId);
            if (it == null) return;
            it.Quantity -= 1;
            if (it.Quantity <= 0) _items.Remove(it);
            OnChanged();
        }

        public void RemoveItem(int itemId)
        {
            var it = _items.FirstOrDefault(x => x.ItemId == itemId);
            if (it != null)
            {
                _items.Remove(it);
                OnChanged();
            }
        }

        public void Clear()
        {
            _items.Clear();
            OnChanged();
        }

        public decimal CalculateSubtotal()
        {
            return _items.Sum(i => i.Subtotal);
        }

        public List<CartItem> CloneCart()
        {
            return _items.Select(i => i.Clone()).ToList();
        }

        protected void OnChanged()
        {
            CartChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
