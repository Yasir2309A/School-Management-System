using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; } = DateTime.Now; 
 
        public string Status { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
    }
}
