using System.Net;

namespace Microsoft.DSX.ProjectTemplate.Data.Exceptions
{
    public class EntityNotFoundException : ExceptionBase
    {
        private static string DefaultMessageHeader => "Not found";

        public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public EntityNotFoundException(string message, string messageHeader = null)
            : base(message, messageHeader ?? DefaultMessageHeader) { }
    }
}
