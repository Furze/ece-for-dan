using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class IfTheRequestIsValid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        private readonly int _organisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        public IfTheRequestIsValid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7SkeletonCreated DomainEvent => A_domain_event_should_be_fired<Rs7SkeletonCreated>();

        protected override void Act() =>
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                    rs7.OrganisationId = _organisationId));

        [Fact]
        public void ThenADomainEventShouldBePublished()
        {
            DomainEvent.ShouldNotBeNull();

            DomainEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void ThenDomainEventShouldBeZeroReturn() => DomainEvent.IsZeroReturn.ShouldBe(false);

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriod() =>
            DomainEvent.FundingPeriod.ShouldBe(FundingPeriodMonth.July);

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriodYear() => DomainEvent.FundingPeriodYear.ShouldBe(2020);

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingYear() => DomainEvent.FundingYear.ShouldBe(2021);

        [Fact]
        public void ThenDomainEventShouldHaveCorrectOrganisation() =>
            DomainEvent.OrganisationId.ShouldBe(_organisationId);

        [Fact]
        public void ThenDomainEventShouldHaveRollStatusNew() => DomainEvent.RollStatus.ShouldBe(RollStatus.ExternalNew);

        [Fact]
        public void ThenDomainEventShouldHaveSourceInternal() => DomainEvent.Source.ShouldBe(Source.Internal);

        [Fact]
        public void Then_the_response_should_be_a_201_created() => Then.Response.ShouldBe.Created();

        [Fact]
        public void Then_the_response_should_contain_a_location_header() =>
            Then.Response.Headers.Should.Include.Location();
    }
}