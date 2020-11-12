using System.Text.Json;
using System.Text.Json.Serialization;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Rs7;
using Shouldly;
using Xunit;

namespace MoE.ECE.Integration.Tests
{
    public class PlayPen
    {
        [Fact]
        public void Foo()
        {
            var foo =
                "{\n    \"data\": [{\n            \"id\": 129001,\n            \"businessEntityId\": \"8a201f2c-62a7-461d-9f87-a3cb129bea09\",\n            \"organisationId\": 114895,\n            \"fundingPeriod\": 7,\n            \"fundingYear\": 2021,\n            \"fundingPeriodYear\": 2020,\n            \"isZeroReturn\": false,\n            \"rollStatus\": \"ExternalNew\",\n            \"receivedDate\": null,\n            \"revisionNumber\": 1,\n            \"revisionDate\": \"2020-11-12T11:10:41.3198622+13:00\",         \n            \"isAttested\": null,\n            \"declaration\": null,\n            \"requestId\": \"129001\"\n        }\n    ],\n    \"pagination\": {\n        \"pageSize\": 10,\n        \"pageNumber\": 1,\n        \"count\": 1\n    }\n}\n";
               
            
            var baa= JsonSerializer.Deserialize<CollectionModel<Rs7Model>>(foo, new JsonSerializerOptions
            {
               // IgnoreNullValues = true,
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)},
                PropertyNameCaseInsensitive = true
            });
            
            baa.ShouldNotBeNull();
        }
    }
}