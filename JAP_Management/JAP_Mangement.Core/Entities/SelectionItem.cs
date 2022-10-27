using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class SelectionItem
    {
        public int Id { get; set; }
        public int SelectionId { get; set; }
        public int ItemId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
