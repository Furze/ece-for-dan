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

        public static EceServiceProviderReferenceData EceServiceProviders => new EceServiceProviderReferenceData();

        public static EceServiceReferenceData EceServices => new EceServiceReferenceData();

        public static EceServiceDateRangedParameterReferenceData EceServiceDateRangedParameters => new EceServiceDateRangedParameterReferenceData();

        public static EceLicencingDetailDateRangedParameterReferenceData EceLicencingDetailDateRangedParameters => new EceLicencingDetailDateRangedParameterReferenceData();

        public static EceOperatingSessionDateRangedParameterReferenceData EceOperatingSessionDateRangedParameters => new EceOperatingSessionDateRangedParameterReferenceData();

        
        public static OperationalFundingReferenceData OperationalFunding => new OperationalFundingReferenceData();

        public void SeedData()
        {
            _referenceDataContext.EceServiceProviders.AddRange(EceServiceProviders.Data);
            _referenceDataContext.EceServices.AddRange(EceServices.Data);
            _referenceDataContext.EceServiceDateRangedParameters.AddRange(EceServiceDateRangedParameters.Data);
            _referenceDataContext.EceLicencingDetailDateRangedParameters.AddRange(EceLicencingDetailDateRangedParameters.Data);
            _referenceDataContext.EceOperatingSessionDateRangedParameters.AddRange(EceOperatingSessionDateRangedParameters.Data);

            _referenceDataContext.SaveChanges();
        }

        /// <summary>
        ///     Breaking out from Seed Data from now because we want to run in our Test Environment.
        /// </summary>
        public void TestData()
        {
            var businessEntityIds = OperationalFunding.Data.Select(request => $"'{request.BusinessEntityId.ToString().ToLower()}'").ToArray();
            var commaSeparatedIds = string.Join(',', businessEntityIds);
            
            var existingRequests = _documentSession
                .Query<OperationalFundingRequest>($"data->> 'BusinessEntityId' in ({commaSeparatedIds})");

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