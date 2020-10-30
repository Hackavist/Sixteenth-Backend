using System;
using System.Collections.Generic;
using Services.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IMenuItemLogic
    {
        IEnumerable<MenuItemResponseDTO> GetAllItemsInABranch(int branchId);
        MenuItemResponseDTO Get(int id);
        int Insert(MenuItemRequestDTO entity);
        IEnumerable<int> InsertRange(IEnumerable<MenuItemRequestDTO> entities);
        void Update(int menuItemId, MenuItemRequestDTO entity);
        void SoftDelete(int id);       
    }
}
