using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Core.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<Course> Courses { get; set; } = new();
    }
}