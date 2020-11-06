using Microsoft.AspNetCore.Mvc;
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
                    rs7.OrganisationId = _organisationId));
        }

        private Rs7Created DomainEvent => A_domain_event_should_be_fired<Rs7Created>();

        [Fact]
        public void ThenADomainEventShouldBePublished()
        {
            DomainEvent.ShouldNotBeNull();

            DomainEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void ThenDomainEventShouldBeZeroReturn()
        {
            DomainEvent.IsZeroReturn.ShouldBe(false);
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
        public void ThenDomainEventShouldHaveRollStatusNew()
        {
            DomainEvent.RollStatus.ShouldBe(RollStatus.ExternalNew);
        }

        [Fact]
        public void ThenDomainEventShouldHaveSourceInternal()
        {
            DomainEvent.Source.ShouldBe(Source.Internal);
        }

        [Fact]
        public void ThenResponseShouldBe201()
        {
            Then.Response.ShouldBe.Created<CreatedAtActionResult>();
        }
    }
}