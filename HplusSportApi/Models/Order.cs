
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace HplusSportApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime orderData { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]

        public virtual Users User { get; set; }

        public virtual List<product> Products { get; set; }
    }
}



