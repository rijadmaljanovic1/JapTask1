using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class StudentUpsertRequest
    {
        public int Id{ get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int MentorId { get; set; }
        public int SelectionId { get; set; }
        public int ProgramId { get; set; }
        public int StudentStatusId { get; set; }
    }
}
