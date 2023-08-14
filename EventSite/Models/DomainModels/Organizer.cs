using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EventSite.Models
{
    public class Organizer
    {
        public int OrganizerId { get; set; }

        [Required(ErrorMessage = "Please enter a Organizer name.")]
        [MaxLength(200)]
        public string OrganizerName { get; set; }
        public ICollection<EventOrganizer> EventOrganizers { get; set; }
    }
}
