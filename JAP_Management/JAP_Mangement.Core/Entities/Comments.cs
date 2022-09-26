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
        public int UserId { get; set; }
        public virtual BaseUser? User { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public string Comment { get; set; }
    }
}
