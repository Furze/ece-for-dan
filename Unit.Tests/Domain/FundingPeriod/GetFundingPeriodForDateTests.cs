using MoE.ECE.Domain.Model.ValueObject;
using Shouldly;
using Xunit;

namespace MoE.ECE.Unit.Tests.Domain.FundingPeriod
{
    /// <summary>
    /// Unit tests to test this defect https://quality.minedu.govt.nz/browse/ERST-13196
    /// </summary>
    public class GetFundingPeriodForDateTests
    {
        [Fact]
        public void Current_funding_period_start_date_should_be_correct()
        {
            // Arrange
            var date = new Date(8, 12, 2020);
            
            // Act
            var fundingPeriod = MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingPeriodForDate(date);
            
            // Assert
            fundingPeriod.StartDate.Day.ShouldBe(1);
            fundingPeriod.StartDate.Month.ShouldBe(11);
            fundingPeriod.StartDate.Year.ShouldBe(2020);
        }
        
        [Fact]
        public void Previous_funding_period_start_date_should_be_correct()
        {
            // Arrange
            var date = new Date(8, 12, 2020);
            
            // Act
            var fundingPeriod = MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingPeriodForDate(date);

            fundingPeriod.PreviousFundingPeriod.StartDate.Day.ShouldBe(1);
            fundingPeriod.PreviousFundingPeriod.StartDate.Month.ShouldBe(7);
            fundingPeriod.PreviousFundingPeriod.StartDate.Year.ShouldBe(2020);
        }
        
        [Fact]
        public void Earlier_funding_period_start_date_should_be_correct()
        {
            // Arrange
            var date = new Date(8, 12, 2020);
            
            // Act
            var fundingPeriod = MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingPeriodForDate(date);

            fundingPeriod.EarlierFundingPeriod(2).StartDate.Day.ShouldBe(1);
            fundingPeriod.EarlierFundingPeriod(2).StartDate.Month.ShouldBe(3);
            fundingPeriod.EarlierFundingPeriod(2).StartDate.Year.ShouldBe(2020);
        }
        
        [Fact]
        public void Earlier_2_funding_period_start_date_should_be_correct()
        {
            // Arrange
            var date = new Date(8, 12, 2020);
            
            // Act
            var fundingPeriod = MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingPeriodForDate(date);

            fundingPeriod.EarlierFundingPeriod(3).StartDate.Day.ShouldBe(1);
            fundingPeriod.EarlierFundingPeriod(3).StartDate.Month.ShouldBe(11);
            fundingPeriod.EarlierFundingPeriod(3).StartDate.Year.ShouldBe(2019);
        }
        
        [Fact]
        public void Next_funding_period_start_date_should_be_correct()
        {
            // Arrange
            var date = new Date(8, 12, 2020);
            
            // Act
            var fundingPeriod = MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingPeriodForDate(date);

            fundingPeriod.NextFundingPeriod.StartDate.Day.ShouldBe(1);
            fundingPeriod.NextFundingPeriod.StartDate.Month.ShouldBe(3);
            fundingPeriod.NextFundingPeriod.StartDate.Year.ShouldBe(2021);
        }
    }
}