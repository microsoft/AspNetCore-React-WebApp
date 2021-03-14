
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DSX.ProjectTemplate.Data;
using Microsoft.DSX.ProjectTemplate.Data.DTOs;
using Microsoft.DSX.ProjectTemplate.Data.Events;
using Microsoft.DSX.ProjectTemplate.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Command.Library
{
    public class CreateLibraryCommand : IRequest<LibraryDto>
    {
        public LibraryDto Library { get; set; }
    }

    public class CreateLibraryCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateLibraryCommand, LibraryDto>
    {
        public CreateLibraryCommandHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        public async Task<LibraryDto> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Library;

            bool nameAlreadyUsed = await Database.Libraries.AnyAsync(e => e.Name.Trim() == dto.Name.Trim(), cancellationToken);
            if (nameAlreadyUsed)
            {
                throw new BadRequestException($"{nameof(dto.Name)} '{dto.Name}' already used.");
            }

            var model = new Data.Models.Library()
            {
                Name = dto.Name,
                Address = new Data.Models.Address() {
                    AddressLine1 = dto.Address_AddressLine1,
                    AddressLine2 = dto.Address_AddressLine2,
                    City = dto.Address_City,
                    StateProvince = dto.Address_StateProvince,
                    ZipCode = dto.Address_ZipCode,
                    Country = dto.Address_Country
                }
            };

            Database.Libraries.Add(model);

            await Database.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new LibraryCreatedDomainEvent(model), cancellationToken);

            return Mapper.Map<LibraryDto>(model);
        }
    }
}
