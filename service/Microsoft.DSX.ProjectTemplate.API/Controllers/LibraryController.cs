using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DSX.ProjectTemplate.Command.Library;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using MediatR;



namespace Microsoft.DSX.ProjectTemplate.API.Controllers
{
    public class LibraryController : BaseController
    {
        public LibraryController(IMediator mediator) : base(mediator) { }

       //GetAllLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> GetAllLibraries()
        {
            return Ok(await Mediator.Send(new GetAllLibrariesQuery()));
        }
    }
}
