namespace CabApp.Models.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [Display(Name = "Pick Up")]
        public Locations PickUp { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public Locations Destination { get; set; }


        [Display(Name = "Time of Booking")]
        public DateTime BookingTime { get; set; }

        [Display(Name = "Time of Trip")]
        public DateTime CabRideTime { get; set; } = DateTime.Now;

        [Display(Name = "Type of Cab")]
        public Types CabType { get; set; } = Types.Hatchback;
    }
}
