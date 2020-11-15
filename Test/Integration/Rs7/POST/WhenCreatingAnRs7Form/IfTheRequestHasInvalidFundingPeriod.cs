using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class IfTheRequestHasInvalidFundingPeriod : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasInvalidFundingPeriod(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Act()
        {
            When.Post(Url, new CreateSkeletonRs7
            {
                FundingPeriod = (FundingPeriodMonth) 5,
                FundingPeriodYear = 2020,
                OrganisationId = 1
            });
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            A_domain_event_should_not_be_fired<Rs7SkeletonCreated>();
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then
                .Response
                .ShouldBe
                .BadRequest
                .ForProperty(nameof(CreateSkeletonRs7.FundingPeriod))
                .WithMessage("5 is invalid. These are the valid options: 3, 7, 11");
        }
    }
}