using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Event;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.POST.WhenCreatingAnRs7ZeroReturn
{
    public class IfTheOrganisationLicenceIsSuspended : SpeedyIntegrationTestBase
    {
        public IfTheOrganisationLicenceIsSuspended(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private const string Url = "api/rs7";
        private readonly int _organisationId = ReferenceData.EceServices.NurtureMe2.RefOrganisationId;

        protected override void Act()
        {
            When.Post(Url,
                ModelBuilder.Rs7Model(rs7 =>
                {
                    rs7.OrganisationId = _organisationId;
                    rs7.IsZeroReturn = true;
                }));
        }

        private Rs7ZeroReturnCreated DomainEvent => A_domain_event_should_be_fired<Rs7ZeroReturnCreated>();

        [Fact]
        public void ThenADomainEventShouldBePublished()
        {
            DomainEvent.ShouldNotBeNull();

            DomainEvent.Id.ShouldNotBe(0);
        }

        [Fact]
        public void Then_the_response_should_be_a_201_created()
        {
            Then.Response.ShouldBe.Created();
        }
        
        [Fact]
        public void Then_the_response_should_contain_a_location_header()
        {
            Then.Response.Headers.Should.Include.Location();
        }
    }
}