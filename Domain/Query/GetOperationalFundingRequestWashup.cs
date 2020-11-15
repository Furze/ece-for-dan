using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetOperationalFundingRequestWashup : PaginationParameters, IQuery<ICollection<OperationalFundingRequestModel>>
    {
        public GetOperationalFundingRequestWashup(
            Guid businessEntityId,
            int? revisionNumber)
        {
            BusinessEntityId = businessEntityId;
            RevisionNumber = revisionNumber;
        }

        public Guid BusinessEntityId { get; set; }

        public int? RevisionNumber { get; set; }
    }
}