using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class TheDeclaration : SpeedyIntegrationTestBase
    {
        protected TheDeclaration(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "When/rs7";

        private Rs7Created Rs7Created
        {
            get => TestData.Rs7Created;
            set => TestData.Rs7Created = value;
        }

        protected override void Arrange()
        {
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7Created = created.Rs7Created);
        }

        private static string GenerateString(int length)
        {
            return new string('x', length);
        }

        [Fact]
        public void ContactPhoneCannotBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.ContactPhone = string.Empty;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.ContactPhone = "T";
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneCannotBeMoreThan50Characters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.ContactPhone = GenerateString(51);
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneIsRequired()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.ContactPhone = null;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void FullNameCannotBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = string.Empty;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = "T";
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameCannotBeMoreThan150Characters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = GenerateString(151);
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameIsRequired()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = null;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void IsDeclaredTrueIsRequired()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.IsDeclaredTrue = null;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.IsDeclaredTrue);
        }

        [Fact]
        public void IsRequired()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 => rs7.Declaration = null);

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.Declaration)
                .WithMessage("'Declaration' must not be empty.");
        }

        [Fact]
        public void RoleCannotBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.Role = string.Empty;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.Role = "T";
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleCannotBeMoreThan100Characters()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.Role = GenerateString(101);
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleIsRequired()
        {
            // Arrange
            var command = ModelBuilder.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null) rs7.Declaration.Role = null;
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }
    }
}