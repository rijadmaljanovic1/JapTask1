using JAP_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Models
{
    public class ProgramItemModel
    {
        public int ProgramId { get; set; }
        public int ItemId { get; set; }
        public List<ItemsModel> ItemsModel { get; set; }
    }
}
