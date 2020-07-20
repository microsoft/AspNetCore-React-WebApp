using MediatR;
using Microsoft.DSX.ProjectTemplate.Data.Models;

namespace Microsoft.DSX.ProjectTemplate.Data.Events
{
    public class GroupCreatedDomainEvent : INotification
    {
        public Group Group { get; }

        public GroupCreatedDomainEvent(Group group)
        {
            Group = group;
        }
    }
}
