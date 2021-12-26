using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models
{
    public class coffee
    {

        [Key]
        public string id { get; set; }

        public string name { get; set; }

        public string img { get; set; }
        public string price { get; set; }
    }
}