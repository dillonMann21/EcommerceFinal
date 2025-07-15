using Api.eCommerce.EC;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Microsoft.AspNetCore.Mvc;
using Spring2025_Samples.Models;
using System.Security.Cryptography.Xml;
namespace Library.eCommerce.Models

{
    public class TaxCalculationResult
    {
        public decimal Subtotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        public TaxCalculationResult(decimal subtotal, decimal taxRate)
        {
            Subtotal = subtotal;
            TaxRate = taxRate;
            TaxAmount = subtotal * (taxRate / 100);
            Total = Subtotal + TaxAmount;
        }
    }
}