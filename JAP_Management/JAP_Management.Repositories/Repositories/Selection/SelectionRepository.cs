using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Selection
{
    public class SelectionRepository : BaseRepository<JAP_Management.Core.Entities.Selection>, ISelectionRepository
    {
        private readonly DatabaseContext _databaseContext;
        public SelectionRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<List<JAP_Management.Core.Entities.Selection>> GetSelectionsAsync(SelectionSearchRequestModel selectionRequestModel)
        {
            try
            {
                var itemsPerPage = 5;
                var list = await _databaseContext.Selections
                    .Include(m => m.Status)
                    .Include(m => m.Program)
                    .Include(z => z.Students)
                    .ThenInclude(m => m.BaseUser)
                    .ToListAsync();

                return list
                .Where(m => Search(m, selectionRequestModel.Search, selectionRequestModel.Filter))
                .OrderBy(m => Sort(m, selectionRequestModel.Sorting))
                .Skip(itemsPerPage * (selectionRequestModel.Page - 1))
                .Take(5)
                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        private bool Search(JAP_Management.Core.Entities.Selection selection, SearchModel searchModel, int filter = 0)
        {
            if (searchModel == null)
            {
                return true;
            }
            searchModel.Value = searchModel.Value.ToLower();

            if (filter == 0 || filter == 1)
            {
                var x = selection.SelectionName.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 2)
            {
                var x = selection.Status.Name.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 3)
            {
                var x = selection.Program.Name.ToLower().Contains(searchModel.Value);
                return x;
            }
            else if (filter == 4)
            {
                var x = selection.Year.ToLower().Contains(searchModel.Value);
                return x;
            }

            return false;
        }

        private dynamic Sort(JAP_Management.Core.Entities.Selection selection, int sorting = 0)
        {
            if (sorting == 1)
            {
                return selection.Id;
            }
            if (sorting == 2)
            {
                return selection.SelectionName;
            }
            else if (sorting == 3)
            {
                return selection.Status.Name;
            }
            else if (sorting == 4)
            {
                return selection.Program.Name;
            }
            else if (sorting == 5)
            {
                return selection.Year;
            }

            return selection.Id;
        }

        public async Task<JAP_Management.Core.Entities.Selection> GetSelectionById(int selectionId)
        {
            var selection = await _databaseContext.Selections
                 .Include(m => m.Program)
                .Include(m => m.Status)
                .Include(z => z.Students)
                .ThenInclude(m => m.BaseUser)
                .FirstOrDefaultAsync(m => m.Id == selectionId);

            return selection;
        }

    }
}
