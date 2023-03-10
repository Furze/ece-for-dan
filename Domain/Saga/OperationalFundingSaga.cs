using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marten;
using MediatR;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Event.OperationalFunding;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Model.OperationalFunding;
using MoE.ECE.Domain.Services;
using MoE.ECE.Domain.Services.Opa.Request;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Saga
{
    public class OperationalFundingSaga :
        IHandleACommand<CreateOperationalFundingRequest>
    {
        private readonly ICqrs _cqrs;

        private readonly IDocumentSession _documentSession;
        private readonly IMapper _mapper;
        private readonly ReferenceDataContext _referenceDataContext;
        private readonly IOperationalFundingCalculator _operationalFundingCalculator;
        private readonly ISystemClock _systemClock;

        public OperationalFundingSaga(IDocumentSession documentSession, ICqrs cqrs,
            IOperationalFundingCalculator operationalFundingCalculator,
            ISystemClock systemClock, IMapper mapper, ReferenceDataContext referenceDataContext)
        {
            _documentSession = documentSession;
            _cqrs = cqrs;
            _operationalFundingCalculator = operationalFundingCalculator;
            _systemClock = systemClock;
            _mapper = mapper;
            _referenceDataContext = referenceDataContext;
        }

        public async Task<Unit> Handle(CreateOperationalFundingRequest command, CancellationToken cancellationToken)
        {
            var operationalFundingOpaRequest = _mapper.Map<OpaRequest<OperationalFundingBaseRequest>>(command);

            var operationalFundingOpaResponse =
                await _operationalFundingCalculator.CalculateAsync(operationalFundingOpaRequest);

            if (operationalFundingOpaResponse == null)
                throw new ECEApplicationException("Received a null operational funding response from OPA");

            if (operationalFundingOpaResponse.Cases.Any(entity => entity.Errors != null))
                throw new ECEApplicationException("Received an error from OPA for the operational funding request");
           
            var firstResponseRecord = operationalFundingOpaResponse.Cases.First();

            var operationalFunding = new OperationalFundingRequest(command, _systemClock.UtcNow);

            _mapper.Map(firstResponseRecord, operationalFunding);

            operationalFunding.OpaRequest = operationalFundingOpaRequest;
            operationalFunding.OpaResponse = operationalFundingOpaResponse;

            await AddBusinessExceptionsAsync(operationalFunding);
            
            _documentSession.Insert(operationalFunding);

            await _documentSession.SaveChangesAsync(cancellationToken);

            var domainEvent = _mapper.Map<OperationalFundingRequestCreated>(operationalFunding);

            await _cqrs.RaiseEventAsync(domainEvent, cancellationToken);

            return Unit.Value;
        }

        private async Task AddBusinessExceptionsAsync(OperationalFundingRequest operationalFunding)
        {
            var organisation = await _referenceDataContext.EceServices.FindAsync(operationalFunding.OrganisationId);

            if (organisation.IsFunded == false)
            {
                operationalFunding.AddBusinessException<FundingWithheldBusinessException>();
            }

            if (organisation.InstallmentPayments == true)
            {
                operationalFunding.AddBusinessException<ServiceMonthlyFundingBusinessException>();
            }
        }
    }
}