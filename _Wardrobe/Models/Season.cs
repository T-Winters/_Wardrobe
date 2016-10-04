using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Wardrobe.Models
{
    public class Season
    {
        public int SeasonID { get; set; }
        public string SeasonName { get; set; }

        public virtual IEnumerable<Top> Top { get; set; }
        public virtual IEnumerable<Bottom> Bottom { get; set; }
        public virtual IEnumerable<Shoe> Shoe { get; set; }
        public virtual IEnumerable<Accessory> Accessory { get; set; }
    }
}