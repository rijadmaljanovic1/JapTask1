using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MentorId{ get; set; }
        public virtual Mentor? Mentor { get; set; }
        public int SelectionId { get; set; }
        public virtual Selection? Selection { get; set; }
        public int ProgramId { get; set; }
        public virtual Program? Program { get; set; }
        public int StudentStatusId { get; set; }
        public virtual StudentStatus? StudentStatus { get; set; }
        public virtual ICollection<Comments>? Comments { get; set; }

    }
}
