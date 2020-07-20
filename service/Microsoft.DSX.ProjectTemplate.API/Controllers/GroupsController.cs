using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command.Group;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.API.Controllers
{
    /// <summary>
    /// Controller for Group APIs.
    /// </summary>
    public class GroupsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance from dependency injection.</param>
        public GroupsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Get all Groups.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetAllGroups()
        {
            return Ok(await Mediator.Send(new GetAllGroupsQuery()));
        }

        /// <summary>
        /// Get a Group by its Id.
        /// </summary>
        /// <param name="id">ID of the Group to get.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(int id)
        {
            return Ok(await Mediator.Send(new GetGroupByIdQuery() { GroupId = id }));
        }

        /// <summary>
        /// Create a new Group.
        /// </summary>
        /// <param name="dto">A Group DTO.</param>
        [HttpPost]
        public async Task<ActionResult<GroupDto>> CreateGroup([FromBody] GroupDto dto)
        {
            return Ok(await Mediator.Send(new CreateGroupCommand() { Group = dto }));
        }

        /// <summary>
        /// Update an existing Group.
        /// </summary>
        /// <param name="dto">Updated Group DTO.</param>
        [HttpPut]
        public async Task<ActionResult<GroupDto>> UpdateGroup([FromBody] GroupDto dto)
        {
            return Ok(await Mediator.Send(new UpdateGroupCommand() { Group = dto }));
        }

        /// <summary>
        /// Delete an existing Group.
        /// </summary>
        /// <param name="id">Id of the Group to be deleted.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupDto>> DeleteGroup([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteGroupCommand() { GroupId = id }));
        }
    }
}
