using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class StudentModel 
    {
        public string BaseUserId{ get; set; }
        public string? FullName { get; set; }
        //public DateTime DateOfBirth { get; set; }
        public string MentorName{ get; set; }
        public string SelectionName { get; set; }
        public string ProgramName { get; set; }
        public string StudentStatusName { get; set; }
        public string CommentByUser { get; set; }

    }
}
