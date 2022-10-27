using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.ProgramItem
{
    public interface IProgramItemService
    {
       Task<ItemsModel> UpdateItemAsync(int itemId, ItemsModel model);
        Task<ItemsModel> GetItemById(int itemId, string userId);
        Task<ItemsModel> AddItemAsync(ItemsModel model);
        Task<List<ItemsModel>> GetItemsAsync(ProgramItemSearchRequestModel programItemRequestModel, string userId);
        List<ItemsModel> GetItems();

    }
}
