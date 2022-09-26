using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } =DateTime.Now;
        public DateTime? ModifiedAt { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
}
