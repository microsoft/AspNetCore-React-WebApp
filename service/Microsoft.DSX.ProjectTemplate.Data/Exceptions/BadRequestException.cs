using System.Net;

namespace Microsoft.DSX.ProjectTemplate.Data.Exceptions
{
    public class BadRequestException : ExceptionBase
    {
        private static string DefaultMessageHeader => "Bad Request";

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public BadRequestException(string message, string messageHeader = null)
            : base(message, messageHeader ?? DefaultMessageHeader) { }
    }
}
