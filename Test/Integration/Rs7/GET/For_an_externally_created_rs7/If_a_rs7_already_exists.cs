using System;
using System.Linq;
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

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_an_externally_created_rs7
{
    public class If_a_rs7_already_exists : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        public If_a_rs7_already_exists(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created(setup =>
                {
                    setup.OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;
                    setup.FundingPeriod = FundingPeriodMonth.July;
                    setup.FundingPeriodYear = 2020;
                })
                .GetResult(storyData => PreExistingRs7 = storyData.Rs7Model);
                
            And
                .An_rs7_has_been_received_externally()
                .GetResult(storyData => ExternallyReceivedRs7 = storyData.Rs7Model);
        }
        
        private Rs7Model PreExistingRs7
        {
            get => TestData.PreExistingRs7;
            set => TestData.PreExistingRs7 = value;
        }
        
        private Rs7Model ExternallyReceivedRs7
        {
            get => TestData.ExternallyReceivedRs7;
            set => TestData.ExternallyReceivedRs7 = value;
        }
        
        protected override void Act()
        {
            When.Get($"{Url}/{ExternallyReceivedRs7.Id}");
        }
        
        [Fact]
        public void Then_the_response_should_be_ok()
        {
            Then.Response.ShouldBe.Ok();
        }
        
        [Fact]
        public void Then_the_response_snapshot_should_be_ok()
        {
            Then.Snapshot().Match<Rs7Model>();
        }
        
        [Fact]
            public void ExpectDomainEventShouldBePublishedUsingThePreExistingId()
            {
                var domainEvent = A_domain_event_should_be_fired<Rs7CreatedFromExternal>();
        
                domainEvent.ShouldNotBeNull();
                domainEvent.Id.ShouldBe(PreExistingRs7.Id);
            }
        
            [Fact]
            public void ExpectDomainEventShouldHavePopulatedAdvanceMonthsFromTheCommand()
            {
                var domainEvent = A_domain_event_should_be_fired<Rs7CreatedFromExternal>();
                
                domainEvent.AdvanceMonths.ShouldNotBeNull();
                var firstAdvanceMonth = domainEvent.AdvanceMonths?.FirstOrDefault();
        
                firstAdvanceMonth.ShouldNotBeNull();
                
                firstAdvanceMonth?.MonthNumber.ShouldBe(7);
                firstAdvanceMonth?.Year.ShouldBe(2020);
                firstAdvanceMonth?.AllDay.ShouldBe(2);
                firstAdvanceMonth?.ParentLed.ShouldBe(4);
                firstAdvanceMonth?.Sessional.ShouldBe(6);
            }
        
            [Fact]
            public void ExpectDomainEventShouldHavePopulatedEntitlementMonthFromTheCommand()
            {
                var domainEvent = A_domain_event_should_be_fired<Rs7CreatedFromExternal>();
                
                domainEvent.EntitlementMonths.ShouldNotBeNull();
                var entitlementMonth = domainEvent.EntitlementMonths?.FirstOrDefault();
        
                entitlementMonth.ShouldNotBeNull();
                entitlementMonth?.MonthNumber.ShouldBe(2);
                entitlementMonth?.Year.ShouldBe(2020);
        
                entitlementMonth?.Days.ShouldNotBeNull();
                
                var entitlementDay = entitlementMonth?.Days?.FirstOrDefault();
                
                entitlementDay.ShouldNotBeNull();
                entitlementDay?.DayNumber.ShouldBe(1);
                entitlementDay?.Under2.ShouldBe(5);
                entitlementDay?.TwoAndOver.ShouldBe(3);
                entitlementDay?.Plus10.ShouldBe(4);
                entitlementDay?.Hours20.ShouldBe(2);
                entitlementDay?.Certificated.ShouldBe(5);
                entitlementDay?.NonCertificated.ShouldBe(6);
            }
        
            [Fact]
            public void ExpectAnRs7CreatedFromExternalDomainEventWithSource()
            {
                A_domain_event_should_be_fired<Rs7CreatedFromExternal>()
                    .Source.ShouldBe("Uranus");
            }
        
            [Fact]
            public void ExpectAnRs7UpdatedIntegrationEventRaised()
            {
                An_integration_event_should_be_fired<Events.Integration.Protobuf.Roll.Rs7Updated>();
            }
    }
}