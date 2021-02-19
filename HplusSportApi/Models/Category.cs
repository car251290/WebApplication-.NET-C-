using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HplusSportApi.Models;

namespace HplusSport.Api.Models
{
    public class Category

    {
        //to get the request on get and set
        public int Id { get;set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public virtual List <product> Products { get; set; }
    }
}
