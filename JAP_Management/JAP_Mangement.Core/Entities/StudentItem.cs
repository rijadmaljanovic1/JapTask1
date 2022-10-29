using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class StudentItem
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int ItemId { get; set; }
        public string PercentageDone{ get; set; }
        public string CandidateStatus{ get; set; }
    }
}
