using Bard;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Domain.Event;
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

        private Rs7Created _rs7 = new Rs7Created();

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(data => _rs7 = data.Rs7Created ?? new Rs7Created());
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
        public void ThenResponseShouldBe201()
        {
            Then.Response.ShouldBe.Created<CreatedAtActionResult>();
        }
    }
}