using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class StudentItemsModel
    {
        public int ItemId{ get; set; }
        public string ItemName{ get; set; }
        public string? Url{ get; set; }
        public int ExpectedHours{ get; set; }
        public int OrderNumber{ get; set; }
        public string PercentageDone { get; set; } = "0%";
        public string CandidateStatus{ get; set; } = "NotStarted";
        public DateTime StartDate{ get; set; }
        public DateTime EndDate{ get; set; }
    }
}
