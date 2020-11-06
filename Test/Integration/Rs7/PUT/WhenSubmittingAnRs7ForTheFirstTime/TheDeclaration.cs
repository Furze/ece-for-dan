using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class TheDeclaration : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public TheDeclaration(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        private Rs7Created Rs7Created
        {
            get => TestData.Rs7Created;
            set => TestData.Rs7Created = value;
        }

        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(created => Rs7Created = created);
        }

        [Fact]
        public void IsRequired()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 => rs7.Declaration = null);

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<UpdateRs7>(rs7 => rs7.Declaration)
                .WithMessage("'Declaration' must not be empty.");
        }

        [Fact]
        public void FullNameIsRequired()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = null;
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameCannotBeAnEmptyString()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = string.Empty;
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = "T";
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameCannotBeMoreThan150Characters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                    rs7.Declaration.FullName = GenerateString(151);
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void ContactPhoneIsRequired()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.ContactPhone = null;
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneCannotBeAnEmptyString()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.ContactPhone = string.Empty;
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.ContactPhone = "T";
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneCannotBeMoreThan50Characters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.ContactPhone = GenerateString(51);
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void RoleIsRequired()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.Role = null;
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleCannotBeAnEmptyString()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.Role = string.Empty;
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.Role = "T";
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleCannotBeMoreThan100Characters()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.Role = GenerateString(101);
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void IsDeclaredTrueIsRequired()
        {
            // Arrange
            var command = Command.UpdateRs7(Rs7Created, rs7 =>
            {
                if (rs7.Declaration != null)
                {
                    rs7.Declaration.IsDeclaredTrue = null;
                }
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.IsDeclaredTrue);
        }

        private static string GenerateString(int length)
        {
            return new string('x', length);
        }
    }
}