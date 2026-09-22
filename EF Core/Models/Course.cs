using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int DurationHours { get; set; }

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
