using AutoMapper;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Program
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _programRepository;
        private readonly IMapper _mapper;

        public ProgramService(IProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }

        #region GetPrograms
        public async Task<List<ProgramModel>> GetProgramsAsync(ProgramSearchRequestModel programModel)
        {
            try
            {
                var programsList = await _programRepository.GetProgramsAsync(programModel);

                if (!programsList.Any())
                    return null;

                var mappedProgramList = _mapper.Map<List<ProgramModel>>(programsList);

                return mappedProgramList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region AddProgram
        public async Task<ProgramModel> AddProgramAsync(ProgramModel model)
        {
            try
            {
                var mappedAddedProgram = _mapper.Map<JAP_Management.Core.Entities.Program>(model);

                var addedProgram= await _programRepository.Add(mappedAddedProgram);

                if (addedProgram == null)
                    return null;

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region GetProgramById
        public async Task<JAP_Management.Core.Entities.Program> GetProgramById(int programId, string userId)
        {
            try
            {
                var program = await _programRepository.GetProgramById(programId);

                if (program == null)
                {
                    return null;
                }

                return program;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region UpdateProgram
        public async Task<ProgramModel> UpdateProgramAsync(int programId, ProgramModel model)
        {
            try
            {
                var mappedProgram = _mapper.Map<JAP_Management.Core.Entities.Program>(model);

                mappedProgram.Id = programId;
                mappedProgram.ModifiedAt = DateTime.Now;

                var updatedProgram = await _programRepository.Update(mappedProgram);

                if (updatedProgram == null)
                    return null;

                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        //#region DeleteProgram
        //public async Task<ProgramModel> DeleteProgramAsync(int id)
        //{
        //    try
        //    {
        //        var deletedProgram = await _programRepository.Delete(id);

        //        var mappedProgramModel = _mapper.Map<ProgramModel>(deletedProgram);

        //        return mappedProgramModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error -> ", ex);
        //        return null;
        //    }
        //}

        //#endregion
    }
}
