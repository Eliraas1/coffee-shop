using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class user
    {
        public string name { get; set; }
        
        [Key]
        public string email { get; set; }

        public string password { get; set; }
        public string role { get; set; }

    }
}