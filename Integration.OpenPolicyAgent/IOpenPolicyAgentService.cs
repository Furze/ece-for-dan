using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MoE.ECE.Integration.OpenPolicyAgent
{
    public interface IOpenPolicyAgentService
    {
        Task<bool> IsPermittedAsync(string requiredPermission, HttpRequest httpRequest);
    }
}
