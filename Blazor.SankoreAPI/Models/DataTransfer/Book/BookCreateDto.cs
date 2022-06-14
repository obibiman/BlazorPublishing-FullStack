﻿using System.ComponentModel.DataAnnotations;

namespace Blazor.SankoreAPI.Models.DataTransfer.Book
{
    public class BookCreateDto
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        [Required, Range(1000, int.MaxValue)]
        public string Year { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required, StringLength(250, MinimumLength=10)]
        public string Summary { get; set; }
        public string Image { get; set; }
        [Required, Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
