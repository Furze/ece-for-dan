using Bard;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using ModelBuilder = MoE.ECE.Integration.Tests.Infrastructure.ModelBuilder;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7ZeroReturn
{
    public class IfTheRs7AlreadyExistsWithNewStatus : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        private const string Url = "api/rs7";
        private readonly int _organisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        private Rs7Model _previouslyStartedNewRs7 = new Rs7Model();

        protected override void Arrange()
        {
            Given
                .A_rs7_skeleton_has_been_created(setup => setup.OrganisationId = _organisationId)
                .GetResult(result => _previouslyStartedNewRs7 = result.Rs7Model);
        }

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.IsZeroReturn = true;
                    rs7.OrganisationId = _previouslyStartedNewRs7.OrganisationId;
                    rs7.FundingPeriod = _previouslyStartedNewRs7.FundingPeriod;
                    rs7.FundingPeriodYear = _previouslyStartedNewRs7.FundingPeriodYear;
                }));
        }

        private Rs7ZeroReturnCreated DomainEvent => A_domain_event_should_be_fired<Rs7ZeroReturnCreated>();

        [Fact]
        public void ThenDomainEventShouldBePublishedUsingThePreExistingId()
        {
            DomainEvent.ShouldNotBeNull();
            DomainEvent.Id.ShouldBe(_previouslyStartedNewRs7.Id);
        }

        [Fact]
        public void ThenDomainEventShouldHaveRollStatusInternalReadyForReview()
        {
            DomainEvent.RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
        }

        [Fact]
        public void ThenDomainEventShouldHaveAReceivedDate()
        {
            DomainEvent.ReceivedDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenDomainEventShouldBeZeroReturn()
        {
            DomainEvent.IsZeroReturn.ShouldBe(true);
        }

        [Fact]
        public void ThenDomainEventShouldBeAttestedFalse()
        {
            DomainEvent.IsAttested.ShouldBe(false);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriod()
        {
            DomainEvent.FundingPeriod.ShouldBe(FundingPeriodMonth.July);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingYear()
        {
            DomainEvent.FundingYear.ShouldBe(2021);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriodYear()
        {
            DomainEvent.FundingPeriodYear.ShouldBe(2020);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectOrganisation()
        {
            DomainEvent.OrganisationId.ShouldBe(_organisationId);
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

        public IfTheRs7AlreadyExistsWithNewStatus(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }
    }
}