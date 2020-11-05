using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MoE.ECE.Web.Infrastructure.Opa
{
    public class OpaClient : IOpaClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOpaTokenGenerator _tokenGenerator;
        private readonly ILogger<OpaClient> _logger;

        public OpaClient(
            HttpClient httpClient,
            IOpaTokenGenerator tokenGenerator,
            ILogger<OpaClient> logger)
        {
            _httpClient = httpClient;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }

        public async Task<OpaResponse<TResponse>> PostRequest<TRequest, TResponse>(OpaRequest<TRequest> request, string endpointUrl) where TResponse : class
        {
            var accessToken = await _tokenGenerator.GenerateAsync();

            if (accessToken == null)
                throw new ApplicationException("Unable to obtain access token from OPA.");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.TokenValue);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            _logger.LogInformation($"Request to be sent to OPA: {JsonConvert.SerializeObject(request, settings)}");

            var httpContent = new StringContent(
                JsonConvert.SerializeObject(request, settings),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            var response = await _httpClient.PostAsync(endpointUrl, httpContent);

            _logger.LogInformation($"Request response received from OPA, status code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //OPA sometimes returns invalid json where it returns an empty object instead of 0 or null
                //which breaks the Deserializing
                if (responseContent.Contains("{}"))
                {
                    _logger.LogError($"OPA response returned with success code but the response contains an empty object which breaks the deserializing, response received from OPA: {responseContent}");
                    responseContent = responseContent.Replace("{}", "null");
                }

                try
                {
                    _logger.LogInformation($"Request response received from OPA: {responseContent}");
                    return JsonConvert.DeserializeObject<OpaResponse<TResponse>>(responseContent);
                }
                catch (Exception ex)
                {
                    var serializedRequest = JsonConvert.SerializeObject(request, settings);
                    //logging here as the throw error is not logging to app insights for some reason,
                    //will leave this here until we can figure out why
                    _logger.LogError($"Received a {response.StatusCode} but failed to deserialize the OPA response with error {ex.Message}. OPA raw response: {responseContent}. OPA request: {serializedRequest}");

                    throw new ApplicationException($"Received a {response.StatusCode} but failed to deserialize the OPA response with error {ex.Message}. OPA raw response: {responseContent}. OPA request: {serializedRequest}");
                }
            }

            var opaResponseBody = "";
            try
            {
                opaResponseBody = await response.Content.ReadAsStringAsync();
            }
            catch 
            {
                // NOOP
            }
            
            var applicationException = new ApplicationException($"Received a {response.StatusCode} response from OPA.");
            _logger.LogError("OPA request error", new
            {
                ResponseStatus = response.StatusCode, 
                ResponseBody = opaResponseBody,
                Request = request
            }, applicationException);
            
            throw applicationException;
        }
    }
}
