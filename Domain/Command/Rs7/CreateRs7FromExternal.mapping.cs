using System;
using System.Linq;
using AutoMapper;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Read.Model.Rs7;
using Moe.ECE.Events.Integration.ELI;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateRs7FromExternalMapping : Profile
    {
        public CreateRs7FromExternalMapping()
        {
            CreateMap<Rs7Received, CreateRs7FromExternal>()
                .ConvertUsing<Rs7ReceivedToCreateRs7FromExternalConverter>();
        }
    }

    public class Rs7ReceivedToCreateRs7FromExternalConverter : ITypeConverter<Rs7Received, CreateRs7FromExternal>
    {
        private readonly ReferenceDataContext _context;

        public Rs7ReceivedToCreateRs7FromExternalConverter(ReferenceDataContext context)
        {
            _context = context;
        }

        public CreateRs7FromExternal Convert(Rs7Received source, CreateRs7FromExternal? destination, ResolutionContext context)
        {
            destination ??= new CreateRs7FromExternal();
            
            destination.BusinessEntityId = Guid.NewGuid();
            destination.IsAttested = source.IsAttested;
            destination.Source = source.Source ?? string.Empty;

            destination.Declaration = new DeclarationModel
            {
                FullName = source.Declaration?.FullName,
                ContactPhone = source.Declaration?.ContactPhone,
                IsDeclaredTrue = true,
                Role = source.Declaration?.Designation
            };

            destination.FundingPeriod = (Model.ValueObject.FundingPeriodMonth) source.FundingPeriod;
            destination.FundingPeriodYear = GetFundingPeriodYear(source);
            destination.OrganisationId = _context.EceServices
                .SingleOrDefault(ece => ece.OrganisationNumber == source.OrganisationNumber)
                ?.RefOrganisationId;

            destination.EntitlementMonths = context.Mapper.Map<Rs7EntitlementMonthModel[]>(source.EntitlementMonths);
            destination.AdvanceMonths = context.Mapper.Map<Rs7AdvanceMonthModel[]>(source.AdvanceMonths);
            destination.IsZeroReturn = false;

            return destination;
        }

        private static int GetFundingPeriodYear(Rs7Received source)
        {
            var advanceMonth = source.AdvanceMonths.FirstOrDefault();

            if (advanceMonth != null)
            {
                return advanceMonth.FundingPeriodYear;
            }

            var entitlementMonth = source.EntitlementMonths.FirstOrDefault();

            return entitlementMonth?.FundingPeriodYear ?? 0;
        }
    }
}