using JAP_Management.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Program
{
    public interface IProgramService
    {
        Task<List<ProgramModel>> GetProgramsAsync(ProgramSearchRequestModel programModel);
        Task<ProgramModel> AddProgramAsync(ProgramModel model);
        Task<JAP_Management.Core.Entities.Program> GetProgramById(int programId, string userId);
        Task<ProgramModel> UpdateProgramAsync(int programId, ProgramModel model);
        //Task<ProgramModel> DeleteProgramAsync(int id);
    }
}
