using AutoMapper;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Repositories.Repositories.ProgramItem;
using JAP_Management.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.ProgramItem
{
    public class ProgramItemService : IProgramItemService
    {
        private readonly IProgramItemRepository _programItemRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProgramItemService(IProgramItemRepository programItemRepository, IUserService userService, IMapper mapper)
        {
            _programItemRepository = programItemRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public List<ItemsModel> GetItems()
        {
            return _mapper.Map<List<ItemsModel>>(_programItemRepository.GetItems());
        }

        #region GetItems
        public async Task<List<ItemsModel>> GetItemsAsync(ProgramItemSearchRequestModel programItemRequestModel, string userId)
        {
            try
            {

                var itemsList = await _programItemRepository.GetItemsAsync(programItemRequestModel);

                if (!itemsList.Any())
                    return null;

                var mappedItemList = _mapper.Map<List<ItemsModel>>(itemsList);


                return mappedItemList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        #region AddItem
        public async Task<ItemsModel> AddItemAsync(ItemsModel model)
        {
            try
            {
               var  mappedItem= _mapper.Map<Item>(model);

                var addedStudent = await _programItemRepository.AddItemAsync(mappedItem);

                if (addedStudent == null)
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

        #region GetItemById
        public async Task<ItemsModel> GetItemById(int itemId, string userId)
        {
            try
            {
                var item = await _programItemRepository.GetItemById(itemId);

                if (item == null)
                {
                    return null;
                }

                var mappedItem = _mapper.Map<ItemsModel>(item);

                // get user comments for students
                //mappedStudent.CommentByUser = student.Comments.FirstOrDefault(mr => mr.AdminId == userId)?.Comment ?? "/";

                return mappedItem;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }

        #endregion

        


        #region UpdateItem
        public async Task<ItemsModel> UpdateItemAsync(int itemId, ItemsModel model)
        {
            try
            {
                var mappedItem = _mapper.Map<Item>(model);


                var updatedItem = await _programItemRepository.Update(mappedItem);

                if (updatedItem == null)
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

        #region DeleteItem
        //public async Task<ItemsModel> DeleteItemAsync(int id)
        //{
        //    try
        //    {
        //        var deletedItem = await _programItemRepository.(id);

        //        var mappedItemModel = _mapper.Map<ItemsModel>(deletedItem);

        //        return mappedItemModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error -> ", ex);
        //        return null;
        //    }
        //}

        #endregion
    }
}
