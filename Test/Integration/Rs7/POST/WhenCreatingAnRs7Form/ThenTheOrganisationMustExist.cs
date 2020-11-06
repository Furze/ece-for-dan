using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class ThenTheOrganisationMustExist : SpeedyIntegrationTestBase
    {
        public ThenTheOrganisationMustExist(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const int SomeJunkId = 1;

        private const string Url = "api/rs7";

        protected override void Act()
        {
            When.Post(Url, ModelBuilder.Rs7Model(rs7 => rs7.OrganisationId = SomeJunkId));
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then
                .Response
                .ShouldBe
                .BadRequest
                .ForProperty<CreateRs7>(rs7 => rs7.OrganisationId);
        }
    }
}