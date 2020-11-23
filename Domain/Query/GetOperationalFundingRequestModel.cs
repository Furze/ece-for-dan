using System;
using System.Collections.Generic;
using MoE.ECE.Domain.Read.Model.OperationalFunding;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class GetOperationalFundingRequestModel : PaginationParameters, IQuery<ICollection<OperationalFundingRequestModel>>
    {
        public GetOperationalFundingRequestModel(
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