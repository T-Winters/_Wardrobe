using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _Wardrobe.Models;
using _Wardrobe.ViewModels;

namespace _Wardrobe.Controllers
{
    public class OutfitsController : Controller
    {
        private _WardrobeContext db = new _WardrobeContext();

        // GET: Outfits
        public ActionResult Index()
        {
            var outfits = db.Outfits.Include(o => o.Bottom).Include(o => o.Shoe).Include(o => o.Top);
            return View(outfits.ToList());
        }

        // GET: Outfits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // GET: Outfits/Create
        public ActionResult Create()
        {
            Outfit outfit = new Outfit();
            if (outfit == null)
            {
                return HttpNotFound();
            }

            ViewBag.BottomId = new SelectList(db.Bottoms, "BottomID", "BottomName");
            ViewBag.ShoeId = new SelectList(db.Shoes, "ShoeID", "ShoeName");
            ViewBag.TopId = new SelectList(db.Tops, "TopID", "TopName");

            OutfitViewModel outfitViewModel = new OutfitViewModel
            {
                Outfit = outfit,
                AllAccessories = (from a in db.Accessories
                                  select new SelectListItem
                                  {
                                      Value = a.AccessoryID.ToString(),
                                      Text = a.AccessoryName
                                  })
            };
            return View(outfitViewModel);
        }

        // POST: Outfits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OutfitId,OutfitName,TopId,BottomId,ShoeId")] Outfit outfit, List<int> SelectedAccessories)
        {
            if (ModelState.IsValid)
            {
                var existingOutfit = outfit;

                existingOutfit.TopId = outfit.TopId;
                existingOutfit.BottomId = outfit.BottomId;
                existingOutfit.ShoeId = outfit.ShoeId;
                existingOutfit.Accessories.Clear();

                foreach (int accessoryId in SelectedAccessories)
                {
                    existingOutfit.Accessories.Add(db.Accessories.Find(accessoryId));
                }
                db.Outfits.Add(outfit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BottomId = new SelectList(db.Bottoms, "BottomId", "BottomName", outfit.BottomId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "ShoeId", "ShoeName", outfit.ShoeId);
            ViewBag.TopId = new SelectList(db.Tops, "TopId", "TopName", outfit.TopId);
            return View(outfit);
        }





        // GET: Outfits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }

            var outfitViewModel = new OutfitViewModel
            {
                Outfit = outfit,
                AllAccessories = from a in db.Accessories
                                 select new SelectListItem
                                 {
                                     Text = a.AccessoryName,
                                     Value = a.AccessoryID.ToString()
                                 }
            };

            ViewBag.BottomId = new SelectList(db.Bottoms, "BottomId", "BottomName", outfit.BottomId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "ShoeId", "ShoeName", outfit.ShoeId);
            ViewBag.TopId = new SelectList(db.Tops, "TopId", "TopName", outfit.TopId);
            return View(outfitViewModel);
        }

        // POST: Outfits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OutfitViewModel outfitViewModel)
        {
            var outfit = db.Outfits.Find(outfitViewModel.Outfit.OutfitId);
            if (ModelState.IsValid)
            {
                outfit.Accessories.Clear();
                foreach (var accessoryId in outfitViewModel.SelectedAccessories)
                {
                    outfit.Accessories.Add(db.Accessories.Find(accessoryId));
                }
                outfit.ShoeId = outfitViewModel.Outfit.ShoeId;
                outfit.TopId = outfitViewModel.Outfit.TopId;
                outfit.BottomId = outfitViewModel.Outfit.BottomId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            outfitViewModel = new OutfitViewModel
            {
                Outfit = outfit,
                AllAccessories = from a in db.Accessories
                                 select new SelectListItem
                                 {
                                     Text = a.AccessoryName,
                                     Value = a.AccessoryID.ToString()
                                 }
            };
            ViewBag.BottomId = new SelectList(db.Bottoms, "BottomId", "BottomName", outfit.BottomId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "ShoeId", "ShoeName", outfit.ShoeId);
            ViewBag.TopId = new SelectList(db.Tops, "TopId", "TopName", outfit.TopId);
            return View(outfitViewModel);
        }

        // GET: Outfits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // POST: Outfits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outfit outfit = db.Outfits.Find(id);
            db.Outfits.Remove(outfit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
