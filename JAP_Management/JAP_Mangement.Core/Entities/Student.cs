using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Student : BaseEntity
    {
        [Key]
        public string BaseUserId { get; set; }
        public BaseUser BaseUser { get; set; }
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
