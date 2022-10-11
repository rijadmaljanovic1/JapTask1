using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Entities
{
    public class Admin : BaseEntity
    {
        [Key]
        public string BaseUserId { get; set; }
        public BaseUser User { get; set; }
    }
}
