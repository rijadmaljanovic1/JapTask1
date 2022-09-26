using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class CommentsModel 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public string Comment { get; set; }
    }
}
