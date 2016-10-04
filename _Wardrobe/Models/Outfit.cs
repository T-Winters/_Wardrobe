using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _Wardrobe.Models
{
    public class Outfit
    {
        public Outfit()
        {
            this.Accessories = new HashSet<Accessory>();
        }
        public int OutfitId { get; set; }
        public string OutfitName { get; set; }
        public int? TopId { get; set; }
        public int? BottomId { get; set; }
        public int? ShoeId { get; set; }

        public virtual Top Top { get; set; }
        public virtual Bottom Bottom { get; set; }
        public virtual Shoe Shoe { get; set; }

        //sets many to many relationship
        public virtual ICollection<Accessory> Accessories { get; set; }
    }
}
