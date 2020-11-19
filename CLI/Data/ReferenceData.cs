using System.Linq;
using Marten;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.OperationalFunding;

namespace MoE.ECE.CLI.Data
{
    public class ReferenceData
    {
        private readonly IDocumentSession _documentSession;
        private readonly ReferenceDataContext _referenceDataContext;

        public ReferenceData(ReferenceDataContext referenceDataContext, IDocumentSession documentSession)
        {
            _referenceDataContext = referenceDataContext;
            _documentSession = documentSession;
        }

        public static Rs7Data Rs7Data => new Rs7Data();

        public static EceServiceReferenceData EceServices => new EceServiceReferenceData();

        public static OperationalFundingReferenceData OperationalFunding => new OperationalFundingReferenceData();

        public void SeedData()
        {
            _referenceDataContext.EceServices.AddRange(EceServices.Data);
            _referenceDataContext.SaveChanges();
        }

        /// <summary>
        ///     Breaking out from Seed Data from now because we want to run in our Test Environment.
        /// </summary>
        public void TestData()
        {
            var businessEntityIds = OperationalFunding.Data.Select(request => $"'{request.BusinessEntityId.ToString().ToLower()}'").ToArray();
            var foo = string.Join(',', businessEntityIds);
            
            var existingRequests = _documentSession
                .Query<OperationalFundingRequest>($"data->> 'BusinessEntityId' in ({foo})");
                
                //.Where(request => businessEntityIds.Contains(request.BusinessEntityId)).ToList();

            foreach (var entity in OperationalFunding.Data)
            {
                var exists = existingRequests.Any(request => request.BusinessEntityId == entity.BusinessEntityId);

                if (exists == false)
                {
                    _documentSession.Insert(entity);
                }
            }

            _documentSession.SaveChanges();
        }
    }
}