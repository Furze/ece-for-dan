using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.FundingPeriod;
using MoE.ECE.Domain.Read.Model.Services;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetServiceByIdHandler : IQueryHandler<GetServiceById, ECEServiceModel>
    {
        private readonly IMapper _mapper;
        private readonly ReferenceDataContext _referenceDataContext;
        private readonly ISystemClock _systemClock;

        public GetServiceByIdHandler(ReferenceDataContext referenceDataContext, IMapper mapper, ISystemClock systemClock)
        {
            _referenceDataContext = referenceDataContext;
            _mapper = mapper;
            _systemClock = systemClock;
        }

        public async Task<ECEServiceModel> Handle(GetServiceById query, CancellationToken cancellationToken)
        {
            var model = await _referenceDataContext.EceServices
                .AsNoTracking()
                .Include(d => d.OperatingSessions)
                .Where(service => service.RefOrganisationId == query.ServiceId)
                .SingleOrDefaultAsync(cancellationToken);

            if (model == null)
            {
                throw new ResourceNotFoundException($"{nameof(ECEServiceModel)} could not be found with id '{query.ServiceId}'");
            }

            var result = _mapper.Map<ECEServiceModel>(model);

            var today = _systemClock.UtcNow.ToNzDate();

            var currentRs7FundingPeriod = FundingPeriod.GetFundingPeriodForDate(today);
            result.CreatableRs7FundingPeriods = new[]
            {
                currentRs7FundingPeriod.EarlierFundingPeriod(3).StartDate,
                currentRs7FundingPeriod.EarlierFundingPeriod(2).StartDate,
                currentRs7FundingPeriod.PreviousFundingPeriod.StartDate,
                currentRs7FundingPeriod.StartDate,
                currentRs7FundingPeriod.NextFundingPeriod.StartDate,
            }.Select(date => new DateTime(date.Year, date.Month, date.Day).ToNzDateTimeOffSet()
            ).ToList();

            return result;
        }
    }
}