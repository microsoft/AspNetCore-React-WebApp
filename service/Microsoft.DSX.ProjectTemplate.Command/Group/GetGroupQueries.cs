using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Group
{
    public class GetAllGroupsQuery : IRequest<IEnumerable<GroupDto>> { }

    public class GetGroupByIdQuery : IRequest<GroupDto>
    {
        public int GroupId { get; set; }
    }

    public class GroupQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllGroupsQuery, IEnumerable<GroupDto>>,
        IRequestHandler<GetGroupByIdQuery, GroupDto>
    {
        public GroupQueryHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        // GET ALL
        public async Task<IEnumerable<GroupDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            return await Database.Groups
                .Select(x => Mapper.Map<GroupDto>(x))
                .ToListAsync();
        }

        // GET BY ID
        public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.GroupId <= 0)
            {
                throw new BadRequestException($"A valid {nameof(Data.Models.Group)} Id must be provided.");
            }

            var innerResult = await Database.Groups
                .FindAsync(request.GroupId);

            if (innerResult == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Group)} with Id {request.GroupId} cannot be found.");
            }

            return Mapper.Map<GroupDto>(innerResult);
        }
    }
}
