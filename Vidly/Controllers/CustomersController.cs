﻿using System;
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

        private readonly ApplicationDbContext _context;

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
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
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

    }
}