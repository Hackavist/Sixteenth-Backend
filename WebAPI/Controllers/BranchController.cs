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
    public class BranchController : ControllerBase
    {
        private readonly IBranchLogic branchLogic;

        public BranchController(IBranchLogic BranchLogic)
        {
            branchLogic = BranchLogic;
        }

        /// <summary>
        ///     Gets all branches
        /// </summary>
        [ProducesResponseType(200, Type = typeof(IEnumerable<BranchResponseDTO>))]
        [HttpGet]
        public IActionResult GetBranches()
        {
            return Ok(branchLogic.GetAll().ToList());
        }

        /// <summary>
        ///     Gets a branch by Id
        /// </summary>
        /// <param name="branchId">The Branch Request Object</param>
        [ProducesResponseType(200, Type = typeof(BranchResponseDTO))]
        [HttpGet("ById")]
        public IActionResult GetBranch([FromQuery] int branchId)
        {
            return Ok(branchLogic.Get(branchId));
        }

        /// <summary>
        ///     Inserts a new branch and returns its Id
        /// </summary>
        /// <param name="branch">The Branch Request Object</param>
        /// <response code="200">Successfully Inserted the branch, Returns The Branch Id</response>
        /// <response code="400">Error inserting the branch, please make sure that the data is correct</response>
        [ProducesResponseType(201, Type = typeof(int))]
        [ProducesResponseType(400, Type = null)]
        [HttpPost]
        public IActionResult InsertBranch([FromBody] BranchRequestDTO branch)
        {
            int ret = branchLogic.Insert(branch);
            return ret > 0
                ? (IActionResult) Ok(ret)
                : BadRequest();
        }

        /// <summary>
        ///     Inserts a new branches and returns Their Ids
        /// </summary>
        /// <param name="branches">The Branch Request Objects</param>
        /// <response code="200">Successfully Inserted the branches, Returns The Branch Ids</response>
        /// <response code="400">Error inserting the branch, please make sure that the data is correct</response>
        [ProducesResponseType(201, Type = typeof(IEnumerable<int>))]
        [ProducesResponseType(400, Type = null)]
        [HttpPost("Range")]
        public IActionResult InsertBranches([FromBody] IEnumerable<BranchRequestDTO> branches)
        {
            var ret = branchLogic.InsertRange(branches);
            if (ret.Any(id => id <= 0)) return BadRequest();
            return Ok(ret);
        }

        /// <summary>
        ///     Updates Branch info in the database
        /// </summary>
        /// <param name="branch">The Branch Request Object</param>
        /// <param name="branchId">The Branch Id </param>
        /// <response code="200">Successfully updated the Branch</response>
        [ProducesResponseType(200, Type = null)]
        [HttpPut]
        public IActionResult UpdateBranch([FromQuery] int branchId, [FromBody] BranchRequestDTO branch)
        {
            branchLogic.Update(branchId, branch);
            return Ok();
        }

        /// <summary>
        ///     Deleting a branch using its Id
        /// </summary>
        /// <param name="branchId">The Branch Id </param>
        /// <response code="200">Branch was successfully deleted from the database</response>
        [ProducesResponseType(200, Type = null)]
        [HttpDelete]
        public IActionResult DeleteBranch([FromQuery] int branchId)
        {
            branchLogic.SoftDelete(branchId);
            return Ok();
        }
    }
}