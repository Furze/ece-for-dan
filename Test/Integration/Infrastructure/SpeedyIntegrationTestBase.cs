using MoE.ECE.Integration.Tests.Chapter;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class SpeedyIntegrationTestBase : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public SpeedyIntegrationTestBase(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }
        
        protected override void Initialize()
        {
            if (TestState.HasRun == false)
            {
                RunTestInitialization();
            }
        }
    }
}