using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class StudentUpsertRequest
    {
        public string BaseUserId{ get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int MentorId { get; set; }
        public int SelectionId { get; set; }
        public int ProgramId { get; set; }
        public int StudentStatusId { get; set; }
    }
}
