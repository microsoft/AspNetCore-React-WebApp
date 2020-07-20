using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Group
{
    public class DeleteGroupCommand : IRequest<bool>
    {
        public int GroupId { get; set; }
    }

    public class DeleteGroupCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteGroupCommand, bool>
    {
        public DeleteGroupCommandHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            if (request.GroupId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Group)} Id must be provided.");
            }

            var group = await Database.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Group)} not found.");
            }

            Database.Groups.Remove(group);

            await Database.SaveChangesAsync();

            Debug.Assert(Database.Groups.Find(group.Id) == null);

            return true;
        }
    }
}
