using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace Microsoft.DSX.ProjectTemplate.Test.Tests
{
    public abstract class BaseTest
    {
        /// <summary>Ensures an <see cref="HttpResponseMessage"/> can be deserialized into the specified <typeparamref name="TObject"/> type.</summary>
        /// <typeparam name="TObject">What to deserialize the response to</typeparam>
        /// <param name="response">The response from the service</param>
        protected async Task<TObject> EnsureObject<TObject>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().NotBeNullOrWhiteSpace();

            return JsonConvert.DeserializeObject<TObject>(responseBody);
        }
    }
}
