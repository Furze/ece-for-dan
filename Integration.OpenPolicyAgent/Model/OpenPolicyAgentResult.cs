using Newtonsoft.Json;

namespace MoE.ECE.Integration.OpenPolicyAgent.Model
{
    public class OpenPolicyAgentResult<T> where T : new()
    {
        [JsonProperty("result")]
        public T Result { get; set; } = new T();
    }
}