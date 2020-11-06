using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
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
        public void IsNotRequired()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 => rs7.Declaration = null);

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void FullNameIsNotRequired()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = null
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void FullNameCanBeAnEmptyString()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = string.Empty
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void FullNameCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = "T"
                };
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
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = GenerateString(151)
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void ContactPhoneIsNotRequired()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = null
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void ContactPhoneCanBeAnEmptyString()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = string.Empty
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void ContactPhoneCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = "T"
                };
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
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = GenerateString(51)
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void RoleIsNotRequired()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = null
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void RoleCanBeAnEmptyString()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = string.Empty
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void RoleCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = "T"
                };
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
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = GenerateString(101)
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void IsDeclaredTrueIsNotRequired()
        {
            // Arrange
            var command = Command.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    IsDeclaredTrue = null
                };
            });

            // Act
            Api.Put($"{Url}/{Rs7Created.Id}", command);

            Then.TheResponse
                .ShouldBe
                .NoContent();
        }

        private static string GenerateString(int length)
        {
            return new string('x', length);
        }
    }
}