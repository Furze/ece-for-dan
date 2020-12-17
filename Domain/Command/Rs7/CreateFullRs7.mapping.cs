using System;
using System.Linq;
using AutoMapper;
using Events.Integration.Protobuf.Eli;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Read.Model.Rs7;

namespace MoE.ECE.Domain.Command.Rs7
{
    public class CreateFullRs7Mapping : Profile
    {
        public CreateFullRs7Mapping()
        {
            CreateMap<Rs7Received, CreateFullRs7>()
                .ConvertUsing<Rs7ReceivedToCreateFullRs7Converter>();
        }
    }

    public class Rs7ReceivedToCreateFullRs7Converter : ITypeConverter<Rs7Received, CreateFullRs7>
    {
        private readonly ReferenceDataContext _context;

        public Rs7ReceivedToCreateFullRs7Converter(ReferenceDataContext context)
        {
            _context = context;
        }

        public CreateFullRs7 Convert(Rs7Received source, CreateFullRs7? destination, ResolutionContext context)
        {
            destination ??= new CreateFullRs7();
            
            destination.BusinessEntityId = Guid.NewGuid();
            destination.IsAttested = source.IsAttested;
            destination.Source = source.Source;

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
                return advanceMonth.Year;
            }

            var entitlementMonth = source.EntitlementMonths.FirstOrDefault();

            return entitlementMonth?.Year ?? 0;
        }
    }
}