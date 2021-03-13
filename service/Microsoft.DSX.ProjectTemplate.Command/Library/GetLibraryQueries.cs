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

namespace Microsoft.DSX.ProjectTemplate.Command.Library
{
    public class GetAllLibrariesQuery : IRequest<IEnumerable<LibraryDto>> { }

   

    public class LibraryQueryHandler : QueryHandlerBase,
        IRequestHandler<GetAllLibrariesQuery, IEnumerable<LibraryDto>>
        
    {
        public LibraryQueryHandler(
            IMediator mediator,
            ProjectTemplateDbContext database,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(mediator, database, mapper, authorizationService)
        {
        }

        // GET ALL
        public async Task<IEnumerable<LibraryDto>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
        {
            return await Database.Libraries
                .Select(x => Mapper.Map<LibraryDto>(x))
                .ToListAsync(cancellationToken);
        }

       

           
        
    }
}
