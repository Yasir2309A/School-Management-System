using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        [Required]
        [MaxLength(20)]
        public string ContactNumber { get; set; }

        public string Address { get; set; }

        [MaxLength(255)]
        public string ProfilePicture { get; set; }
    }
}
