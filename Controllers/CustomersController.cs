using Microsoft.AspNetCore.Mvc;
using AnyName.Data;
using AnyName.Models;

namespace AnyName.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomersController(ApplicationDbContext db) { _db = db; }

        // show customer list page
        public IActionResult Index()
        {
            var customers = _db.Customers.ToList();
            return View(customers);
        }

        // open the add form page
        public IActionResult Create()
        {
            return View();
        }

        // save a new customer
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // find customer by id and open edit form
        public IActionResult Edit(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);
        }

        // save changes made in edit form
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // find customer by id and delete it
        public IActionResult Delete(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer != null)
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
