using EventSite.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSite.Models
{
    public class EventViewModel : IValidatableObject
    {
        public Event Event { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Organizer> Organizers { get; set; }
        public int[] SelectedOrganizers { get; set; }

        [NotMapped]
        [RequiredImage(ErrorMessage = "Please upload an image.")]
        [Display(Name = "Event Image")]
        public IFormFile ImageFile { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx) {
            if (SelectedOrganizers == null)
            {
                yield return new ValidationResult(
                  "Please select at least one organizer.",
                  new[] { nameof(SelectedOrganizers) });
            }
        }

    }
}
