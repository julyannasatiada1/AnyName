using Microsoft.AspNetCore.Mvc;
using AnyName.Data;
using AnyName.Models;

namespace AnyName.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db) { _db = db; }

        // get all products to show on the list
        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        // just open the add form page
        public IActionResult Create()
        {
            return View();
        }

        // save a new product to the database
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges(); 
            return RedirectToAction("Index");
        }

        // find product by id and open the edit form
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index"); 
            return View(product);
        }

        // save changes made in the edit form
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // find product by id and delete it
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
