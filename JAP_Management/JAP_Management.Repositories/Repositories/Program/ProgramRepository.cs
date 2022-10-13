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

namespace JAP_Management.Repositories.Repositories.Program
{
    public class ProgramRepository : BaseRepository<JAP_Management.Core.Entities.Program>, IProgramRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ProgramRepository(DatabaseContext databaseContext): base(databaseContext)
        {
            _databaseContext = databaseContext;
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
    }
}
