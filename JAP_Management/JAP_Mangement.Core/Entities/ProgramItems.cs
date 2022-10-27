using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class ProgramItems
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int OrderNumber { get; set; }
    }
}
