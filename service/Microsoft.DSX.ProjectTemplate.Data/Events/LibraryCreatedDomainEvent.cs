using MediatR;
using Microsoft.DSX.ProjectTemplate.Data.Models;

namespace Microsoft.DSX.ProjectTemplate.Data.Events
{
    public class LibraryCreatedDomainEvent : INotification
    {
        public Library Library { get; }

        public LibraryCreatedDomainEvent(Library library)
        {
            Library = library;
        }
    }
}
