using System;
using System.Threading.Tasks;
using JustEat.HttpClientInterception;
using Xunit;
using Xunit.Abstractions;

namespace MonoBug
{
    public sealed class MonoBugTest
    {
        [Fact]
        public async Task Test()
        {
            var options = new HttpClientInterceptorOptions();

            var builder = new HttpRequestInterceptionBuilder()
                .Requests()
                .ForHost("public.je-apis.com")
                .ForPath("terms")
                .Responds()
                .WithJsonContent(new { Id = 1, Link = "https://www.just-eat.co.uk/privacy-policy" })
                .RegisterWith(options);

            var client = options.CreateHttpClient();

            // The value of json will be "{\"Id\":1,\"Link\":\"https://www.just-eat.co.uk/privacy-policy\"}"
            var json = await client.GetStringAsync("http://public.je-apis.com/terms");
        }
    }
}
