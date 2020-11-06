using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsContainsAnInvalidEntitlementMonthDay : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidEntitlementMonthDay(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(created => Rs7 = created);
        }

        [Fact]
        public void IfTheDayNumberIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().DayNumber = 100; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .WithErrorCode("1040");
        }
        
        [Fact]
        public void IfTheSuppliedUnder2ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Under2 = -1; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }
        
        [Fact]
        public void IfTheSuppliedUnder2ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Under2 = 10000; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }
        
        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().TwoAndOver = -1; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }
        
        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().TwoAndOver = 10000; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }
        
        [Fact]
        public void IfTheSuppliedHours20ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Hours20 = -1; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }
        
        [Fact]
        public void IfTheSuppliedHours20ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Hours20 = 10000; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Certificated = -1; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }
        
        [Fact]
        public void IfTheSuppliedCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().Certificated = 10000; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }
        
        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().NonCertificated = -1; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }
        
        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = Command.UpdateRs7(Rs7, r =>
            {
                r.EntitlementMonths.First().Days.First().NonCertificated = 10000; // Not valid
            });

            // Act
            Api.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }
        
        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }
    }
}