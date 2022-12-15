namespace CabApp.Models
{
    [Index("LicenseNumber", IsUnique = true)]
    [Index("CabNumber", IsUnique = true)]
    public class CabDriver
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string LicenseNumber { get; set; } 

        [StringLength(10)]
        [Required]
        public string CabNumber { get; set; }

        [StringLength(25)]
        public string CabName { get; set; }

        public Types CabType { get; set; }

        public ApplicationUser Driver { get; set; }

        [ForeignKey(nameof(Driver))]
        public string DriverId { get; set; }
    }
}
