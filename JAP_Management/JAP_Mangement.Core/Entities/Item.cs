using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Item
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public string Url{ get; set; }
        public int ExpectedHours{ get; set; }
        public virtual ICollection<ProgramItems>? ProgramItems { get; set; }
        public virtual ICollection<SelectionItem>? SelectionItems { get; set; }
        public virtual ICollection<StudentItem>? StudentItems { get; set; }
    }
}
