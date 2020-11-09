using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class IfTheRequestHasPreExistingFundingPeriodWithStatusNotNew : SpeedyIntegrationTestBase
    {
        public IfTheRequestHasPreExistingFundingPeriodWithStatusNotNew(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .And_the_rs7_has_been_saved_as_draft()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.OrganisationId = Rs7Model.OrganisationId;
                    rs7.FundingPeriod = Rs7Model.FundingPeriod;
                    rs7.FundingPeriodYear = Rs7Model.FundingPeriodYear;
                })
            );
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            A_domain_event_should_be_fired<Rs7Created>()
                .Id
                .ShouldBe(Rs7Model.Id);
            //The preExisting Id should be the only Rs7Created event fired
        }

        [Fact]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then
                .Response
                .ShouldBe
                .BadRequest
                .WithMessage(
                    $"Rs7 for EceService ({Rs7Model.OrganisationId}) with fundingPeriod {Rs7Model.FundingPeriod} for year {Rs7Model.FundingPeriodYear} already exists.");
        }
    }
}