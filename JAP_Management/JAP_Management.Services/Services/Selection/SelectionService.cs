using AutoMapper;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Selection
{
    public class SelectionService : ISelectionServicee
    {
        private readonly ISelectionRepository _selectionRepository;
        private readonly IMapper _mapper;

        public SelectionService(ISelectionRepository selectionRepository, IMapper mapper)
        {
            _selectionRepository = selectionRepository;
            _mapper = mapper;
        }

        #region GetSelections
        public async Task<List<SelectionModel>> GetSelectionsAsync(SelectionSearchRequestModel selectionRequestModel)
        {
            try
            {
                var selectionList = await _selectionRepository.GetSelectionsAsync(selectionRequestModel);

                if (!selectionList.Any())
                    return null;

                var mappedStudentList = _mapper.Map<List<SelectionModel>>(selectionList);

              

                return mappedStudentList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region AddSelection
        public async Task<SelectionUpsertRequest> AddSelectionAsync(SelectionUpsertRequest model)
        {
            try
            {
                var mappedAddedSelection = _mapper.Map<JAP_Management.Core.Entities.Selection>(model);

                var addedSelection = await _selectionRepository.Add(mappedAddedSelection);

                if (addedSelection == null)
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

        #region GetSelectionById
        public async Task<SelectionModel> GetSelectionById(int selectionId)
        {
            try
            {
                var selection = await _selectionRepository.GetSelectionById(selectionId);

                if (selection == null)
                {
                    return null;
                }

                var mappedSelection = _mapper.Map<SelectionModel>(selection);


                return mappedSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region GetSelectionByUpsertId
        public async Task<SelectionUpsertRequest> GetSelectionByUpsertId(int selectionId)
        {
            try
            {
                var selection = await _selectionRepository.GetSelectionById(selectionId);

                if (selection == null)
                {
                    return null;
                }

                var mappedSelection = _mapper.Map<SelectionUpsertRequest>(selection);


                return mappedSelection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region UpdateSelection
        public async Task<SelectionUpsertRequest> UpdateSelectionAsync(int selectionId, SelectionUpsertRequest model)
        {
            try
            {
                var mappedSelection = _mapper.Map<JAP_Management.Core.Entities.Selection>(model);

                mappedSelection.Id = selectionId;
                mappedSelection.ModifiedAt = DateTime.Now;

                var updatedSelection = await _selectionRepository.Update(mappedSelection);

                if (updatedSelection == null)
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

        #region DeleteSelection
        public async Task<SelectionModel> DeleteSelectionAsync(int id)
        {
            try
            {
                var deletedSelection = await _selectionRepository.DeleteSelection(id);

                var mappedSelectionModel = _mapper.Map<SelectionModel>(deletedSelection);

                return mappedSelectionModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion
    }
}
