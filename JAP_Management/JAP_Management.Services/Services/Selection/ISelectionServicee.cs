using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Selection
{
    public interface ISelectionServicee
    {
        Task<List<SelectionModel>> GetSelectionsAsync(SelectionSearchRequestModel selectionRequestModel);
        Task<SelectionUpsertRequest> AddSelectionAsync(SelectionUpsertRequest model);
        Task<SelectionModel> GetSelectionById(int selectionId);
        Task<SelectionUpsertRequest> GetSelectionByUpsertId(int selectionId);
        Task<SelectionUpsertRequest> UpdateSelectionAsync(int selectionId, SelectionUpsertRequest model);
        Task<SelectionModel> DeleteSelectionAsync(int id);
    }
}
