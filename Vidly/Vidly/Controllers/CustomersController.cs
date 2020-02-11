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
        private ApplicationDbContext context;

        public CustomersController()
        {
            this.context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }

        public ActionResult Index()
        {
            var customers = this.context
                .Customers
                .Include(c => c.MembershipType)
                .ToList();

            var models = new List<IndexCustomerViewModels>();

            foreach (var customer in customers)
            {
                models.Add(new IndexCustomerViewModels
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    MembershipType = customer.MembershipType
                });
            }

            return this.View(models);
        }

        public ActionResult Details(int id)
        {
            var customer = this.context
                .Customers
                .Include(x => x.MembershipType)
                .FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new DetailsCustomerViewModels
            {
                Name = customer.Name,
                MembershipTypeName = customer.MembershipType.Name,
                BirthDate = customer.BirthDate
            };

            return this.View(viewModel);
        }
    }
}