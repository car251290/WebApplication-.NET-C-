using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HplusSport.Api.Models;

namespace HplusSportApi.Models
{
    public class product
    {
        public int Id { get; set; }

        public string Sku { get; set; }
        //for make the product is necessary
        [Required]
        public string Name { get; set; }
        //for make a restriction with the strings
        [MaxLength(255)]
        public string Description { get; set;}

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }
      
        public int CategoryId { get; set; }
        [JsonIgnore]

        public virtual Category Category { get; set; }


    }
}
