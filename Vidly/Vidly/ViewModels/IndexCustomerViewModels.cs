using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class IndexCustomerViewModels
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MembershipType MembershipType { get; set; }
    }
}