using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            //Maps Customer to CustomerDto with delegate method reference
            //Leave as IQueryable to perform query on this
            var customersQuery = _context.Customers
                    .Include(x => x.MembershipType);

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustommer(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);
            if (customer == null)
                return NotFound();

            //Maps Customer to new CustomerDto and returns it
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //public CustomerDto GetCustommer(int id)
        //{
        //    var customer = _context.Customers.SingleOrDefault(x => x.Id == id);
        //    if (customer == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);

        //    //Maps Customer to new CustomerDto and returns it
        //    return Mapper.Map<Customer, CustomerDto>(customer);
        //}

        // POST /api/customers
        //Only called if sending HttpPost request. Used for Adding
        //Using IHttpActionResult as the return type of actions will align with Restful conventions
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //Map customerDto to new customer 
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //Customer added to Database creates Id
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        //public CustomerDto CreateCustomer(CustomerDto customerDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    //Map customerDto to new customer 
        //    var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

        //    _context.Customers.Add(customer);
        //    _context.SaveChanges();

        //    //Customer added to Database creates Id
        //    customerDto.Id = customer.Id;

        //    return customerDto;
        //}

        // PUT /api/customers/1
        [HttpPut]//Only called if sending HttpPut request. Used for updating
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //Map customerDto to customerInDb 
            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]//Only called if sending HttpDelete request. Used for deleting
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}
