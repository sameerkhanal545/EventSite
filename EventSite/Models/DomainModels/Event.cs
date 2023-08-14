using EventSite.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSite.Models
{
    public partial class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [MaxLength(200)]
        public string Title { get; set; }

        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Please enter a Description.")]
        [MaxLength(1000)]
        public string Description { get; set; }


        [Required(ErrorMessage = "Please enter a Location.")]
        [MaxLength(200)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Please enter a EventDate.")]
        public DateTime EventDate { get; set; }

		[Required(ErrorMessage = "Please enter a Event end Date.")]
		public DateTime EventEndDate { get; set; }

		[Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<EventOrganizer>  EventOrganizers{ get; set; }
    }
}
