using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoE.ECE.Integration.OpenPolicyAgent.Services
{
    public class FakeOpenPolicyAgentService : IOpenPolicyAgentService
    {
        private readonly ILogger<FakeOpenPolicyAgentService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FakeOpenPolicyAgentService(ILogger<FakeOpenPolicyAgentService> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public Task<bool> IsPermittedAsync(string requiredPermission, HttpRequest httpRequest)
        {
            if (LocalDevelopment.DisableAuthnAndAuthz(_webHostEnvironment))
            {
                _logger.LogWarning($"{nameof(FakeOpenPolicyAgentService)} is being used instead of calling a real {nameof(OpenPolicyAgentService)}. Will always allow all authz.");
                return Task.FromResult(true);
            }

            throw new System.InvalidOperationException($"{nameof(FakeOpenPolicyAgentService)} SHOULD NOT BE USED FOR NON DEBUG BUILDS!");

        }
    }
}