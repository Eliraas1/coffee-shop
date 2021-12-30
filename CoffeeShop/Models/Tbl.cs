using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.Models
{
    public class Tbl
    {
        [Key]
        [Column(Order = 0)]
        public int tid { get; set; }

        public int amount { get; set; }

        public bool isIn { get; set; }

        public int taken { get; set; }

        public List<user> user { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Date { get; set; }

        public int OpenSeats()
        {
            return amount - taken;
        }
        public Tbl()
        {
            Date = DateTime.Now;
        }
        public Tbl(int am,bool In, int take)
        {
            Date = DateTime.Now;
            amount = am;
            isIn = In;
            taken = take;
        }
    }
}