using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Comments : BaseEntity
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
        public virtual Admin? Admin { get; set; }
        public string StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public string Comment { get; set; }
    }
}
