using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext context;

        public CustomersController()
        {
            this.context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customersDto = this.context
                .Customers
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return this.Ok(customersDto); 
        }

        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = this.context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return this.NotFound(); 
            }

            var dto = Mapper.Map<Customer, CustomerDto>(customer);
            return this.Ok(dto);
        }

        //POST /api/customers
        [HttpPost] // or we can remove the HttpPost attribute if our method is called 'PostCustomer'
        public IHttpActionResult CreateCustomer(CustomerDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(); 
            }

            var customer = Mapper.Map<CustomerDto, Customer>(dto); 
            this.context.Customers.Add(customer);
            this.context.SaveChanges();

            dto.Id = customer.Id;
            return this.Created(new Uri(Request.RequestUri + "/" + customer.Id), dto); 
        }

        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = this.context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(dto, customerInDb);

            this.context.SaveChanges();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = this.context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            this.context.Customers.Remove(customerInDb);
            this.context.SaveChanges();
        }
    }
}