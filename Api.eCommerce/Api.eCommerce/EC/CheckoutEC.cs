using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Api.eCommerce.Database;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Spring2025_Samples.Models;
namespace Api.eCommerce.EC
{
    public class CheckoutEC
    {
        public List<Item> GetCart()
        {
            return ShoppingCartService.Current.CartItems;
        }

        public decimal GetCartSubtotal()
        {
            return ShoppingCartService.Current.CartSubtotal;
        }

        public decimal CalculateTotal(decimal taxRate)
        {
            return ShoppingCartService.Current.CalculateTotal(taxRate);
        }

        public decimal ProcessCheckout(decimal taxRate)
        {
            return ShoppingCartService.Current.Checkout(taxRate);
        }
    }
}