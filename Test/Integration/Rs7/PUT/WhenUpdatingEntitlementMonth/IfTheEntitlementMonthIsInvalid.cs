using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingEntitlementMonth
{
    public class IfTheEntitlementMonthIsInvalid : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheEntitlementMonthIsInvalid(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(result => Rs7Model = result);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private void Act(UpdateRs7EntitlementMonth updateRs7EntitlementMonthCommand)
        {
            Api.Put($"{Url}/{Rs7Model.Id}/entitlement-months/{updateRs7EntitlementMonthCommand.MonthNumber}",
                updateRs7EntitlementMonthCommand);
        }


        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Year = 2021;
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }

        [Fact]
        public void IfTheMonthIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.MonthNumber = 6;
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }

        [Fact]
        public void IfTheDayNumberIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().DayNumber = 100; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1040");
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Under2 = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Under2 = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }


        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().TwoAndOver = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().TwoAndOver = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Hours20 = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Hours20 = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Certificated = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().Certificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().NonCertificated = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().NonCertificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = Command.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                em.Days.First().NonCertificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.A_domain_event_should_not_be_fired<Rs7EntitlementMonthUpdated>();
        }
    }
}