using System.ComponentModel.DataAnnotations.Schema;

namespace CabApp.Models
{
    public enum Types
    {
        [Display(Name = "AutoRickshaw")]
        Auto,
        [Display(Name = "Compact Car")]
        Hatchback,
        [Display(Name = "Compact Sedan")]
        Sedan,
        [Display(Name = "7-Seater")]
        SevenSeater,
        [Display(Name = "Luxurious Limo")]
        Limousine
    }

    public enum Locations
    {
        [Display(Name = "Vytilla Hub")]
        VytillaHub,
        [Display(Name = "Panampilly Nagar")]
        PanampillyNagar,
        Kakkanad,
        Edappally,
        Palarivattom,
        Kalamassery,
        [Display(Name = "North Railway Station")]
        NorthRailwayStation,
        [Display(Name = "South Railway Station")]
        SouthRailwayStation,
        Kaloor,
        Thripunithura
    }

    public class BookingCab
    {
        public int Id { get; set; }

        [Required]
        public Locations PickUp { get; set; }

        [Required]
        public Locations Destination { get; set; }

        public DateTime BookingTime { get; set; }

        public DateTime CabRideTime { get; set; }

        public Types CabType { get; set;}

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public CabDriver? Driver { get; set; }

        [ForeignKey(nameof(Driver))]
        public int? DriverId { get; set; }
    }
}
 