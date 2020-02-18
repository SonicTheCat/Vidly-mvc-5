﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
         public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public DateTime? BirthDate { get; set; }

        public int MembershipTypeId { get; set; }
    }
}