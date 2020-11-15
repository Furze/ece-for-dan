using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class IfTheOrganisationLicenceIsCancelled : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        private readonly int _organisationId = ReferenceData.EceServices.FamilyTiesEducare.RefOrganisationId;

        public IfTheOrganisationLicenceIsCancelled(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        protected override void Act() =>
            When.Post(Url, ModelBuilder.Rs7Model(rs7 => rs7.OrganisationId = _organisationId));

        [Fact]
        public void ThenTheResponseShouldBeABadRequest() =>
            Then
                .Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.EceServiceIneligibleBecauseLicenceStatus);
    }
}