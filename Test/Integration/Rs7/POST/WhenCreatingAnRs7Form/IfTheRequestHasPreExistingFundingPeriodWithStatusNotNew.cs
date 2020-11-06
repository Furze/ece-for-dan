using Bard;
using MoE.ECE.Domain.Event;
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

        private Rs7Updated Rs7PreExistingNotNew
        {
            get => TestData.Rs7Created;
            set => TestData.Rs7Created = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .And_the_rs7_has_been_saved_as_draft()
                .GetResult(result => Rs7PreExistingNotNew = result.Rs7Updated);
        }

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.OrganisationId = Rs7PreExistingNotNew.OrganisationId;
                    rs7.FundingPeriod = Rs7PreExistingNotNew.FundingPeriod;
                    rs7.FundingPeriodYear = Rs7PreExistingNotNew.FundingPeriodYear;
                })
            );
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            A_domain_event_should_be_fired<Rs7Created>()
                .Id
                .ShouldBe(Rs7PreExistingNotNew.Id);
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
                    $"Rs7 for EceService ({Rs7PreExistingNotNew.OrganisationId}) with fundingPeriod {Rs7PreExistingNotNew.FundingPeriod} for year {Rs7PreExistingNotNew.FundingPeriodYear} already exists.");
        }
    }
}