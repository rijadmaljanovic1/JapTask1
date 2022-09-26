using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class StudentSearchRequestModel
    {
        public int Page { get; set; }
        public int Filter { get; set; } = 0;
        public int Sorting { get; set; } = 0;
        public SearchModel Search { get; set; }
    }
}
