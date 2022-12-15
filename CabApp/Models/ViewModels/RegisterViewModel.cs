namespace CabApp.Models.ViewModels
{
    public enum Roles
    {
        User,
        Driver
    }
    public class RegisterViewModel
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

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(30, MinimumLength = 8)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
