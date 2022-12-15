namespace CabApp.Models.ViewModels
{
    public class EditViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        public Roles Role { get; set; }
    }
}
