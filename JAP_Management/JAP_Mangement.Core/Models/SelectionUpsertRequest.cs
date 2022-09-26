using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class SelectionUpsertRequest
    {
        public int Id { get; set; }
        public string? SelectionName { get; set; }
        public int StatusId { get; set; }
        public string? Year { get; set; }
        public int ProgramId { get; set; }

    }
}
