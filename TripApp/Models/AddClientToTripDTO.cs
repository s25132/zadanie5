using System.ComponentModel.DataAnnotations;

namespace TripApp.Models
{
    public class AddClientToTripDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string Pesel { get; set; }

        public string TripName { get; set; }

        public DateTime? PaymentDate { get; set; }

    }
}
