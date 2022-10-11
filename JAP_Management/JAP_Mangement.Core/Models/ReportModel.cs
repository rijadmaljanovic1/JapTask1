using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class ReportModel
    {
        public string SelectionName{ get; set; }
        public string ProgramName{ get; set; }
        public float StudentSuccessRate{ get; set; }
        public int OverallSuccess{ get; set; }
    }
}
