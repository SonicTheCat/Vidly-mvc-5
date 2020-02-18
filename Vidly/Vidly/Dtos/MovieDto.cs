﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1, 1000, ErrorMessage = "Number in stock must be in range from 1 to 1000")]
        public int NumberInStock { get; set; }

        public int GenreId { get; set; }
    }
}