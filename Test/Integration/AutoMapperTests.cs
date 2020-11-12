using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;

namespace MoE.ECE.Integration.Tests
{
    [Collection(Collections.IntegrationTestCollection)]
    public class AutoMapperTests
    {
        public AutoMapperTests(RunOnceBeforeAllTests runOnceBeforeAllTests)
        {
            _runOnceBeforeAllTests = runOnceBeforeAllTests;
        }

        private readonly RunOnceBeforeAllTests _runOnceBeforeAllTests;

        [Fact]
        public void AssertConfigurationIsValid()
        {
            _runOnceBeforeAllTests.Services.GetService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}