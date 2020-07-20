using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.DSX.ProjectTemplate.API.Controllers
{
    /// <summary>
    /// Base controller for our web API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Gets injected Mediator instance.
        /// </summary>
        protected IMediator Mediator { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="mediator">Mediator instance from dependency injection.</param>
        protected BaseController(IMediator mediator)
            : base()
        {
            Mediator = mediator;
        }
    }
}
