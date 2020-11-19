using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.CLI.Data
{
    public class OperationalFundingReferenceData
    {
        public OperationalFundingRequest[] Data
        {
            get
            {
                return new[]
                {
                    new OperationalFundingRequest
                    {
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        BusinessEntityId = new Guid("4ac2acf5-0130-4a60-b8ea-3fe3e7cd5073"),
                        FundingPeriodMonth = FundingPeriodMonth.March,
                        FundingYear = 2019,
                        AdvanceMonths = new List<AdvanceMonthFundingComponent>
                        {
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "March",
                                MonthNumber = 3,
                                Year = 2019,
                                AllDayWorkingDays = 22,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 1867.20m,
                                AmountPayableUnderTwo = 8997.30m,
                                AmountPayableTwoAndOver = 4492.95m,
                                AmountPayableTwentyHours = 8639.40m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 1867.20m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 8997.30m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4492.95m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8639.40m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "April",
                                MonthNumber = 4,
                                Year = 2019,
                                AllDayWorkingDays = 23,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 1867.20m,
                                AmountPayableUnderTwo = 8997.30m,
                                AmountPayableTwoAndOver = 4492.95m,
                                AmountPayableTwentyHours = 8639.40m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 1867.20m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 8997.30m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4492.95m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8639.40m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "May",
                                MonthNumber = 5,
                                Year = 2019,
                                AllDayWorkingDays = 21,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 1960.56m,
                                AmountPayableUnderTwo = 9447.17m,
                                AmountPayableTwoAndOver = 4717.60m,
                                AmountPayableTwentyHours = 9071.37m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 1960.56m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9447.17m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4717.60m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 9071.37m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "June",
                                MonthNumber = 6,
                                Year = 2019,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 1773.84m,
                                AmountPayableUnderTwo = 8547.44m,
                                AmountPayableTwoAndOver = 4268.30m,
                                AmountPayableTwentyHours = 8207.43m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 1773.84m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 8547.44m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4268.30m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8207.43m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            }
                        }
                    },
                    new OperationalFundingRequest
                    {
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        BusinessEntityId = new Guid("caf94574-f829-4865-98de-343e98687938"),
                        FundingPeriodMonth = FundingPeriodMonth.July,
                        FundingYear = 2020,
                        AdvanceMonths = new List<AdvanceMonthFundingComponent>
                        {
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "July",
                                MonthNumber = 7,
                                Year = 2019,
                                AllDayWorkingDays = 22,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 3080.88m,
                                AmountPayableUnderTwo = 10277.69m,
                                AmountPayableTwoAndOver = 5455.73m,
                                AmountPayableTwentyHours = 8525.06m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 3080.88m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 10277.69m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 5455.73m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8525.06m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "August",
                                MonthNumber = 8,
                                Year = 2019,
                                AllDayWorkingDays = 23,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 3080.88m,
                                AmountPayableUnderTwo = 10277.69m,
                                AmountPayableTwoAndOver = 5455.73m,
                                AmountPayableTwentyHours = 8525.06m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 3080.88m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 10277.69m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 5455.73m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8525.06m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "September",
                                MonthNumber = 9,
                                Year = 2019,
                                AllDayWorkingDays = 21,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2940.84m,
                                AmountPayableUnderTwo = 9810.52m,
                                AmountPayableTwoAndOver = 5207.74m,
                                AmountPayableTwentyHours = 8137.55m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2940.84m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9810.52m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 5207.74m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 8137.55m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "October",
                                MonthNumber = 10,
                                Year = 2019,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2800.80m,
                                AmountPayableUnderTwo = 9343.35m,
                                AmountPayableTwoAndOver = 4959.75m,
                                AmountPayableTwentyHours = 7750.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2800.80m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9343.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4959.75m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 7750.05m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            }
                        }
                    },
                    new OperationalFundingRequest
                    {
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        BusinessEntityId = new Guid("8983c640-4eb9-4000-8eaa-999017801ab6"),
                        FundingPeriodMonth = FundingPeriodMonth.November,
                        FundingYear = 2020,
                        AdvanceMonths = new List<AdvanceMonthFundingComponent>
                        {
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "November",
                                MonthNumber = 11,
                                Year = 2019,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2800.80m,
                                AmountPayableUnderTwo = 9343.35m,
                                AmountPayableTwoAndOver = 4959.75m,
                                AmountPayableTwentyHours = 7750.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2800.80m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9343.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4959.75m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 7750.05m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "December",
                                MonthNumber = 12,
                                Year = 2019,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2800.80m,
                                AmountPayableUnderTwo = 9343.35m,
                                AmountPayableTwoAndOver = 4959.75m,
                                AmountPayableTwentyHours = 7750.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2800.80m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9343.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4959.75m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 7750.05m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "January",
                                MonthNumber = 1,
                                Year = 2020,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2800.80m,
                                AmountPayableUnderTwo = 9343.35m,
                                AmountPayableTwoAndOver = 4959.75m,
                                AmountPayableTwentyHours = 7750.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2800.80m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9343.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4959.75m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 7750.05m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "February",
                                MonthNumber = 2,
                                Year = 2020,
                                AllDayWorkingDays = 20,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 2800.80m,
                                AmountPayableUnderTwo = 9343.35m,
                                AmountPayableTwoAndOver = 4959.75m,
                                AmountPayableTwentyHours = 7750.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 2800.80m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        Amount = 9343.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 4959.75m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 7750.05m,
                                        Rate = 10.1m,
                                        FundedChildHours = 30,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            }
                        }
                    },
                    new OperationalFundingRequest
                    {
                        OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                        BusinessEntityId = new Guid("C8EE3A99-1994-44E4-94E1-AB222589F1AC"),
                        FundingPeriodMonth = FundingPeriodMonth.March,
                        FundingYear = 2020,
                        EntitlementMonths = new List<EntitlementMonthFundingComponent>
                        {
                            new EntitlementMonthFundingComponent
                            {
                                MonthName = "October 2019",
                                MonthNumber = 10,
                                Year = 2019,
                                AllDayCertificatedTeacherHours = 54,
                                AllDayNonCertificatedTeacherHours = 46,
                                SessionalCertificatedTeacherHours = 0,
                                SessionalNonCertificatedTeacherHours = 0,
                                TotalWorkingDays = 3,
                                WashUpTwentyHours = 782.92m,
                                WashUpTwoAndOver = 440.25m,
                                WashUpPlusTen = 275.89m,
                                WashUpUnderTwo = 747.05m,
                                TotalWashUp = 2246.11m,
                                EntitlementFundingComponents = new List<EntitlementFundingComponent>
                                {
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 747.05m,
                                        Rate = 11.15m,
                                        FundedChildHours = 67,
                                        OperatingDays = 3,
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 440.25m,
                                        Rate = 5.87m,
                                        FundedChildHours = 75,
                                        OperatingDays = 3,
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 782.92m,
                                        Rate = 10.58m,
                                        FundedChildHours = 74,
                                        OperatingDays = 3,
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 275.89m,
                                        Rate = 5.87m,
                                        FundedChildHours = 47,
                                        OperatingDays = 3,
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new EntitlementMonthFundingComponent
                            {
                                MonthName = "November 2019",
                                MonthNumber = 11,
                                Year = 2019,
                                AllDayCertificatedTeacherHours = 7,
                                AllDayNonCertificatedTeacherHours = 13,
                                SessionalCertificatedTeacherHours = 0,
                                SessionalNonCertificatedTeacherHours = 0,
                                TotalWorkingDays = 3,
                                WashUpTwentyHours = 253.922m,
                                WashUpTwoAndOver = 29.35m,
                                WashUpPlusTen = 64.57m,
                                WashUpUnderTwo = 44.60m,
                                TotalWashUp = 392.44m,
                                EntitlementFundingComponents = new List<EntitlementFundingComponent>
                                {
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 44.60m,
                                        Rate = 10.1m,
                                        FundedChildHours = 4,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 29.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 5,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 253.92m,
                                        Rate = 10.1m,
                                        FundedChildHours = 24,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 64.57m,
                                        Rate = 10.1m,
                                        FundedChildHours = 11,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            },
                            new EntitlementMonthFundingComponent
                            {
                                MonthName = "December 2019",
                                MonthNumber = 12,
                                Year = 2019,
                                AllDayCertificatedTeacherHours = 0,
                                AllDayNonCertificatedTeacherHours = 0,
                                SessionalCertificatedTeacherHours = 0,
                                SessionalNonCertificatedTeacherHours = 0,
                                TotalWorkingDays = 0,
                                WashUpTwentyHours = 0.00m,
                                WashUpTwoAndOver = 0.005m,
                                WashUpPlusTen = 0.00m,
                                WashUpUnderTwo = 0.00m,
                                TotalWashUp = 0.00m,
                                EntitlementFundingComponents = new List<EntitlementFundingComponent>
                                {
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 44.60m,
                                        Rate = 10.1m,
                                        FundedChildHours = 4,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.UnderTwo,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 29.35m,
                                        Rate = 10.1m,
                                        FundedChildHours = 5,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 253.92m,
                                        Rate = 10.1m,
                                        FundedChildHours = 24,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new EntitlementFundingComponent
                                    {
                                        Amount = 64.57m,
                                        Rate = 10.1m,
                                        FundedChildHours = 11,
                                        OperatingDays = 1,
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        SessionTypeId = Session.Sessional
                                    }
                                }
                            },
                            new EntitlementMonthFundingComponent
                            {
                                MonthName = "January 2020",
                                MonthNumber = 01,
                                Year = 2020,
                                AllDayCertificatedTeacherHours = 0,
                                AllDayNonCertificatedTeacherHours = 0,
                                SessionalCertificatedTeacherHours = 0,
                                SessionalNonCertificatedTeacherHours = 0,
                                TotalWorkingDays = 0,
                                WashUpTwentyHours = 0.00m,
                                WashUpTwoAndOver = 0.005m,
                                WashUpPlusTen = 0.00m,
                                WashUpUnderTwo = 0.00m,
                                TotalWashUp = 0.00m
                            }
                        }
                    },
                    new OperationalFundingRequest
                    {
                        OrganisationId = 9933,
                        BusinessEntityId = new Guid("a3f81a55-bc83-4d99-ad4c-8267629b714b"),
                        FundingPeriodMonth = FundingPeriodMonth.July,
                        FundingYear = 2020,
                        AdvanceMonths = new List<AdvanceMonthFundingComponent>
                        {
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "October",
                                MonthNumber = 10,
                                Year = 2019,
                                AllDayWorkingDays = 0,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 995.96m,
                                AmountPayableUnderTwo = 0,
                                AmountPayableTwoAndOver = 398.39m,
                                AmountPayableTwentyHours = 14602.48m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 967.02m,
                                        Rate = 6.81m,
                                        FundedChildHours = 142,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 183.52m,
                                        Rate = 4.96m,
                                        FundedChildHours = 142,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 415.41m,
                                        Rate = 6.81m,
                                        FundedChildHours = 61,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 18784.98m,
                                        Rate = 11.61m,
                                        FundedChildHours = 1618,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 18784.98m,
                                        Rate = 6.39m,
                                        FundedChildHours = 150,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    }
                                }
                            }
                        }
                    },
                    new OperationalFundingRequest
                    {
                        OrganisationId = 9933,
                        BusinessEntityId = new Guid("86e6038c-6c82-4bef-80cf-e96a47b15eff"),
                        FundingPeriodMonth = FundingPeriodMonth.November,
                        FundingYear = 2020,
                        AdvanceMonths = new List<AdvanceMonthFundingComponent>
                        {
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "November",
                                MonthNumber = 11,
                                Year = 2019,
                                AllDayWorkingDays = 0,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 980.64m,
                                AmountPayableUnderTwo = 0,
                                AmountPayableTwoAndOver = 326.88m,
                                AmountPayableTwentyHours = 18668.88m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 1607.16m,
                                        Rate = 6.81m,
                                        FundedChildHours = 236,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 396.80m,
                                        Rate = 4.96m,
                                        FundedChildHours = 80,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 524.37m,
                                        Rate = 6.81m,
                                        FundedChildHours = 77,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 26447.58m,
                                        Rate = 11.61m,
                                        FundedChildHours = 2278,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 2178.99m,
                                        Rate = 6.39m,
                                        FundedChildHours = 341,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "December",
                                MonthNumber = 12,
                                Year = 2019,
                                AllDayWorkingDays = 0,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 612.9m,
                                AmountPayableUnderTwo = 0,
                                AmountPayableTwoAndOver = 204.3m,
                                AmountPayableTwentyHours = 11668.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 980.64m,
                                        Rate = 6.81m,
                                        FundedChildHours = 144,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 163.68m,
                                        Rate = 4.96m,
                                        FundedChildHours = 33,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 367.74m,
                                        Rate = 6.81m,
                                        FundedChildHours = 54,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 13351.50m,
                                        Rate = 11.61m,
                                        FundedChildHours = 1150,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 869.04m,
                                        Rate = 6.39m,
                                        FundedChildHours = 136,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.Sessional
                                    }
                                }
                            },
                            new AdvanceMonthFundingComponent
                            {
                                MonthName = "January",
                                MonthNumber = 1,
                                Year = 2020,
                                AllDayWorkingDays = 0,
                                SessionalWorkingDays = 0,
                                AmountPayablePlusTen = 612.9m,
                                AmountPayableUnderTwo = 0,
                                AmountPayableTwoAndOver = 204.3m,
                                AmountPayableTwentyHours = 11668.05m,
                                AdvanceFundingComponents = new List<AdvanceFundingComponent>
                                {
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.PlusTen,
                                        Amount = 124.74m,
                                        Rate = 5.20m,
                                        FundedChildHours = 24,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwoAndOver,
                                        Amount = 41.58m,
                                        Rate = 5.20m,
                                        FundedChildHours = 8,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    },
                                    new AdvanceFundingComponent
                                    {
                                        FundingComponentTypeId = FundingComponent.TwentyHours,
                                        Amount = 2375.82m,
                                        Rate = 8.87m,
                                        FundedChildHours = 268,
                                        OperatingDays = 20,
                                        SessionTypeId = Session.AllDay
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}
