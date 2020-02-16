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

        public ActionResult Create()
        {
            var membershipTypes = this.context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return this.View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = this.context.MembershipTypes.ToList()
                };

                return this.View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                this.context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = this.context
                    .Customers
                    .Include(x => x.MembershipType)
                    .Single(x => x.Id == customer.Id);

                // TryUpdateModel() is not very good way for updating model! Its not secure!
                // this.TryUpdateModel(customerInDb); 

                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            this.context.SaveChanges();
            return this.RedirectToAction(nameof(this.Index));
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

        public ActionResult Edit(int id)
        {
            var customer = this.context
               .Customers
               .Include(x => x.MembershipType)
               .FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return this.HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = this.context.MembershipTypes.ToList()
            };

            return this.View("CustomerForm", viewModel);
        }
    }
}