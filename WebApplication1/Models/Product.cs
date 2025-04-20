using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }
    }
}
