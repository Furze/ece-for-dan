using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenUpdatingDeclaration
{
    public class If_the_rs7_has_an_invalid_roll_status:SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";
        
        public If_the_rs7_has_an_invalid_roll_status(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output, TestState testState) : base(testSetUp, output, testState)
        {
        }
        
        protected override void Arrange()
        {
            If
                .A_rs7_has_been_created()
                .UseResult(result => Rs7Model = result);
            
            this.UpdateRs7Declaration =  Command.UpdateRs7Declaration();
        }
        
        private Rs7Model Rs7Model
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        private UpdateRs7Declaration UpdateRs7Declaration
        {
            get => TestData.UpdateRs7Declaration;
            set => TestData.UpdateRs7Declaration = value;
        }

        [Fact]
        public void Invalid_roll_status()
        {
            Api.Put($"{Url}/{this.Rs7Model.Id}/declaration", UpdateRs7Declaration);
            
            Then.TheResponse.ShouldBe.BadRequest.WithErrorCode(ErrorCode.InvalidRollStatusForUpdate);
        }
    }
}