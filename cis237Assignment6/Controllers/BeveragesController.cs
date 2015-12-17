using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237Assignment6.Models;

namespace cis237Assignment6.Controllers
{
    [Authorize]
    public class BeveragesController : Controller
    {
        private BeverageSDiagneEntities db = new BeverageSDiagneEntities();

        // GET: Beverages
        public ActionResult Index(string sortby)
        {
            
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortby) ? "Name asc" : "";
            ViewBag.SortPriceParameter = string.IsNullOrEmpty(sortby) ? "Price asc" : "";

            DbSet<Beverage> BeveragesToSearch = db.Beverages;
            Beverage beverage = new Beverage();

            string filterName = "";
            string filterPack = "";
            string filterMin = "";
            string filterMax = "";
            bool filterActive = beverage.active ;
            

            decimal min = 0;
            decimal max = 100;
           
            if (Session["name"] != null && !String.IsNullOrWhiteSpace((string)Session["name"]))
            {
                filterName = (string)Session["name"];
            }

            if (Session["pack"] != null && !String.IsNullOrWhiteSpace((string)Session["pack"]))
            {
                filterPack = (string)Session["pack"];
            }

            if (Session["min"] != null && !String.IsNullOrWhiteSpace((string)Session["min"]))
            {
                filterMin = (string)Session["min"];
                min = decimal.Parse(filterMin);
            }

            if (Session["max"] != null && !String.IsNullOrWhiteSpace((string)Session["max"]))
            {
                filterMax = (string)Session["max"];
                max = decimal.Parse(filterMax);
            }

            if (Session["active"] != null && !String.IsNullOrWhiteSpace((string)Session["active"]))
            {                
                switch (Int32.Parse((string)Session["active"]))
                {
                    case -1:
                        break;
                    case 0:filterActive = false;
                        break;
                    case 1:filterActive = true;
                        break;
                }

            }

            IEnumerable<Beverage> filtered = BeveragesToSearch.Where(Beverage => Beverage.price >= min &&
                                                                                 Beverage.price <= max &&
                                                                                 Beverage.name.Contains(filterName) &&
                                                                                 Beverage.pack.Contains(filterPack)&&
                                                                                 Beverage.active ==  filterActive);

            switch (sortby)
            {
                case "Name asc":
                    filtered = filtered.OrderBy(x => x.name);
                    break;

                case "Price asc":
                    filtered = filtered.OrderBy(x => x.price);
                    break;
            }

            IEnumerable<Beverage> finalFiltered = filtered.ToList();

            ViewBag.filterName = filterName;
            ViewBag.filterPack = filterPack;
            ViewBag.filterMin = filterMin;
            ViewBag.filterMax = filterMax;
            //ViewBag.filterActive = filterActive;

            //Return the view with a filtered elesction
            return View(finalFiltered);

            //This is what is used to be returned before a filter was setup
            //return View(db.Beverages.ToList());
        }

        // GET: Beverages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // GET: Beverages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beverages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]            
        public ActionResult Create([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                db.Beverages.Add(beverage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beverage);
        }

        // GET: Beverages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pack,price,active")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                    db.Entry(beverage).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beverage beverage = db.Beverages.Find(id);
            if (beverage == null)
            {
                return HttpNotFound();
            }
            return View(beverage);
        }

        // POST: Beverages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Beverage beverage = db.Beverages.Find(id);
            db.Beverages.Remove(beverage);
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
        
        [HttpPost, ActionName("Filter")]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            String name = Request.Form.Get("name");
            String pack = Request.Form.Get("pack");
            String min = Request.Form.Get("min");
            String max = Request.Form.Get("max");
            String active = Request.Form.Get("active");

            Session["name"] = name;
            Session["pack"] = pack;
            Session["min"] = min;
            Session["max"] = max;
            Session["active"] = active;

            return RedirectToAction("Index");
        }

       
    }
}
