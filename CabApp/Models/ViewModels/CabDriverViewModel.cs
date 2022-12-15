namespace CabApp.Models.ViewModels
{
    public class CabDriverViewModel
    {
        [StringLength(20)]
        [Required]
        public string LicenseNumber { get; set; }

        [StringLength(10)]
        [Required]
        public string CabNumber { get; set; }

        [StringLength(25)]
        public string CabName { get; set; }

        public Types CabType { get; set; }
    }
}
