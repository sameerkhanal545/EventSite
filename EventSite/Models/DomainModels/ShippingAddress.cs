using System.ComponentModel.DataAnnotations;

namespace EventSite.Models.DomainModels
{
	public class ShippingAddress
	{
		public int ShippingAddressId { get; set; }
		public string UserId { get; set; }
		[Required(ErrorMessage = "Please enter a name.")]
		[MaxLength(200)]
		public string FullName { get; set; }
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
