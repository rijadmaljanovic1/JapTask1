using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Selection
{
    public interface ISelectionRepository : IBaseRepository<JAP_Management.Core.Entities.Selection>
    {
        Task<List<JAP_Management.Core.Entities.Selection>> GetSelectionsAsync(SelectionSearchRequestModel selectionRequestModel);
        Task<JAP_Management.Core.Entities.Selection> GetSelectionById(int selectionId);
    }
}
