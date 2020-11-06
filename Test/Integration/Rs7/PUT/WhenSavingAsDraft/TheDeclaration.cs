using Bard;
using MoE.ECE.Domain.Event;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSavingAsDraft
{
    public class TheDeclaration : SpeedyIntegrationTestBase
    {
        public TheDeclaration(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
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
        public void ContactPhoneCanBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = string.Empty
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void ContactPhoneCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = "T"
                };
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
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = GenerateString(51)
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.ContactPhone);
        }

        [Fact]
        public void ContactPhoneIsNotRequired()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    ContactPhone = null
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void FullNameCanBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = string.Empty
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void FullNameCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = "T"
                };
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
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = GenerateString(151)
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.FullName);
        }

        [Fact]
        public void FullNameIsNotRequired()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    FullName = null
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void IsDeclaredTrueIsNotRequired()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    IsDeclaredTrue = null
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void IsNotRequired()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 => rs7.Declaration = null);

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void RoleCanBeAnEmptyString()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = string.Empty
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }

        [Fact]
        public void RoleCannotBeLessThanTwoCharacters()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = "T"
                };
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
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = GenerateString(101)
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .BadRequest
                .ForProperty<DeclarationModel>(model => model.Role);
        }

        [Fact]
        public void RoleIsNotRequired()
        {
            // Arrange
            var command = ModelBuilder.SaveAsDraft(Rs7Created, rs7 =>
            {
                rs7.Declaration = new DeclarationModel
                {
                    Role = null
                };
            });

            // Act
            When.Put($"{Url}/{Rs7Created.Id}", command);

            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}