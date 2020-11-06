using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.Event
{
    public abstract class WhenAnRs7Received : IntegrationTestBase
    {
        private InMemoryServiceBus _serviceBus = null!;
        private Exception _exception = null!;

        protected WhenAnRs7Received(
            RunOnceBeforeAllTests testSetUp,
            ITestOutputHelper output,
            TestState testState)
            : base(testSetUp, output, testState)
        {
        }

        protected override void Arrange()
        {
            _serviceBus = (InMemoryServiceBus) Services.GetService(typeof(InMemoryServiceBus));
        }

        protected override void Act()
        {
            try
            {
                AsyncHelper.RunSync(() => _serviceBus.PublishAsync(CreateRs7Received(), Constants.Topic.Eli, CancellationToken.None));
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        private void ShouldContainFailure(Expression<Func<ValidationFailure, bool>> func)
        {
            ((ValidationException) _exception)
                .Errors
                .ShouldContain(func);
        }

        private BadRequestException ShouldHaveBadRequestException()
        {
            var badRequest = (BadRequestException) _exception;
            badRequest.ShouldNotBeNull();

            return badRequest;
        }

        protected void ShouldContainErrorCode(string errorCode)
        {
            ShouldContainFailure(v => v.ErrorCode == errorCode);
        }

        protected void ShouldContainErrorProperty(string propertyName)
        {
            ShouldContainFailure(v => v.PropertyName == propertyName);
        }

        protected virtual Rs7Received CreateRs7Received()
        {
            var command = new Rs7Received
            {
                OrganisationNumber = ReferenceData.EceServices.MontessoriLittleHands.OrganisationNumber!,
                FundingPeriod = FundingPeriodMonth.July,
                IsAttested = true,
                Source = "Uranus",
                AdvanceMonths = new[]
                {
                    new Rs7ReceivedAdvanceMonth
                    {
                        MonthNumber = 7,
                        FundingPeriodYear = 2020,
                        AllDay = 2,
                        ParentLed = 4,
                        Sessional = 6
                    },
                    new Rs7ReceivedAdvanceMonth
                    {
                        MonthNumber = 8,
                        FundingPeriodYear = 2020,
                        AllDay = 1,
                        ParentLed = 2,
                        Sessional = 4
                    },
                    new Rs7ReceivedAdvanceMonth
                    {
                        MonthNumber = 9,
                        FundingPeriodYear = 2020,
                        AllDay = 3,
                        ParentLed = 5,
                        Sessional = 3
                    },
                    new Rs7ReceivedAdvanceMonth
                    {
                        MonthNumber = 10,
                        FundingPeriodYear = 2020,
                        AllDay = 1,
                        ParentLed = 2,
                        Sessional = 3
                    }
                },
                EntitlementMonths = new[]
                {
                    new Rs7ReceivedEntitlementMonth
                    {
                        MonthNumber = 2,
                        FundingPeriodYear = 2020,
                        Days = new[]
                        {
                            new Rs7ReceivedEntitlementDay
                            {
                                DayNumber = 1,
                                Certificated = 5,
                                NonCertificated = 6,
                                Hours20 = 2,
                                TwoAndOver = 3,
                                Plus10 = 4,
                                Under2 = 5
                            }
                        }
                    },
                    new Rs7ReceivedEntitlementMonth
                    {
                        MonthNumber = 3,
                        FundingPeriodYear = 2020,
                        Days = new[]
                        {
                            new Rs7ReceivedEntitlementDay
                            {
                                DayNumber = 1,
                                Certificated = 5,
                                NonCertificated = 6,
                                Hours20 = 2,
                                TwoAndOver = 3,
                                Plus10 = 4,
                                Under2 = 5
                            }
                        }
                    },
                    new Rs7ReceivedEntitlementMonth
                    {
                        MonthNumber = 4,
                        FundingPeriodYear = 2020,
                        Days = new[]
                        {
                            new Rs7ReceivedEntitlementDay
                            {
                                DayNumber = 1,
                                Certificated = 5,
                                NonCertificated = 6,
                                Hours20 = 2,
                                TwoAndOver = 3,
                                Plus10 = 4,
                                Under2 = 5
                            }
                        }
                    },
                    new Rs7ReceivedEntitlementMonth
                    {
                        MonthNumber = 5,
                        FundingPeriodYear = 2020,
                        Days = new[]
                        {
                            new Rs7ReceivedEntitlementDay
                            {
                                DayNumber = 1,
                                Certificated = 5,
                                NonCertificated = 6,
                                Hours20 = 2,
                                TwoAndOver = 3,
                                Plus10 = 4,
                                Under2 = 5
                            }
                        }
                    }
                }
            };

            return command;
        }

        public class GivenTheReceivedDataIsValid : WhenAnRs7Received
        {
            private RollContext _context = null!;
            private int _rs7Count;

            public GivenTheReceivedDataIsValid(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override void Arrange()
            {
                base.Arrange();

                _context = Services.GetService<RollContext>();

                _rs7Count = GetRs7Count();
            }

            private int GetRs7Count() => _context.Rs7S.Count();

            [Fact]
            public void ExpectDomainEventShouldHavePopulatedAdvanceMonthsFromTheCommand()
            {
                var domainEvent = GetDomainEvent<Rs7CreatedFromExternal>();
                var firstAdvanceMonth = domainEvent.AdvanceMonths.First();

                firstAdvanceMonth.MonthNumber.ShouldBe(7);
                firstAdvanceMonth.Year.ShouldBe(2020);
                firstAdvanceMonth.AllDay.ShouldBe(2);
                firstAdvanceMonth.ParentLed.ShouldBe(4);
                firstAdvanceMonth.Sessional.ShouldBe(6);
            }

            [Fact]
            public void ExpectDomainEventShouldHavePopulatedEntitlementMonthFromTheCommand()
            {
                var domainEvent = GetDomainEvent<Rs7CreatedFromExternal>();
                var entitlementMonth = domainEvent.EntitlementMonths.First();

                entitlementMonth.MonthNumber.ShouldBe(2);
                entitlementMonth.Year.ShouldBe(2020);

                var entitlementDay = entitlementMonth.Days.First();
                entitlementDay.DayNumber.ShouldBe(1);
                entitlementDay.Under2.ShouldBe(5);
                entitlementDay.TwoAndOver.ShouldBe(3);
                entitlementDay.Plus10.ShouldBe(4);
                entitlementDay.Hours20.ShouldBe(2);
                entitlementDay.Certificated.ShouldBe(5);
                entitlementDay.NonCertificated.ShouldBe(6);
            }

            [Fact]
            public void ExpectAnRs7CreatedFromExternalDomainEventRaised()
            {
                GetDomainEvent<Rs7CreatedFromExternal>()
                    .ShouldNotBeNull();
            }

            [Fact]
            public void ExpectAnRs7CreatedFromExternalDomainEventWithSource()
            {
                GetDomainEvent<Rs7CreatedFromExternal>()
                    .Source.ShouldBe("Uranus");
            }

            [Fact]
            public void ExpectAnRs7UpdatedIntegrationEventRaised()
            {
                Then.An_integration_event_should_be_fired<Domain.Integration.Events.Rs7.Rs7Updated>();
            }

            [Fact]
            public void ExpectAnRs7AddedToDatabase()
            {
                GetRs7Count().ShouldBe(_rs7Count + 1);
            }
        }

        public class GivenAPreExistingRs7WithNewStatus : WhenAnRs7Received
        {
            private RollContext _context = null!;
            private int _rs7Count;
            private Rs7Created _previouslyStartedNewRs7 = new Rs7Created();

            public GivenAPreExistingRs7WithNewStatus(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override void Arrange()
            {
                base.Arrange();

                If
                    .A_rs7_has_been_created(setup =>
                    {
                        setup.OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId;
                        setup.FundingPeriod = FundingPeriodMonth.July;
                        setup.FundingPeriodYear = 2020;
                    })
                    .UseResult(result => _previouslyStartedNewRs7 = result);

                _context = Services.GetService<RollContext>();

                _rs7Count = GetRs7Count();
            }

            private int GetRs7Count() => _context.Rs7S.Count();

            [Fact]
            public void ExpectDomainEventShouldBePublishedUsingThePreExistingId()
            {
                var domainEvent = GetDomainEvent<Rs7CreatedFromExternal>();

                domainEvent.ShouldNotBeNull();
                domainEvent.Id.ShouldBe(_previouslyStartedNewRs7.Id);
            }

            [Fact]
            public void ExpectDomainEventShouldHavePopulatedAdvanceMonthsFromTheCommand()
            {
                var domainEvent = GetDomainEvent<Rs7CreatedFromExternal>();
                var firstAdvanceMonth = domainEvent.AdvanceMonths.First();

                firstAdvanceMonth.MonthNumber.ShouldBe(7);
                firstAdvanceMonth.Year.ShouldBe(2020);
                firstAdvanceMonth.AllDay.ShouldBe(2);
                firstAdvanceMonth.ParentLed.ShouldBe(4);
                firstAdvanceMonth.Sessional.ShouldBe(6);
            }

            [Fact]
            public void ExpectDomainEventShouldHavePopulatedEntitlementMonthFromTheCommand()
            {
                var domainEvent = GetDomainEvent<Rs7CreatedFromExternal>();
                var entitlementMonth = domainEvent.EntitlementMonths.First();

                entitlementMonth.MonthNumber.ShouldBe(2);
                entitlementMonth.Year.ShouldBe(2020);

                var entitlementDay = entitlementMonth.Days.First();
                entitlementDay.DayNumber.ShouldBe(1);
                entitlementDay.Under2.ShouldBe(5);
                entitlementDay.TwoAndOver.ShouldBe(3);
                entitlementDay.Plus10.ShouldBe(4);
                entitlementDay.Hours20.ShouldBe(2);
                entitlementDay.Certificated.ShouldBe(5);
                entitlementDay.NonCertificated.ShouldBe(6);
            }

            [Fact]
            public void ExpectAnRs7CreatedFromExternalDomainEventWithSource()
            {
                GetDomainEvent<Rs7CreatedFromExternal>()
                    .Source.ShouldBe("Uranus");
            }

            [Fact]
            public void ExpectAnRs7UpdatedIntegrationEventRaised()
            {
                Then.An_integration_event_should_be_fired<Domain.Integration.Events.Rs7.Rs7Updated>();
            }

            [Fact]
            public void ExpectExistingRs7UpdatedInDatabase()
            {
                GetRs7Count().ShouldBe(_rs7Count);
            }
        }

        public class GivenTheReceivedDataIsForUnknownOrganisation : WhenAnRs7Received
        {
            public GivenTheReceivedDataIsForUnknownOrganisation(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.OrganisationNumber = "12345";

                return @event;
            }

            [Fact]
            public void ExpectAValidationException()
            {
                _exception.ShouldBeOfType<ValidationException>();
            }

            [Fact]
            public void ExpectCorrectErrorCode()
            {
                ShouldContainErrorCode("NotEmptyValidator");
            }

            [Fact]
            public void ExpectCorrectErrorMessage()
            {
                ShouldContainErrorProperty(nameof(CreateRs7FromExternal.OrganisationId));
            }
        }

        public class GivenTheOrganisationNumberNull : WhenAnRs7Received
        {
            public GivenTheOrganisationNumberNull(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.OrganisationNumber = null!;

                return @event;
            }

            [Fact]
            public void ExpectValidationException()
            {
                _exception.ShouldBeOfType<ValidationException>();
            }

            [Fact]
            public void ExpectCorrectValidationErrorCode()
            {
                ShouldContainErrorCode("NotEmptyValidator");
            }

            [Fact]
            public void ExpectCorrectValidationProperty()
            {
                ShouldContainErrorProperty(nameof(CreateRs7FromExternal.OrganisationId));
            }
        }

        public class GivenOnlyInvalidFundingPeriod : WhenAnRs7Received
        {
            public GivenOnlyInvalidFundingPeriod(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.FundingPeriod = (FundingPeriodMonth) 100;

                return @event;
            }

            [Fact]
            public void ExpectValidationException()
            {
                _exception.ShouldBeOfType<ValidationException>();
            }

            [Fact]
            public void ExpectCorrectValidationErrorCode()
            {
                ShouldContainErrorCode("InvalidFundingPeriod");
            }

            [Fact]
            public void ExpectCorrectValidationProperty()
            {
                ShouldContainErrorProperty(nameof(CreateRs7FromExternal.FundingPeriod));
            }
        }

        public class GivenTheOrganisationLicenseIsCancelled : WhenAnRs7Received
        {
            public GivenTheOrganisationLicenseIsCancelled(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.OrganisationNumber = ReferenceData.EceServices.FamilyTiesEducare.OrganisationNumber!;

                return @event;
            }

            [Fact]
            public void ExpectBadRequestException()
            {
                ShouldHaveBadRequestException()
                    .ErrorCode
                    .ShouldBe(ErrorCode.EceServiceIneligibleBecauseLicenceStatus);
            }
        }

        public class GivenTheOrganisationLicenseIsSuspended : WhenAnRs7Received
        {
            public GivenTheOrganisationLicenseIsSuspended(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.OrganisationNumber = ReferenceData.EceServices.NurtureMe2.OrganisationNumber!;

                return @event;
            }

            public void ExpectBadRequestException()
            {
                ShouldHaveBadRequestException()
                    .ErrorCode
                    .ShouldBe(ErrorCode.EceServiceIneligibleBecauseLicenceStatus);
            }
        }

        public class GivenNoEntitlementMonths : WhenAnRs7Received
        {
            public GivenNoEntitlementMonths(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.EntitlementMonths = Enumerable.Empty<Rs7ReceivedEntitlementMonth>();

                return @event;
            }

            [Fact]
            public void ExpectNoValidationException()
            {
                _exception.ShouldBeNull();
            }
        }

        public class GivenNoEntitlementMonthsOrAdvanceMonths : WhenAnRs7Received
        {
            public GivenNoEntitlementMonthsOrAdvanceMonths(
                RunOnceBeforeAllTests testSetUp,
                ITestOutputHelper output,
                TestState testState)
                : base(testSetUp, output, testState)
            {
            }

            protected override Rs7Received CreateRs7Received()
            {
                var @event = base.CreateRs7Received();

                @event.EntitlementMonths = Enumerable.Empty<Rs7ReceivedEntitlementMonth>();
                @event.AdvanceMonths = Enumerable.Empty<Rs7ReceivedAdvanceMonth>();

                return @event;
            }

            [Fact]
            public void ExpectValidationException()
            {
                _exception.ShouldBeOfType<ValidationException>();
            }

            [Fact]
            public void ExpectCorrectValidationErrorCode()
            {
                ShouldContainErrorCode("MissingEntitlementMonths");
            }

            [Fact]
            public void ExpectCorrectValidationProperty()
            {
                ShouldContainErrorProperty(nameof(CreateRs7FromExternal.EntitlementMonths));
            }
        }
    }
}