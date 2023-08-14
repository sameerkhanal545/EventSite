using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EventSite.Models
{
    public class Category
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a Category id.")]
        [Remote("CheckCategory", "Validation", "Admin")] 
        public string CategoryId { get; set; }
        
        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a category name.")]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}