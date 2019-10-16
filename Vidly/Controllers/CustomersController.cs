using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            ////Defferred Execution
            //var customers = _context.Customers;

            //Immediate Execution
           // var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View();
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            //Immediate Execution with Lambda
            var cQuery = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);

            if (cQuery == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cQuery);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Used in conjunction with View Helper method AntiForgeryToken
        public ActionResult Save(Customer customer)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);

            }


            if (customer.Id == 0) //Add
            {
                _context.Customers.Add(customer);
            } 
            else //Edit
            {
                var customerInDb = _context.Customers.Single(x => x.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;

                ////Optional 2:
                //TryUpdateModel(customerInDb);

                ////Optional 3 Automapper:
                //Mapper.Map(customer, customerInDb);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {

                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                //Had to initialize Customer so that ModelState.IsValid would pass
                //since Customer must be not null
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

    

            return View("CustomerForm", viewModel);
        }
    }
}