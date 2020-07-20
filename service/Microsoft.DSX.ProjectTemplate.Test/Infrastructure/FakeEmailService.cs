using Microsoft.DSX.ProjectTemplate.Data.Abstractions;
using System.Threading.Tasks;

namespace Microsoft.DSX.ProjectTemplate.Test.Infrastructure
{
    internal class FakeEmailService : IEmailService
    {
        private int m_sentCount;

        public FakeEmailService()
        {
            m_sentCount = 0;
        }

        public Task SendEmailAsync(string from, string to, string subject, string body)
        {
            m_sentCount++;

            return Task.CompletedTask;
        }

        public int GetSentCount()
        {
            return m_sentCount;
        }
    }
}
