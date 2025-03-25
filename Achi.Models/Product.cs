using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Achi.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Kitap Adı")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [Display(Name = "Yazar")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Satış Fiyatı")]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Perakende Fiyatı")]
        
        public double Price { get; set; }

        [Required]
        [Display(Name = "50+ Toptan Fiyatı")]
        
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "100+ Toptan Fiyatı")]
        
        public double Price100 { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }

        
        public string Image { get; set; }

    }
}
