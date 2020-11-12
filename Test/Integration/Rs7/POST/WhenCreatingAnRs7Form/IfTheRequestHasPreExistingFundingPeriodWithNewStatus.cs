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
    public class IfTheRequestHasPreExistingFundingPeriodWithNewStatus : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public IfTheRequestHasPreExistingFundingPeriodWithNewStatus(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model _rs7 = new Rs7Model();

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(data => _rs7 = data.Rs7Model);
        }

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.OrganisationId = _rs7.OrganisationId;
                    rs7.FundingPeriod = _rs7.FundingPeriod;
                    rs7.FundingPeriodYear = _rs7.FundingPeriodYear;
                })
            );
        }

        [Fact]
        public void ThenDomainEventShouldBePublishedUsingThePreExistingId()
        {
            A_domain_event_should_be_fired<Rs7Created>()
                .Id
                .ShouldBe(_rs7.Id);
        }

        [Fact]
        public void Then_the_response_should_be_a_201_created()
        {
            Then.Response.ShouldBe.Created();
        }
        
        [Fact]
        public void Then_the_response_should_contain_a_location_header()
        {
            Then.Response.Headers.Should.Include.Location();
        }
    }
}