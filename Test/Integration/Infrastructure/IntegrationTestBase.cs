using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bard;
using Bard.Configuration;
using Bard.Infrastructure;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Moe.ECE.Events.Integration;
using Moe.Library.Cqrs;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    [Collection(Collections.IntegrationTestCollection)]
    public abstract class IntegrationTestBase<TStoryBook, TStoryData> : IClassFixture<TestState<TStoryBook, TStoryData>>
        where TStoryBook : StoryBook<TStoryData>, new()
        where TStoryData : class, new()
    {
        private readonly ITestOutputHelper _output;

        protected IntegrationTestBase(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState<TStoryBook, TStoryData> testState)
        {
            _output = output;
            TestState = testState;
            HttpClient = testSetUp.HttpClient;
            Output = new LogWriter(output.WriteLine);
            Services = testSetUp.Services.CreateScope().ServiceProvider;

            // ReSharper disable once VirtualMemberCallInConstructor
            Initialize();
        }

        protected IThen Then => TestState.Scenario.Then;

        protected IWhen When => TestState.Scenario.When;

        protected TStoryBook Given => TestState.Scenario.Given;

        protected TStoryBook And => Given;
        protected TestState<TStoryBook, TStoryData> TestState { get; }

        private IServiceProvider Services { get; }

        private LogWriter Output { get; }

        private HttpClient HttpClient { get; }

        protected dynamic TestData => TestState.Data;

        protected virtual void Initialize()
        {
            RunTestInitialization();
        }

        protected void RunTestInitialization()
        {
            var scenario = ScenarioConfiguration.WithStoryBook<TStoryBook, TStoryData>()
                .Configure(options =>
                {
                    options.Client = HttpClient;
                    options.Services = Services;
                    options.LogMessage = _output.WriteLine;
                    //options.MaxApiResponseTime = 4000; //OPA ops funding call is taking 20 seconds
                    options.BadRequestProvider = new ResponseValidator();
                    options.JsonSerializeOptions = new JsonSerializerOptions
                    {
                        IgnoreNullValues = true,
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    };
                    options.JsonDeserializeOptions = new JsonSerializerOptions
                    {
                        Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)},
                        PropertyNameCaseInsensitive = true
                    };
                });

            TestState.Initialize(scenario, Services, Output);
            var databaseManager = new DatabaseManager(Services.GetService<IDocumentStore>());
            databaseManager.ResetDatabase();
            ArrangeAndAct();
        }

        private void ArrangeAndAct()
        {
            Arrange();

            Act();
        }

        /// <summary>
        ///     Responsible for setting up configured services used in the test scenarios.
        /// </summary>
        protected virtual void Arrange()
        {
        }

        /// <summary>
        ///     Responsible for performing the actual test scenario.
        /// </summary>
        protected virtual void Act()
        {
        }

        protected TDomainEvent A_domain_event_should_be_fired<TDomainEvent>()
            where TDomainEvent : class, IDomainEvent
        {
            return TestState.A_domain_event_should_be_fired<TDomainEvent>();
        }

        protected void A_domain_event_should_not_be_fired<TDomainEvent>()
            where TDomainEvent : class, IDomainEvent
        {
            TestState.A_domain_event_should_not_be_fired<TDomainEvent>();
        }

        protected TIntegrationEvent An_integration_event_should_be_fired<TIntegrationEvent>()
            where TIntegrationEvent : class, IIntegrationEvent
        {
            return TestState.An_integration_event_should_be_fired<TIntegrationEvent>();
        }
    }
}