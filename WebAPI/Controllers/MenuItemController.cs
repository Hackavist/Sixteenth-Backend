using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;

namespace SixteenthApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController:ControllerBase
    {
        private readonly IMenuItemLogic menuItemLogic;

        public MenuItemController(IMenuItemLogic menuItemLogic)
        {
            this.menuItemLogic = menuItemLogic;
        }

        /// <summary>
        ///     Gets all the menu items in a branch
        /// </summary>
        /// <param name="branchId">The Requested Branch Id</param>
        [ProducesResponseType(200, Type = typeof(IEnumerable<MenuItemResponseDTO>))]
        [HttpGet("ByBranchId")]
        public IActionResult GetMenuItemsOfABranch([FromQuery] int branchId)
        {
            return Ok(menuItemLogic.GetAllItemsInABranch(branchId));
        }

        /// <summary>
        ///     Gets a branch by Id
        /// </summary>
        /// <param name="itemId">The Requested Item Id</param>
        [ProducesResponseType(200, Type = typeof(MenuItemResponseDTO))]
        [HttpGet("ByItemId")]
        public IActionResult GetMenuItemById([FromQuery] int itemId)
        {
            return Ok(menuItemLogic.Get(itemId));
        }

        /// <summary>
        ///     Inserts a new MenuItem and returns its Id
        /// </summary>
        /// <param name="menuItem">The menuitem Request Object</param>
        /// <response code="201">Successfully Inserted the menuItem, Returns The menuItem Id</response>
        /// <response code="400">Error inserting the menuItem, please make sure that the data is correct</response>
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400, Type = null)]
        [HttpPost]
        public IActionResult InsertMenuItem([FromBody] MenuItemRequestDTO menuItem)
        {
            int ret = menuItemLogic.Insert(menuItem);
            return ret > 0
                ? (IActionResult)Ok(ret)
                : BadRequest();
        }

        /// <summary>
        ///     Inserts some new MenuItems and returns Their Ids
        /// </summary>
        /// <param name="menuItems">The menuitem Request Objects</param>
        /// <response code="200">Successfully Inserted the menuItems, Returns The items Ids</response>
        /// <response code="400">Error inserting the menuItems, please make sure that the data is correct</response>
        [ProducesResponseType(201, Type = typeof(IEnumerable<int>))]
        [ProducesResponseType(400, Type = null)]
        [HttpPost("Range")]
        public IActionResult InsertMenuItems([FromBody] IEnumerable<MenuItemRequestDTO> menuItems)
        {
            var ret = menuItemLogic.InsertRange(menuItems);
            if (ret.Any(id => id <= 0)) return BadRequest();
            return Ok(ret);
        }

        /// <summary>
        ///     Updates the menuitem
        /// </summary>
        /// <param name="menuItemId">The menuItem Id </param>
        /// <param name="menuItem">The new menuItem</param>
        /// <response code="200">Successfully updated the menuItem </response>
        [ProducesResponseType(200, Type = null)]
        [HttpPut]
        public IActionResult UpdateMenuItem([FromQuery] int menuItemId, [FromBody] MenuItemRequestDTO menuItem)
        {
            menuItemLogic.Update(menuItemId, menuItem);
            return Ok();
        }

        /// <summary>
        ///     Deleting a MenuItem using its Id
        /// </summary>
        /// <param name="menuItemId">The MenuItem Id </param>
        /// <response code="200">menuItem was successfully deleted from the database</response>
        [ProducesResponseType(200, Type = null)]
        [HttpDelete]
        public IActionResult DeleteMenuItem([FromQuery] int menuItemId)
        {
            menuItemLogic.SoftDelete(menuItemId);
            return Ok();
        }
    }
}
