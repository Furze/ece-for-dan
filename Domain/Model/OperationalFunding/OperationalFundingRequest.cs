using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Command;
using MoE.ECE.Domain.Infrastructure.Services.Opa;
using MoE.ECE.Domain.Services.Opa.Request;
using MoE.ECE.Domain.Services.Opa.Response;

namespace MoE.ECE.Domain.Model.OperationalFunding
{
    public class OperationalFundingRequest : FundingRequest
    {
        public OperationalFundingRequest()
        {
        }

        public OperationalFundingRequest(CreateOperationalFundingRequest command, in DateTimeOffset now)
        {
            OrganisationId = command.OrganisationId;
            BusinessEntityId = command.BusinessEntityId;
            RequestId = command.RequestId;
            RevisionNumber = command.RevisionNumber;
            CreationDate = now;
        }

        public ICollection<EntitlementMonthFundingComponent>? EntitlementMonths { get; set; } = new List<EntitlementMonthFundingComponent>();

        public override string FundingDescription => "Existing Service Application for Funding (RS7)";
        public OpaRequest<OperationalFundingBaseRequest>? OpaRequest { get; set; }
        public OpaResponse<OperationalFundingBaseResponse>? OpaResponse { get; set; }
    }
}
