using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoE.ECE.Integration.OpenPolicyAgent;
using MoE.ECE.Integration.OpenPolicyAgent.Model;
using MoE.ECE.Integration.OpenPolicyAgent.Services;
using MoE.ECE.Web.Infrastructure.Authorisation;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace MoE.ECE.Unit.Tests.Web.Infrastructure.Authorisation
{
    public class RequiredPermissionAuthorisationHandlerTests
    {
        private TestableRequiredPermissionAuthorisationHandler SystemUnderTest { get; }
        private readonly AuthorizationHandlerContext _authContext;
        private readonly WireMockServer _mockServer;
        private readonly IHttpClientFactory _clientFactory;

        public RequiredPermissionAuthorisationHandlerTests()
        {
            _mockServer = WireMockServer.Start();
            var configuration = BuildConfiguration(_mockServer);

            _authContext = CreateAuthorizationHandlerContext();

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            if(httpContextAccessor.HttpContext != null)
                httpContextAccessor.HttpContext.Request.Path = "/client-path";

            _clientFactory = Substitute.For<IHttpClientFactory>();
            _clientFactory.CreateClient("open-policy-agent").Returns(new HttpClient());

            SystemUnderTest = new TestableRequiredPermissionAuthorisationHandler(
                new OpenPolicyAgentService(configuration, _clientFactory, Substitute.For<ILogger<OpenPolicyAgentService>>()),
                httpContextAccessor,
                Substitute.For<ILogger<RequiredPermissionAuthorisationHandler>>());
        }

        [Fact]
        public void AuthorisationShouldFailWhenOpenPolicyAgentReturnsNotAllowed()
        {
            WhenOpenPolicyAgentReturnsAuthzAllow(false);

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsNotAllowed();
        }

        [Fact]
        public void AuthorisationShouldSucceedWhenOpenPolicyAgentReturnsAllowed()
        {
            WhenOpenPolicyAgentReturnsAuthzAllow(true);

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsAllow();
        }

        private void AuthzAllowDecisionIsAllow()
        {
            try
            {
                _authContext.Received(Quantity.None())
                    .Fail();

                _authContext.Received(Quantity.AtLeastOne())
                    .Succeed(Arg.Any<PermissionRequirement>());
            }
            catch (NSubstitute.Exceptions.ReceivedCallsException e)
            {
                throw new Xunit.Sdk.XunitException($"Expected Authorisation to succeed but it failed. \n{e.Message}");
            }
        }

        private void AuthzAllowDecisionIsNotAllowed(string message = "")
        {
            try
            {
                _authContext.Received()
                    .Fail();

                _authContext.Received(Quantity.None())
                    .Succeed(Arg.Any<PermissionRequirement>());
            }
            catch (NSubstitute.Exceptions.ReceivedCallsException)
            {
                throw new Xunit.Sdk.XunitException($"Expected Authorisation to fail but it succeeded! \n{message}");
            }
        }


        [Theory]
        [InlineData(400, "Bad request")]
        [InlineData(401, "Unauthenticated")]
        [InlineData(403, "Unauthorised")]
        [InlineData(404, "Not found")]
        [InlineData(405, "Method not allowed")]
        [InlineData(406, "Not Acceptable")]
        [InlineData(407, "Proxy Auth")]
        [InlineData(408, "Time out")]
        [InlineData(500, "Server error")]
        [InlineData(501, "Not implemented")]
        [InlineData(502, "Bad Gateway")]
        [InlineData(503, "Service Unavailable")]
        [InlineData(504, "Gateway Timeout")]
        public void AuthorisationShouldFailWhenOpenPolicyAgentReturnsCode(int code, string message)
        {
            WhenOpenPolicyAgentReturnsHttpStatusCode(code);

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsNotAllowed(message);
        }

        [Fact]
        public void AuthorisationShouldFailWhenWhenOpenPolicyAgentReturnsRubbishResponse()
        {
            WhenOpenPolicyAgentReturnsFaultyResponse(FaultType.MALFORMED_RESPONSE_CHUNK);

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsNotAllowed();
        }

        [Fact]
        public void AuthorisationShouldFailWhenWhenOpenPolicyAgentReturnsEmptyResponse()
        {
            WhenOpenPolicyAgentReturnsFaultyResponse(FaultType.EMPTY_RESPONSE);

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsNotAllowed();
        }

        [Fact]
        public void AuthorisationShouldFailWhenExceptionIsThrownTryingToTalkToOpenPolicyAgent()
        {
            WhenExceptionIsThrownTryingToTalkToOpenPolicyAgent();

            SystemUnderTest.HandleRequirementAsync(_authContext, Requirement())
                .GetAwaiter().GetResult();

            AuthzAllowDecisionIsNotAllowed();
        }

        private void WhenExceptionIsThrownTryingToTalkToOpenPolicyAgent()
        {
            _clientFactory.CreateClient("open-policy-agent")
                .Throws(new Exception("SURPRISE! YOU'RE DEAD!!!"));
        }

        private void WhenOpenPolicyAgentReturnsFaultyResponse(FaultType faultType)
        {
            _mockServer.Reset();
            _mockServer
                .Given(PostingToAuthZ())
                .RespondWith(BaseResonse()
                        .WithFault(faultType, 100));
        }

        private static IResponseBuilder BaseResonse(int code = 200)
        {
            return Response.Create()
                .WithStatusCode(code)
                .WithHeader("Content-Type", "application/json");
        }

        private static IRequestBuilder PostingToAuthZ()
        {
            return Request.Create()
                .WithPath("/authz")
                .UsingPost();
        }

        private void WhenOpenPolicyAgentReturnsHttpStatusCode(int code) => WhenOpenPolicyAgentReturns(true, code);

        private void WhenOpenPolicyAgentReturnsAuthzAllow(bool allow) => WhenOpenPolicyAgentReturns(allow);

        private void WhenOpenPolicyAgentReturns(bool allow, int code = 200)
        {
            _mockServer.Reset();
            _mockServer
                .Given(PostingToAuthZ())
                .RespondWith(BaseResonse(code)
                    .WithBodyAsJson(new OpenPolicyAgentResult<OpenPolicyAgentAuthzResponse>() { Result = new OpenPolicyAgentAuthzResponse { Allow = allow } }));
        }

        private PermissionRequirement Requirement() => new PermissionRequirement("can-run-tests", false);

        private IConfigurationRoot BuildConfiguration(WireMockServer server)
            => new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"OpenPolicyAgent:BaseUri", server.Urls[0]},
                    {"OpenPolicyAgent:AuthZPath", "/authz"}
                })
                .Build();

        private AuthorizationHandlerContext CreateAuthorizationHandlerContext()
            => Substitute.For<AuthorizationHandlerContext>(
                new[] { new PermissionRequirement("can-run-tests", false) },
                new ClaimsPrincipal(new GenericIdentity("PapaSmurf")),
                this);

        private class TestableRequiredPermissionAuthorisationHandler : RequiredPermissionAuthorisationHandler
        {
            public TestableRequiredPermissionAuthorisationHandler(
                IOpenPolicyAgentService openPolicyAgentService, IHttpContextAccessor httpContextAccessor,
                ILogger<RequiredPermissionAuthorisationHandler> logger)
                : base(openPolicyAgentService, httpContextAccessor, logger) { }

            public new async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
                => await base.HandleRequirementAsync(context, requirement);
        }
    }
}