using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Selection : BaseEntity
    {
        public int Id { get; set; }
        public string? SelectionName { get; set; }
        public int StatusId { get; set; }
        public virtual SelectionStatus? Status { get; set; }
        public string? Year { get; set; }
        public int ProgramId { get; set; }
        public virtual Program? Program { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}
