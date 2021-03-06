﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

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
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = this.context
                .Customers
                .Include(x => x.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(x => x.Name.Contains(query));
            }

            var customersDto = customersQuery
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
        public IHttpActionResult UpdateCustomer(int id, CustomerDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var customerInDb = this.context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                return this.NotFound();
            }

            Mapper.Map(dto, customerInDb);
            this.context.SaveChanges();

            return this.Ok();
        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = this.context.Customers.SingleOrDefault(x => x.Id == id);
            if (customerInDb == null)
            {
                return this.NotFound();
            }

            this.context.Customers.Remove(customerInDb);
            this.context.SaveChanges();

            return this.Ok();
        }
    }
}