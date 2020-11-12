using System.Linq;
using Bard;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class IfTheRequestIsContainsAnInvalidEntitlementMonthDay : SpeedyIntegrationTestBase
    {
        public IfTheRequestIsContainsAnInvalidEntitlementMonthDay(RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output, TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output,
            testState)
        {
        }

        private const string Url = "api/rs7";

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Model);
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        [Fact]
        public void IfTheDayNumberIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().DayNumber = 100; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1040");
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Certificated = 10000; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Certificated = -1; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Certificated);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Hours20 = 10000; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedHours20ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Hours20 = -1; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Hours20);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().NonCertificated = 10000; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void IfTheSuppliedNonCertificatedValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().NonCertificated = -1; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.NonCertificated);
        }

        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().TwoAndOver = 10000; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedTwoAndOverValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().TwoAndOver = -1; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.TwoAndOver);
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsGreaterThan9999ThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Under2 = 10000; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }

        [Fact]
        public void IfTheSuppliedUnder2ValueIsLessThanZeroThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            var updateCommand = ModelBuilder.SaveAsDraft(Rs7, r =>
            {
                var rs7EntitlementDayModels = r.EntitlementMonths?.First().Days;
                if (rs7EntitlementDayModels != null)
                    rs7EntitlementDayModels.First().Under2 = -1; // Not valid
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateCommand);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<Rs7EntitlementDayModel>(model => model.Under2);
        }
    }
}