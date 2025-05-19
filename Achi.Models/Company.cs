using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Achi.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        public string Name { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }

        [ValidateNever]
        public City City { get; set; }

        [ValidateNever]
        public Country Country { get; set; }

        [ValidateNever]
        public State State { get; set; }
    }
}
