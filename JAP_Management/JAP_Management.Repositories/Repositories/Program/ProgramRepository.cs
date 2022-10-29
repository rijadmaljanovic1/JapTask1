using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Base;
using JAP_Management.Repositories.Repositories.Students;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JAP_Management.Core.Entities;
using JAP_Management.Repositories.Repositories.Selection;
using JAP_Management.Core.Helpers;

namespace JAP_Management.Repositories.Repositories.Program
{
    public class ProgramRepository : BaseRepository<JAP_Management.Core.Entities.Program>, IProgramRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ISelectionRepository _selectionRepository;
        public ProgramRepository(DatabaseContext databaseContext, ISelectionRepository selectionRepository) : base(databaseContext)
        {
            _databaseContext = databaseContext;
            _selectionRepository = selectionRepository;
        }

        public async Task<List<JAP_Management.Core.Entities.Program>> GetProgramsAsync(ProgramSearchRequestModel programModel)
        {
            try
            {
                var list = await _databaseContext.Programs.Include(x=> x.Technologies).ToListAsync();

                return list.Where(m => Search(m, programModel.Search)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }
        private bool Search(JAP_Management.Core.Entities.Program program, SearchModel searchModel)
        {
            if (searchModel == null)
            {
                return true;
            }
            else
            {
                searchModel.Value = searchModel.Value.ToLower();

                var x = program.Name.ToLower().Contains(searchModel.Value) ||
                        program.Description.ToLower().Contains(searchModel.Value);
                return x;
            }
        } 

        public async Task<JAP_Management.Core.Entities.Program> GetProgramById(int programId)
        {
            return await _databaseContext.Programs
                .Include(m => m.Technologies)
                .FirstOrDefaultAsync(m => m.Id == programId);
        }
        public async Task<ProgramItemModel> AddProgramItemsAsync(ProgramItemModel programItemModels)
        {
            if(programItemModels == null || !programItemModels.ItemsModel.Any())
            {
                return new ProgramItemModel();
            }

            int i = 1;

            foreach (var item in programItemModels.ItemsModel)
            {
                item.OrderNumber = i;
                i++;
            }

            var currentProgramItems = await GetProgramItemsByProgramId(programItemModels.ProgramId);
            var programItemsDeleted = DeleteProgramItems(currentProgramItems);

            if (programItemsDeleted)
            {
                foreach (var item in programItemModels.ItemsModel)
                {
                    await _databaseContext.ProgramItems.AddAsync(new Core.Entities.ProgramItems
                    {
                        ProgramId = programItemModels.ProgramId,
                        ItemId = item.Id,
                        OrderNumber = item.OrderNumber
                    });
                }
                await _databaseContext.SaveChangesAsync();
            }

            var items = programItemModels.ItemsModel.OrderBy(m => m.OrderNumber);

            var selection = _selectionRepository.GetSelectionByProgramId(programItemModels.ProgramId).Result;

            List<SelectionItem> selectionItems = new();
            DateTime previousItemEndDate = new();
            int iteration = 0;

            foreach (var item in items)
            {
                var endDate = DateCalculator.CalculateEndDate(iteration == 0 ? selection.StartDate : previousItemEndDate, item.ExpectedHours);
                selectionItems.Add(new SelectionItem()
                {
                    SelectionId = selection.Id,
                    ItemId = item.Id,
                    StartDate = iteration == 0 ? selection.StartDate : previousItemEndDate,
                    EndDate = endDate
                });
                previousItemEndDate = endDate;
                if (previousItemEndDate.DayOfWeek == DayOfWeek.Saturday)
                    previousItemEndDate.AddDays(2);
                iteration++;
            }
            await _databaseContext.AddRangeAsync(selectionItems);
            await _databaseContext.SaveChangesAsync();

            var currentSelectionItems = await GetSelectionItemsBySelectionId(selection.Id);
            var selectionItemsDeleted = DeleteSelectionItems(currentSelectionItems);
            List<SelectionItem> newSelectionItems = new();
            try
            {
                if (selectionItemsDeleted)
                {
                    foreach (var item in selectionItems)
                    {
                        newSelectionItems.Add(new SelectionItem
                        {
                            SelectionId = selection.Id,
                            ItemId = item.ItemId,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate
                        });
                    }
                }
                await _databaseContext.AddRangeAsync(newSelectionItems);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return new ProgramItemModel();
        }

        public async Task<List<Item>> GetItemsByProgramId(int programId)
        {
            try
            {
                var programItems = await _databaseContext.ProgramItems.Where(m => m.ProgramId == programId).ToListAsync();
                var items = await _databaseContext.Items.ToListAsync();
                var list= new List<Item>();

                foreach (var item in items)
                {
                    foreach (var programItem in programItems)
                    {
                        if (item.Id == programItem.ItemId)
                        {
                            list.Add(item);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        public async Task<List<ProgramItems>> GetProgramItemsByProgramId(int programId)
        {
            try
            {
                var programItems = await _databaseContext.ProgramItems.Where(m => m.ProgramId == programId).ToListAsync();

                return programItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }
        public bool DeleteProgramItems(List<ProgramItems> items)
        {
             _databaseContext.ProgramItems.RemoveRange(items);
            return true;
        }
       
        public async Task<List<SelectionItem>> GetSelectionItemsBySelectionId(int selectionId)
        {
            try
            {
                var selectionItems = await _databaseContext.SelectionItems.Where(m => m.SelectionId == selectionId).ToListAsync();

                return selectionItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }
        public bool DeleteSelectionItems(List<SelectionItem> items)
        {
            try
            {
                _databaseContext.SelectionItems.RemoveRange(items);
                _databaseContext.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }

            return true;
        }
    }
}
