using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using Polly.Wrap;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    public class ProxyController : ControllerBase
    {
        private readonly AsyncFallbackPolicy<IActionResult> _fallbackPolicy;
        private readonly HttpClient _httpClient;

        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _fallbackPolicy = Policy<IActionResult>
                .Handle<Exception>()
                .FallbackAsync(Content("Sorry, we are currently experiencing issues. Please try again later"));

            _httpClient = httpClientFactory.CreateClient();
        }

        private async Task<IActionResult> ProxyTo(string url)
            => await _fallbackPolicy.ExecuteAsync(async () => Content(await _httpClient.GetStringAsync(url)));
    }
}
