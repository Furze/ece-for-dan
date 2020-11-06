using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using IntegrationEvents = MoE.Rolls.Domain.Integration.Events;
using ModelBuilder = MoE.ECE.Integration.Tests.Infrastructure.ModelBuilder;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7ZeroReturn
{
    public class IfTheRequestIsValid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        private readonly int _organisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.OrganisationId = _organisationId;
                    rs7.IsZeroReturn = true;
                }));
        }

        private Rs7ZeroReturnCreated DomainEvent => A_domain_event_should_be_fired<Rs7ZeroReturnCreated>();
        private IntegrationEvents.Rs7.Rs7Updated IntegrationEvent => An_integration_event_should_be_fired<IntegrationEvents.Rs7.Rs7Updated>();

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            IntegrationEvent.ShouldNotBeNull();

            IntegrationEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void ThenADomainEventShouldBePublished()
        {
            DomainEvent.ShouldNotBeNull();

            DomainEvent.Id.ShouldNotBe(0);
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
        public void ThenDomainEventShouldOnlyHaveOriginalRevision()
        {
            DomainEvent.RevisionNumber.ShouldBe(1);
            DomainEvent.RevisionDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenResponseShouldBe201()
        {
            Then.TheResponse.ShouldBe.Created<CreatedAtActionResult>();
        }

        public IfTheRequestIsValid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }
    }
}