using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _Wardrobe.Models
{
    public class Shoe
    {
        public int ShoeID { get; set; }
        public string ShoeName { get; set; }
        public string Photo { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Occasion { get; set; }
        public int SeasonID { get; set; }


        public virtual Season Season { get; set; }

        public virtual IEnumerable<Outfit> Outfit { get; set; }
    }
}