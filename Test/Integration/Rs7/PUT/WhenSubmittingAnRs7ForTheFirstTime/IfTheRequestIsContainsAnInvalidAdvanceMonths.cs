﻿using System.Linq;
using Bard;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;
using MoE.ECE.Integration.Tests.Chapter;
using MoE.ECE.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace MoE.ECE.Integration.Tests.Rs7.PUT.WhenSubmittingAnRs7ForTheFirstTime
{
    public class IfTheRequestIsContainsAnInvalidAdvanceMonths : SpeedyIntegrationTestBase
    {
        private const string Url = "api/rs7";

        public IfTheRequestIsContainsAnInvalidAdvanceMonths(RunOnceBeforeAllTests testSetUp, ITestOutputHelper output,
            TestState<ECEStoryBook, ECEStoryData> testState) : base(testSetUp, output, testState)
        {
        }

        private Rs7Model Rs7
        {
            get => TestData.Rs7Model;
            set => TestData.Rs7Model = value;
        }

        protected override void Arrange() =>
            Given
                .A_rs7_has_been_created()
                .GetResult(created => Rs7 = created.Rs7Model);

        [Theory]
        [InlineData(0)]
        [InlineData(10000)]
        public void IfTheYearIsOutsideTheAllowedValueThenTheResponseShouldBeAHttp400(int year)
        {
            // Arrange
            UpdateRs7? updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.AdvanceMonths != null)
                {
                    rs7.AdvanceMonths.First().Year = year;
                }
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public void IfTheMonthIsOutsideTheAllowedValueThenTheResponseShouldBeAHttp400(int month)
        {
            // Arrange
            UpdateRs7? updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.AdvanceMonths != null)
                {
                    rs7.AdvanceMonths.First().MonthNumber = month;
                }
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode(ErrorCode.InclusiveBetweenValidator);
        }

        [Fact]
        public void IfTheMonthIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            UpdateRs7? updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.AdvanceMonths != null)
                {
                    rs7.AdvanceMonths.First().MonthNumber = 12;
                }
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }

        [Fact]
        public void IfTheYearIsInvalidThenTheResponseShouldBeAHttp400()
        {
            // Arrange
            UpdateRs7? updateRs7 = ModelBuilder.UpdateRs7(Rs7, rs7 =>
            {
                if (rs7.AdvanceMonths != null)
                {
                    rs7.AdvanceMonths.First().Year = 2021;
                }
            });

            // Act
            When.Put($"{Url}/{Rs7.Id}", updateRs7);

            Then.Response
                .ShouldBe
                .BadRequest
                .WithErrorCode("1020");
        }
    }
}