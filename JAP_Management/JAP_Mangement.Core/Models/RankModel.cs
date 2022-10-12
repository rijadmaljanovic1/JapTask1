using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class RankModel
    {
        public string SelectionName { get; set; }
        public string ProgramName { get; set; }
        public decimal StudentSuccessRate { get; set; }
        public int OverallSuccess { get; set; }
    }
}
