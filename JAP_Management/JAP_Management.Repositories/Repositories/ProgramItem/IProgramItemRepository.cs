using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.ProgramItem
{
    public interface IProgramItemRepository : IBaseRepository<Item>
    {
       Task<List<Item>> GetItemsAsync(ProgramItemSearchRequestModel itemRequestModel);
       Task<Item> AddItemAsync(Item mappedItem);
       Task<Item> GetItemById(int itemId);
        List<Item> GetItems();

    }
}
