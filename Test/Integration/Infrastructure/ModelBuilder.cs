using System;
using System.Linq;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Model.ValueObject;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.ECE.Events.Integration.ELI;
using Newtonsoft.Json;
using FundingPeriodMonth = MoE.ECE.Domain.Model.ValueObject.FundingPeriodMonth;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
     public class ModelBuilder
    {
        public static UpdateRs7EntitlementMonth UpdateRs7EntitlementMonth(Rs7Model rs7Model, int monthIndex, Action<UpdateRs7EntitlementMonth>? applyCustomerSetup = null)
        {
            var clonedRs7Model = Clone(rs7Model);

            var entitlementMonth1 = clonedRs7Model.EntitlementMonths?.ElementAt(monthIndex);

            var command = new UpdateRs7EntitlementMonth
            {
                Year = entitlementMonth1?.Year ?? 0,
                MonthNumber = entitlementMonth1?.MonthNumber ?? 0,
                Days = entitlementMonth1?.Days
            };
            applyCustomerSetup?.Invoke(command);
            return command;
        }

        public static UpdateRs7Declaration UpdateRs7Declaration(Action<UpdateRs7Declaration>? applyCustomerSetup = null)
        {
            var command = new UpdateRs7Declaration
            {
                Role = "role",
                ContactPhone = "123",
                FullName = "joe bloggs",
                IsDeclaredTrue = true
            };
            applyCustomerSetup?.Invoke(command);
            return command;
        }

        public Rs7PeerReject Rs7PeerReject(Guid businessEntityId, Action<Rs7PeerReject>? applyCustomSetup = null)
        {
            var command = new Rs7PeerReject(businessEntityId);
            applyCustomSetup?.Invoke(command);
            return command;
        }
       
        public static SubmitRs7ForApproval SubmitRs7ForApproval(Rs7Model rs7Model, Action<SubmitRs7ForApproval>? applyCustomSetup = null)
        {
            var clonedRs7Model = Clone(rs7Model);
            var command = new SubmitRs7ForApproval
            {
                IsAttested = true,
                AdvanceMonths = clonedRs7Model.AdvanceMonths,
                EntitlementMonths = clonedRs7Model.EntitlementMonths,
            };

            if (command.AdvanceMonths != null)
                foreach (var advanceMonth in command.AdvanceMonths)
                {
                    // Default organisation Montessori little hands cannot capture Sessional & Parent Led Days
                    advanceMonth.AllDay = 2;
                    advanceMonth.Sessional = null;
                    advanceMonth.ParentLed = null;
                }

            command.Id = rs7Model.Id;
            applyCustomSetup?.Invoke(command);
            return command;
        }

        public static SaveAsDraft SaveAsDraft(Rs7Model rs7Model, Action<SaveAsDraft>? applyCustomSetup = null)
        {
            // We need to clone because the underlying Rs7Model that was created is shared across multiple
            // tests and we get tests failures.
            var clonedRs7Model = Clone(rs7Model);
            var command = new SaveAsDraft
            {
                Id = clonedRs7Model.Id,
                AdvanceMonths = clonedRs7Model.AdvanceMonths,
                EntitlementMonths = clonedRs7Model.EntitlementMonths,
                FundingPeriod = clonedRs7Model.FundingPeriod,
                FundingYear = clonedRs7Model.FundingYear,
                OrganisationId = clonedRs7Model.OrganisationId,
                ReceivedDate = clonedRs7Model.ReceivedDate,
                RevisionDate = clonedRs7Model.RevisionDate,
                RevisionNumber = clonedRs7Model.RevisionNumber,
                RollStatus = RollStatus.ExternalDraft,
                BusinessEntityId = clonedRs7Model.BusinessEntityId,
                Declaration = null,
                IsAttested = null
            };

            if (command.AdvanceMonths != null)
                foreach (var advanceMonth in command.AdvanceMonths)
                {
                    // Default organisation Montessori little hands cannot capture Sessional & Parent Led Days
                    advanceMonth.AllDay = 2;
                    advanceMonth.Sessional = null;
                    advanceMonth.ParentLed = null;
                }

            applyCustomSetup?.Invoke(command);

            return command;
        }

        public static UpdateRs7 UpdateRs7(Rs7Model rs7Model, Action<UpdateRs7>? applyCustomSetup = null)
        {
            // We need to clone because the underlying Rs7Model that was created is shared across multiple
            // tests and we get tests failures.
            var clonedRs7Model = Clone(rs7Model);
            UpdateRs7 command = new UpdateRs7
            {
                Id = clonedRs7Model.Id,
                AdvanceMonths = clonedRs7Model.AdvanceMonths,
                EntitlementMonths = clonedRs7Model.EntitlementMonths,
                FundingPeriod = clonedRs7Model.FundingPeriod,
                FundingYear = clonedRs7Model.FundingYear,
                FundingPeriodYear = clonedRs7Model.FundingPeriodYear,
                OrganisationId = clonedRs7Model.OrganisationId,
                ReceivedDate = clonedRs7Model.ReceivedDate,
                RevisionDate = clonedRs7Model.RevisionDate,
                RevisionNumber = clonedRs7Model.RevisionNumber,
                RollStatus = RollStatus.InternalReadyForReview,
                BusinessEntityId = clonedRs7Model.BusinessEntityId,
                Declaration = new DeclarationModel
                {
                    FullName = "Donald Trump",
                    ContactPhone = "021509753",
                    Role = "President",
                    IsDeclaredTrue = true
                },
                IsAttested = true,
                IsZeroReturn = clonedRs7Model.IsZeroReturn,
            };

            if (command.AdvanceMonths != null)
            {
                foreach (var advanceMonth in command.AdvanceMonths)
                {
                    // Default organisation Montessori little hands cannot capture Sessional & Parent Led Days
                    advanceMonth.AllDay = 2;
                    advanceMonth.Sessional = null;
                    advanceMonth.ParentLed = null;
                }
            }

            if (command.EntitlementMonths != null)
            {
                foreach (var entitlementMonth in command.EntitlementMonths)
                {
                    if (entitlementMonth.Days == null)
                    {
                        continue;
                    }

                    foreach (var day in entitlementMonth.Days)
                    {
                        day.Under2 = 18;
                        day.Plus10 = 23;
                        day.Certificated = 1;
                        day.TwoAndOver = 1;
                        day.NonCertificated = 2;
                    }
                }
            }

            applyCustomSetup?.Invoke(command);

            return command;
        }

        private static T Clone<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var clone = JsonConvert.DeserializeObject<T>(json);
            return clone;
        }
      
        public static Rs7Model Rs7Model(Action<Rs7Model>? applyCustomSetup = null)
        {
            var model = new Rs7Model
            {
                OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                FundingPeriodYear = DateTimeOffset.Now.Year,
                FundingPeriod = FundingPeriodMonth.July
            };

            applyCustomSetup?.Invoke(model);
            return model;
        }

        public static CreateSkeletonRs7 CreateSkeletonRs7(Action<CreateSkeletonRs7>? applyCustomSetup = null)
        {
            var command = new CreateSkeletonRs7
            {
                OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                FundingPeriodYear = DateTimeOffset.Now.Year,
                FundingPeriod = FundingPeriodMonth.July
            };

            applyCustomSetup?.Invoke(command);
            return command;
        }

        public static CreateRs7ZeroReturn CreateRs7ZeroReturn(Action<CreateRs7ZeroReturn>? applyCustomSetup = null)
        {
            var command = new CreateRs7ZeroReturn
            {
                OrganisationId = ReferenceData.EceServices.MontessoriLittleHands.RefOrganisationId,
                FundingPeriodYear = DateTimeOffset.Now.Year,
                FundingPeriod = FundingPeriodMonth.July
            };

            applyCustomSetup?.Invoke(command);
            return command;
        }

        public static Rs7Received Rs7Received(Action<Rs7Received>? modifyEvent = null)
        {
            var integrationEvent = new Rs7Received
            {
                OrganisationNumber = ReferenceData.EceServices.MontessoriLittleHands.OrganisationNumber,
                FundingPeriod = Moe.ECE.Events.Integration.ELI.FundingPeriodMonth.July,
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

            modifyEvent?.Invoke(integrationEvent);
            
            return integrationEvent;
        }
    }
}