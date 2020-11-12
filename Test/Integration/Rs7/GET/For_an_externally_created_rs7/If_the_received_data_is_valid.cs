using System;
using System.Linq;
using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using Rs7Updated = Events.Integration.Protobuf.Roll.Rs7Updated;

namespace MoE.ECE.Integration.Tests.Rs7.GET.For_an_externally_created_rs7
{
    public class If_the_received_data_is_valid : SpeedyIntegrationTestBase
    {
        public If_the_received_data_is_valid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange()
        {
            Given
                .An_rs7_has_been_received_externally()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        protected override void Act()
        {
            When.Get($"{Url}/{Rs7Model.Id}");
        }

        [Fact]
        public void Then_an_integration_event_should_have_been_fired()
        {
            An_integration_event_should_be_fired<Rs7Updated>();
        }

        [Fact]
        public void Then_the_domain_event_should_have_populated_the_advanced_months_correctly()
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
        public void Then_the_domain_event_should_have_populated_the_entitlement_months_correctly()
        {
            var domainEvent = A_domain_event_should_be_fired<Rs7CreatedFromExternal>();

            domainEvent.AdvanceMonths.ShouldNotBeNull();

            var entitlementMonth = domainEvent.EntitlementMonths?.FirstOrDefault();

            entitlementMonth.ShouldNotBeNull();
            entitlementMonth?.MonthNumber.ShouldBe(2);
            entitlementMonth?.Year.ShouldBe(2020);

            entitlementMonth?.Days.ShouldNotBeNull();

            var entitlementDay = entitlementMonth?.Days?.First();
            entitlementDay?.DayNumber.ShouldBe(1);
            entitlementDay?.Under2.ShouldBe(5);
            entitlementDay?.TwoAndOver.ShouldBe(3);
            entitlementDay?.Plus10.ShouldBe(4);
            entitlementDay?.Hours20.ShouldBe(2);
            entitlementDay?.Certificated.ShouldBe(5);
            entitlementDay?.NonCertificated.ShouldBe(6);
        }

        [Fact]
        public void Then_the_domain_event_should_have_populated_the_source_correctly()
        {
            A_domain_event_should_be_fired<Rs7CreatedFromExternal>()
                .Source.ShouldBe("Uranus");
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
    }
}