using System.Threading.Tasks;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public interface IOpaTokenGenerator
    {
        Task<OpaAccessToken?> GenerateAsync();
    }
}