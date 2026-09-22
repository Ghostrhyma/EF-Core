using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null;

        public DateTime EnrolledAt { get; set; }
        public int Grade { get; set; }
    }
}
