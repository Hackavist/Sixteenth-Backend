using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Interfaces;
using Models.DataModels.Core;
using Repository;
using Services.DTOs;
using Services.Helpers;

namespace BusinessLogic.Implementations
{
    public class MenuItemLogic : IMenuItemLogic
    {
        private readonly IRepository<MenuItem> menuItemRepository;
        private readonly IMapper mapper;

        public MenuItemLogic(IRepository<MenuItem> menuItemRepository, IMapper mapper)
        {
            this.menuItemRepository = menuItemRepository;
            this.mapper = mapper;
        }

        public MenuItemResponseDTO Get(int id)
        {
            return mapper.Map<MenuItem, MenuItemResponseDTO>(menuItemRepository.Get(id));
        }

        public IEnumerable<MenuItemResponseDTO> GetAllItemsInABranch(int branchId)
        {
            return mapper.Map<IEnumerable<MenuItem>, IEnumerable<MenuItemResponseDTO>>(menuItemRepository.GetAll().Where(x => x.BranchId == branchId).ToList());
        }

        public int Insert(MenuItemRequestDTO entity)
        {
            MenuItem menuItem = mapper.Map<MenuItemRequestDTO, MenuItem>(entity);
            menuItemRepository.Insert(menuItem).Wait();
            return menuItem.Id > 0 ? menuItem.Id : throw new Exception($"Insertion of menuItem of name {menuItem.Name} failed");
        }

        public IEnumerable<int> InsertRange(IEnumerable<MenuItemRequestDTO> entities)
        {
            return entities.Select(Insert).ToList();
        }

        public void SoftDelete(int id)
        {
            MenuItem x = menuItemRepository.Get(id);
            menuItemRepository.SoftDelete(x ?? throw new KeyNotFoundException($"MenuItem with id {id} is not found in the database"));
        }

        public void Update(int menuItemId, MenuItemRequestDTO entity)
        {
            MenuItem oldItem = menuItemRepository.Get(menuItemId);
            MenuItem newItem = mapper.Map<MenuItemRequestDTO, MenuItem>(entity);
            newItem.Id = menuItemId;
            ObjectHelpers.UpdateObjects(oldItem, newItem);
            menuItemRepository.Update(oldItem);
        }
    }
}
