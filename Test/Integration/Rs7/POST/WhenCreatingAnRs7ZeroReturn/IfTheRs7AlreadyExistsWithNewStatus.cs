using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7ZeroReturn
{
    public class IfTheRs7AlreadyExistsWithNewStatus : IntegrationTestBase
    {
        public IfTheRs7AlreadyExistsWithNewStatus(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private readonly int _organisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        private Rs7Created _previouslyStartedNewRs7 = new Rs7Created();

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created(setup => setup.OrganisationId = _organisationId)
                .UseResult(result => _previouslyStartedNewRs7 = result);
        }

        protected override void Act()
        {
            Api.Post(Url,
                Command.Rs7Model(rs7 =>
                {
                    rs7.IsZeroReturn = true;
                    rs7.OrganisationId = _previouslyStartedNewRs7.OrganisationId;
                    rs7.FundingPeriod = _previouslyStartedNewRs7.FundingPeriod;
                    rs7.FundingPeriodYear = _previouslyStartedNewRs7.FundingPeriodYear;
                }));
        }

        private Rs7ZeroReturnCreated DomainEvent => Then.A_domain_event_should_be_fired<Rs7ZeroReturnCreated>();

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
        public void ThenResponseShouldBe201()
        {
            Then.TheResponse.ShouldBe.Created<CreatedAtActionResult>();
        }
    }
}