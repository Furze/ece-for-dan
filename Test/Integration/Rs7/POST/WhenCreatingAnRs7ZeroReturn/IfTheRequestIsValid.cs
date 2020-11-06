﻿using Microsoft.AspNetCore.Mvc;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = Events.Integration.Protobuf.Roll.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7ZeroReturn
{
    public class IfTheRequestIsValid : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsValid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

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
        private Rs7Updated IntegrationEvent => An_integration_event_should_be_fired<Rs7Updated>();

        [Fact]
        public void ThenADomainEventShouldBePublished()
        {
            DomainEvent.ShouldNotBeNull();

            DomainEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void ThenAnIntegrationEventShouldBePublished()
        {
            IntegrationEvent.ShouldNotBeNull();

            IntegrationEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void ThenDomainEventShouldBeAttestedFalse()
        {
            DomainEvent.IsAttested.ShouldBe(false);
        }

        [Fact]
        public void ThenDomainEventShouldBeZeroReturn()
        {
            DomainEvent.IsZeroReturn.ShouldBe(true);
        }

        [Fact]
        public void ThenDomainEventShouldHaveAReceivedDate()
        {
            DomainEvent.ReceivedDate.ShouldNotBeNull();
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriod()
        {
            DomainEvent.FundingPeriod.ShouldBe(FundingPeriodMonth.July);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingPeriodYear()
        {
            DomainEvent.FundingPeriodYear.ShouldBe(2020);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectFundingYear()
        {
            DomainEvent.FundingYear.ShouldBe(2021);
        }

        [Fact]
        public void ThenDomainEventShouldHaveCorrectOrganisation()
        {
            DomainEvent.OrganisationId.ShouldBe(_organisationId);
        }

        [Fact]
        public void ThenDomainEventShouldHaveRollStatusInternalReadyForReview()
        {
            DomainEvent.RollStatus.ShouldBe(RollStatus.InternalReadyForReview);
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
            Then.Response.ShouldBe.Created<CreatedAtActionResult>();
        }
    }
}