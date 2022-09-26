using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Program
{
    public interface IProgramRepository : IBaseRepository<JAP_Management.Core.Entities.Program>
    {
        Task<List<JAP_Management.Core.Entities.Program>> GetProgramsAsync(ProgramSearchRequestModel programModel);
        Task<JAP_Management.Core.Entities.Program> GetProgramById(int programId);

    }
}
