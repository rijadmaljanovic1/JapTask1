using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.ProgramItem
{
    public class ProgramItemRepository : BaseRepository<Item>, IProgramItemRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ProgramItemRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Item> GetItems()
        {
            try
            {
                return _databaseContext.Items.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }
        public async Task<List<Item>> GetItemsAsync(ProgramItemSearchRequestModel itemRequestModel)
        {
            try
            {
                var itemsPerPage = 5;

                var list = await _databaseContext.Items
                    .Include(z => z.ProgramItems)
                    .ToListAsync();

                return list
                    .Where(m => Search(m, itemRequestModel.Search, itemRequestModel.Filter))
                    .OrderBy(m => Sort(m, itemRequestModel.Sorting))
                    .Skip(itemsPerPage * (itemRequestModel.Page - 1))
                    .Take(5)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }


        public async Task<Item> AddItemAsync(Item mappedItem)
        {
            var newItem = new Item()
            {
                Url = mappedItem.Url,
                Description = mappedItem.Description,
                ExpectedHours = mappedItem.ExpectedHours,
                Name = mappedItem.Name
            };
            _databaseContext.Items.Add(newItem);
            _databaseContext.SaveChanges();

            return newItem;
        }

        private bool Search(Item item, SearchModel searchModel, int filter = 0)
        {
            if (searchModel.Value == "")
            {
                return true;
            }
            searchModel.Value = searchModel.Value.ToLower();

            if (filter == 0 || filter == 1)
            {
                var x = item.Name.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 2)
            {
                var x = item.Url.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 3)
            {
                var x = item.Description.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 4)
            {
                var x = item.ExpectedHours.ToString().Contains(searchModel.Value);
                return x;
            }
            
            return false;
        }

        private dynamic Sort(Item item, int sorting = 0)
        {
            if (sorting == 1)
            {
                return item.Id;
            }
            else if (sorting == 2)
            {
                return item.Name;
            }
            else if (sorting == 3)
            {
                return item.Url;
            }
            else if (sorting == 4)
            {
                return item.Description;
            }
            else if (sorting == 5)
            {
                return item.ExpectedHours;
            }
            return item.Id;
        }

        public async Task<Item> GetItemById(int itemId)
        {
            return await _databaseContext.Items
                .Include(m => m.ProgramItems)
                .FirstOrDefaultAsync(m => m.Id == itemId);
        }
    }
}
