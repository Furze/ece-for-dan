using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7Form
{
    public class ThenTheOrganisationMustNotBeClosedPast12Months : SpeedyIntegrationTestBase
    {
        public ThenTheOrganisationMustNotBeClosedPast12Months(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private readonly int _organisationId = ReferenceData.EceServices.TestClosedService.RefOrganisationId;
        private const string Url = "api/rs7";

        protected override void Act()
        {
            When.Post(Url, ModelBuilder.Rs7Model(rs7 => rs7.OrganisationId = _organisationId));
        }

        [Fact(Skip =
            "TODO(ERST-11367): Originally part of ERST-11035, but couldn't be implemented until Closed status records actually imported from First.")]
        public void ThenTheResponseShouldBeABadRequest()
        {
            Then
                .Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.EceServiceIneligibleBecauseStatusClosed);
        }
    }
}