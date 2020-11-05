using Bard;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests
{
    public class UnitTest1 : IntegrationTestBase<TestStoryBook, TestStoryData>
    {
        [Fact]
        public void Test1()
        {
        }

        public UnitTest1(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<TestStoryBook, TestStoryData> testState) : base(testSetUp, output, testState)
        {
        }
    }

    public class TestStoryData 
    {
    }

    public class TestStoryBook : StoryBook<TestStoryData>
    {
    }
}