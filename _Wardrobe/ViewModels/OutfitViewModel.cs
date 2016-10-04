using _Wardrobe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Wardrobe.ViewModels
{
    public class OutfitViewModel
    {
        // Created to help allow choosing multiple accessories rather than doing it with a viewbag
        public Outfit Outfit { get; set; }
        public IEnumerable<SelectListItem> AllAccessories { get; set; }

        private List<int> _selectedAccessories;
        public List<int> SelectedAccessories
        {
            get
            {
                if (_selectedAccessories == null)
                {
                    _selectedAccessories = (from a in Outfit.Accessories
                                            select a.AccessoryID).ToList();
                }
                return _selectedAccessories;
            }
            set { _selectedAccessories = value; }
        }
    }
}