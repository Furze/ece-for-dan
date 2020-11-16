using System.Linq;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Services;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Services.GET
{
    public class WhenSearchingForAService : IntegrationTestBase<ECEStoryBook, ECEStoryData>
    {
        public WhenSearchingForAService(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        [Fact]
        public void IfAFullECANumberIsProvidedFourServicesShouldBeReturned()
        {
            When.Get("api/services?search-term=ECA445");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(4);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[0].ServiceName
                .ShouldBe("Te Kohanga Reo o Mana Tamariki");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[1].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tahuri Mai");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[2].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tararua ki Paraparaumu");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[3].ServiceName
                .ShouldBe("Te Kōhanga Reo o Waikare");
        }

        [Fact]
        public void IfAFullOrganisationNumberIsProvidedOneServiceShouldBeReturned()
        {
            When.Get("api/services?search-term=10227");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(1);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.First().ServiceName
                .ShouldBe("Simply Kids Preschool");
        }

        [Fact]
        public void IfAPartialECANumberIsProvidedFourServicesShouldBeReturned()
        {
            When.Get("api/services?search-term=ECA4");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(4);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[0].ServiceName
                .ShouldBe("Te Kohanga Reo o Mana Tamariki");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[1].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tahuri Mai");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[2].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tararua ki Paraparaumu");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[3].ServiceName
                .ShouldBe("Te Kōhanga Reo o Waikare");
        }

        [Fact]
        public void IfAPartialOrganisationNumberIsProvidedOneServiceShouldBeReturned()
        {
            When.Get("api/services?search-term=1022");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(1);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.First().ServiceName
                .ShouldBe("Simply Kids Preschool");
        }

        [Fact]
        public void IfAPartialServiceNameGreaterThan30charIsProvidedOneServiceShouldBeReturned()
        {
            When.Get("api/services?search-term=Aurora%20Tamariki%20early%20years%20Car");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(1);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.First().ServiceName
                .ShouldBe("Aurora Tamariki Early Years Care and Kindergarten");
        }

        [Fact]
        public void IfAPartialServiceNameIsProvidedOneServiceShouldBeReturned()
        {
            When.Get("api/services?search-term=simply");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(1);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.First().ServiceName
                .ShouldBe("Simply Kids Preschool");
        }

        [Fact]
        public void IfAPartialServiceNameWithIsProvidedServicesAgnosticOfMacronsShouldBeReturned()
        {
            When.Get("api/services?search-term=te%20kohanga%20reo%20o");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(4);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[0].ServiceName
                .ShouldBe("Te Kohanga Reo o Mana Tamariki");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[1].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tahuri Mai");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[2].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tararua ki Paraparaumu");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[3].ServiceName
                .ShouldBe("Te Kōhanga Reo o Waikare");
        }

        [Fact]
        public void IfAPartialServiceNameWithMacronHttpEncodedIsProvidedServicesAgnosticOfMacronsShouldBeReturned()
        {
            When.Get("api/services?search-term=te%20k%C5%8Dhanga%20reo%20o");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[0].ServiceName
                .ShouldBe("Te Kohanga Reo o Mana Tamariki");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[1].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tahuri Mai");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[2].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tararua ki Paraparaumu");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[3].ServiceName
                .ShouldBe("Te Kōhanga Reo o Waikare");
        }

        [Fact]
        public void IfAPartialServiceNameWithMacronIsProvidedServicesAgnosticOfMacronsShouldBeReturned()
        {
            When.Get("api/services?search-term=te%20kōhanga%20reo%20o");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[0].ServiceName
                .ShouldBe("Te Kohanga Reo o Mana Tamariki");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[1].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tahuri Mai");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[2].ServiceName
                .ShouldBe("Te Kōhanga Reo o Tararua ki Paraparaumu");
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data[3].ServiceName
                .ShouldBe("Te Kōhanga Reo o Waikare");
        }

        [Fact]
        public void IfAPartialServiceNameWithMixedCaseIsProvidedOneServiceShouldBeReturned()
        {
            When.Get("api/services?search-term=SiMpLy");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBe(1);
            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.First().ServiceName
                .ShouldBe("Simply Kids Preschool");
        }

        [Fact]
        public void IfNoServiceNameIsProvidedAllServicesShouldBeReturned()
        {
            When.Get("api/services");

            Then.Response.Content<CollectionModel<SearchEceServiceModel>>().Data.Length.ShouldBeGreaterThan(0);
        }
    }
}