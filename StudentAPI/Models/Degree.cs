using System;
using System.Collections.Generic;

namespace StudentAPI.Models
{
    public partial class Degree
    {
        public Degree()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string Code { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
