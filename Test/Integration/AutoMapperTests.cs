using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;

namespace MoE.ECE.Integration.Tests
{
    [Collection(Collections.IntegrationTestCollection)]
    public class AutoMapperTests
    {
        private readonly RunOnceBeforeAllTests _runOnceBeforeAllTests;

        public AutoMapperTests(RunOnceBeforeAllTests runOnceBeforeAllTests) =>
            _runOnceBeforeAllTests = runOnceBeforeAllTests;

        [Fact]
        public void AssertConfigurationIsValid() => _runOnceBeforeAllTests.Services.GetService<IMapper>()
            .ConfigurationProvider.AssertConfigurationIsValid();
    }
}