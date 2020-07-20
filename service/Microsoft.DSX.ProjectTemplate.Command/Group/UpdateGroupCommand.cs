using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Group
{
    public class UpdateGroupCommand : IRequest<GroupDto>
    {
        public GroupDto Group { get; set; }
    }

    public class UpdateGroupCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateGroupCommand, GroupDto>
    {
        public UpdateGroupCommandHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Group;

            if (dto.Id <= 0)
            {
                throw new BadRequestException($"{nameof(Data.Models.Group)} Id must be greater than zero.");
            }

            var model = await Database.Groups
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityNotFoundException($"{nameof(Data.Models.Group)} with Id {dto.Id} not found.");
            }

            // ensure uniqueness of name
            bool nameAlreadyUsed = await Database.Groups
                .AnyAsync(e => e.Name.Trim() == dto.Name.Trim()) && dto.Name != (model.Name);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.Name)} {dto.Name} already used.");
            }

            model.Name = dto.Name;
            model.IsActive = dto.IsActive;

            Database.Groups.Update(model);

            await Database.SaveChangesAsync();

            return Mapper.Map<GroupDto>(model);
        }
    }
}
