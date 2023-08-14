using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventSite.Models
{
    public class CheckoutViewModel
    {
        public List<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();

		public double OrderTotal { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [MaxLength(200)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter a Address 1.")]
        [MaxLength(200)]
        public string AddressLine1 { get; set; }
     
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter a city.")]
        [MaxLength(200)]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a state.")]
        [MaxLength(200)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter a postal code.")]
        [MaxLength(200)]
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
