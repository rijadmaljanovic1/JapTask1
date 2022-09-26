using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class SelectionModel 
    {
        public int Id{ get; set; }
        public string? SelectionName { get; set; }
        public string? StatusName { get; set; }
        public string? Year { get; set; }
        public string? ProgramName { get; set; }
        public List<StudentModel2> Students { get; set; }

    }
}
