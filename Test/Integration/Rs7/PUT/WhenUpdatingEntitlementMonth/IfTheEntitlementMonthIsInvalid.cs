using System.Linq;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingEntitlementMonth
{
    public class IfTheEntitlementMonthIsInvalid : SpeedyIntegrationTestBase
    {
        protected IfTheEntitlementMonthIsInvalid(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(storyData => Rs7Model = storyData.Rs7Model);
        }

        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private void Act(UpdateRs7EntitlementMonth updateRs7EntitlementMonthCommand)
        {
            When.Put($"{Url}/{Rs7Model.Id}/entitlement-months/{updateRs7EntitlementMonthCommand.MonthNumber}",
                updateRs7EntitlementMonthCommand);
        }

        [Fact]
        public void IfTheDayNumberIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().DayNumber = 100; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1040");
        }

        [Fact]
        public void IfTheMonthIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand =
                ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em => { em.MonthNumber = 6; });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Certificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Certificated = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Hours20 = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Hours20 = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().NonCertificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().NonCertificated = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().TwoAndOver = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().TwoAndOver = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Under2 = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().Under2 = -1; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }

        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand =
                ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em => { em.Year = 2021; });

            Act(updateRs7EntitlementMonthCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1030");
        }

        [Fact]
        public void ThenADomainEventShouldNotBePublished()
        {
            // Arrange
            var updateRs7EntitlementMonthCommand = ModelBuilder.UpdateRs7EntitlementMonth(Rs7Model, 2, em =>
            {
                if (em.Days != null)
                    em.Days.First().NonCertificated = 10000; // Not valid
            });

            Act(updateRs7EntitlementMonthCommand);

            A_domain_event_should_not_be_fired<Rs7EntitlementMonthUpdated>();
        }
    }
}