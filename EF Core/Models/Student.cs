using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
