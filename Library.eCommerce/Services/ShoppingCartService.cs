﻿using Library.eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;
        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
        public decimal CartSubtotal 
        {
            get {
                return CartItems.Sum(item => item.Subtotal);
            }
        }
        public static ShoppingCartService Current {  
            get
            {
                if(instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            } 
        }
        private static ShoppingCartService? instance;
        private ShoppingCartService() { 
            items = new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            var existingInvItem = _prodSvc.GetById(item.Id);
            if(existingInvItem == null || existingInvItem.Quantity == 0) {
                return null;
            }

            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }

            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem == null)
            {
                //add
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            } else
            {
                //update
                existingItem.Quantity++;
            }


            return existingInvItem;
        }

        public Item? ReturnItem(Item? item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }

            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id); ;
                if(inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                } else
                {
                    inventoryItem.Quantity++;
                }
            }


            return itemToReturn;
        }
        public decimal CalculateTotal(decimal taxRate)
        {
            decimal subtotal = CartSubtotal;
            decimal taxAmount = subtotal * (taxRate / 100);
            return subtotal + taxAmount;
        }

        public decimal Checkout(decimal taxRate) // clear cart and return final price
        {
            if (!CartItems.Any())
            {
                throw new InvalidOperationException("Cart is empty silly goose");
            }
            decimal total = CalculateTotal(taxRate);
            items.Clear();
            return total;

        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
}
