namespace CabApp.Models.ViewModels
{
    public class LoginViewModel
    {
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(25,MinimumLength =8)]
        public string Password { get; set; }
    }
}
