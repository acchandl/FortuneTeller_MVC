using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fortune_Teller_MVC.Models;

namespace Fortune_Teller_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth1).Include(c => c.ColorID1);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //Retirement age fortune

            if (customer.Age % 2 == 1)
            {
                ViewBag.NumberOfYears = 10;
            }
            else if (customer.Age % 2 == 0)
            {
                ViewBag.NumberOfYears = 10000000;

            }

            //Money in the bank fortune

            var firstLetterOfMonth = customer.BirthMonth1.BirthMonthName[0];
            var secondLetterOfMonth = customer.BirthMonth1.BirthMonthName[1];
            var thirdLetterOfMonth = customer.BirthMonth1.BirthMonthName[2];
            string wholeName = customer.FirstName + customer.LastName;

            if (wholeName.Contains(firstLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 50;
            }
            else if (wholeName.Contains(secondLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 50000;
            }
            else if (wholeName.Contains(thirdLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 50000000;
            }

            else
            {
                ViewBag.AmountOfMoney = 0;
            }

            
            //Vacation home fortune
            
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.Location = "Red Riding Hood Woods";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.Location = "Three Little Pigs' Valley";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.Location = "The Seven Dwarfs Mine";
            }
            else if (customer.NumberOfSiblings == 3)
            {
               ViewBag.Location = "Briar Rose's Kingdom";
            }
            else
            {
                ViewBag.Location = "Rumpelstiltskin's Mountain";
            }

            //Mode of transporation fortune

            
            if (customer.ColorID1.ColorID1 == 1)
            {
                ViewBag.Transportation = "and a Royal Carriage.";
                
            }
            if (customer.ColorID1.ColorID1 == 2)
            {
                ViewBag.Transportation = "and a Magical Pumpkin that turns into a Royal Carriage.";
                
            }
            if (customer.ColorID1.ColorID1 == 3)
            {
                ViewBag.Transportation = "and a Magical Broom Stick.";

            }
            if (customer.ColorID1.ColorID1 == 4)
            {
                ViewBag.Transportation = "and a Magical Fairy.";

            }
            if (customer.ColorID1.ColorID1 == 5)
            {
                ViewBag.Transportation = "and a Magical Carpet";

            }
            if (customer.ColorID1.ColorID1 == 6)
            {
                ViewBag.Transportation = "and the Millennium Falcon.";

            }



            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName");
            ViewBag.ColorID = new SelectList(db.ColorIDs, "ColorID1", "ColorName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = customer.CustomerID });
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.ColorIDs, "ColorID1", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.ColorIDs, "ColorID1", "ColorName", customer.ColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.ColorIDs, "ColorID1", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
