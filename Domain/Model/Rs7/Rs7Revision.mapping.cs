using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7RevisionMapping : Profile
    {
        public Rs7RevisionMapping()
        {
            CreateMap<UpdateRs7, Rs7Revision>()
                .ConvertUsing<Rs7RevisionConverter>();

            CreateMap<CreateFullRs7, Rs7Revision>()
                .ConvertUsing<CreateRs7RevisionFromExternalConverter>();

            CreateMap<UpdateRs7EntitlementMonth, Rs7Revision>()
                .ConvertUsing<UpdateRs7EntitlementMonthToRs7RevisionConverter>();

            CreateMap<SaveAsDraft, Rs7Revision>()
                .ConvertUsing<Rs7RevisionConverter>();

            CreateMap<SubmitRs7ForApproval, Rs7Revision>().ConvertUsing<SubmitRs7ForApprovalToRs7RevisionConverter>();
        }
    }

    public static class MappingFunctions
    {
        public static void MapAdvanceMonths(IEnumerable<Rs7AdvanceMonthModel> sourceAdvanceMonths,
            ICollection<Rs7AdvanceMonth> destinationAdvanceMonths, ResolutionContext context)
        {
            foreach (var advance in sourceAdvanceMonths)
            {
                Rs7AdvanceMonth? match = destinationAdvanceMonths
                    .FirstOrDefault(month => month.MonthNumber == advance.MonthNumber && month.Year == advance.Year);

                //TODO: Remove:Mixture of mapping and validation 
                if (match == null)
                {
                    throw new BadRequestException(
                        "1020",
                        $"The provided advance month number {advance.MonthNumber} and year {advance.Year} is not valid for the given Rs7.");
                }

                context.Mapper.Map(advance, match);
            }
        }

        public static void MapEntitlementMonths(IEnumerable<Rs7EntitlementMonthModel>? entitlementMonthsSource,
            ICollection<Rs7EntitlementMonth> entitlementMonthsDestination)
        {
            if (entitlementMonthsSource == null)
            {
                return;
            }

            foreach (var sourceMonth in entitlementMonthsSource)
            {
                Rs7EntitlementMonth? destinationMonth = entitlementMonthsDestination
                    .FirstOrDefault(month =>
                        month.MonthNumber == sourceMonth.MonthNumber && month.Year == sourceMonth.Year);

                //TODO: Remove:Mixture of mapping and validation
                if (destinationMonth == null)
                {
                    throw new BadRequestException(
                        "1030",
                        $"The provided entitlement month number {sourceMonth.MonthNumber} and funding period year {sourceMonth.Year} is not valid for the given Rs7.");
                }

                MapEntitlementMonthDays(sourceMonth, destinationMonth);
            }
        }

        private static void MapEntitlementMonthDays(Rs7EntitlementMonthModel entitlement,
            Rs7EntitlementMonth monthMatch)
        {
            if (entitlement.Days == null)
            {
                return;
            }

            foreach (var entitlementDay in entitlement.Days)
            {
                Rs7EntitlementDay? dayMatch = monthMatch
                    .Days
                    .FirstOrDefault(day => day.DayNumber == entitlementDay.DayNumber);

                //TODO: Remove:Mixture of mapping and validation
                if (dayMatch == null)
                {
                    throw new BadRequestException(
                        "1040",
                        $"The provided day number {entitlementDay.DayNumber} is not valid for the given entitlement month {entitlement.MonthNumber}.");
                }

                dayMatch.Certificated = entitlementDay.Certificated;
                dayMatch.Hours20 = entitlementDay.Hours20;
                dayMatch.NonCertificated = entitlementDay.NonCertificated;
                dayMatch.TwoAndOver = entitlementDay.TwoAndOver;
                dayMatch.Plus10 = entitlementDay.Plus10;
                dayMatch.Under2 = entitlementDay.Under2;
            }
        }
    }

    public class SubmitRs7ForApprovalToRs7RevisionConverter : ITypeConverter<SubmitRs7ForApproval, Rs7Revision>
    {
        public Rs7Revision Convert(SubmitRs7ForApproval source, Rs7Revision destination, ResolutionContext context)
        {
            if (destination == null)
            {
                throw new InvalidOperationException(
                    $"A {nameof(UpdateRs7)} mapping must be given a target {nameof(Rs7)} to populate.");
            }

            //Rs7 business object properties are ignored as they are either set by Create, or updated by API.
            // + RevisionNumber is never changed
            // + RevisionDate is updated to system date
            if (source.AdvanceMonths != null)
            {
                MappingFunctions.MapAdvanceMonths(source.AdvanceMonths, destination.AdvanceMonths, context);
            }

            MappingFunctions.MapEntitlementMonths(source.EntitlementMonths, destination.EntitlementMonths);
            destination.IsAttested = source.IsAttested;
            return destination;
        }
    }

    public class Rs7RevisionConverter : ITypeConverter<Rs7Model, Rs7Revision>
    {
        public Rs7Revision Convert(Rs7Model source, Rs7Revision destination, ResolutionContext context)
        {
            if (destination == null)
            {
                throw new InvalidOperationException(
                    $"A {nameof(UpdateRs7)} mapping must be given a target {nameof(Rs7)} to populate.");
            }

            //Rs7 business object properties are ignored as they are either set by Create, or updated by API.
            // + RevisionNumber is never changed
            // + RevisionDate is updated to system date
            if (source.AdvanceMonths != null)
            {
                MappingFunctions.MapAdvanceMonths(source.AdvanceMonths, destination.AdvanceMonths, context);
            }

            MappingFunctions.MapEntitlementMonths(source.EntitlementMonths, destination.EntitlementMonths);
            destination.IsAttested = source.IsAttested;
            destination.IsZeroReturn = source.IsZeroReturn.GetValueOrDefault();
            destination.Declaration = context.Mapper.Map<Declaration>(source.Declaration);
            return destination;
        }
    }

    public class CreateRs7RevisionFromExternalConverter : ITypeConverter<CreateFullRs7, Rs7Revision>
    {
        public Rs7Revision Convert(CreateFullRs7 source, Rs7Revision destination, ResolutionContext context)
        {
            destination.Declaration = context.Mapper.Map<Declaration>(source.Declaration);
            destination.IsAttested = source.IsAttested;
            destination.IsZeroReturn = source.IsZeroReturn.GetValueOrDefault();
            destination.Source = source.Source;

            _ = source.AdvanceMonths?.Join(
                destination.AdvanceMonths,
                month => month.MonthNumber,
                month => month.MonthNumber,
                (inner, outer) =>
                {
                    context.Mapper.Map(inner, outer);

                    return outer;
                }).ToList();

            _ = source.EntitlementMonths?.Join(
                destination.EntitlementMonths,
                month => month.MonthNumber,
                month => month.MonthNumber,
                (inner, outer) =>
                {
                    context.Mapper.Map(inner, outer);

                    inner.Days?.ToList().ForEach(day =>
                    {
                        context.Mapper.Map(day, outer.Days.SingleOrDefault(d => d.DayNumber == day.DayNumber));
                    });

                    return outer;
                }).ToList();

            return destination;
        }
    }

    public class
        UpdateRs7EntitlementMonthToRs7RevisionConverter : ITypeConverter<UpdateRs7EntitlementMonth, Rs7Revision>
    {
        public Rs7Revision Convert(UpdateRs7EntitlementMonth source, Rs7Revision destination, ResolutionContext context)
        {
            if (destination == null)
            {
                throw new InvalidOperationException(
                    $"A {nameof(UpdateRs7EntitlementMonth)} mapping must be given a target {nameof(Rs7)} to populate.");
            }

            MappingFunctions.MapEntitlementMonths(new HashSet<Rs7EntitlementMonthModel> {source}
                , destination.EntitlementMonths);

            return destination;
        }
    }
}