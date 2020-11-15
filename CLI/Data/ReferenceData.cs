using MoE.ECE.Domain.Infrastructure.EntityFramework;

namespace MoE.ECE.CLI.Data
{
    public class ReferenceData
    {
        private readonly ReferenceDataContext _referenceDataContext;

        public ReferenceData(ReferenceDataContext referenceDataContext) => _referenceDataContext = referenceDataContext;

        public static Rs7Data Rs7Data => new();

        public static EceServiceReferenceData EceServices => new();

        public void SeedData()
        {
            _referenceDataContext.EceServices.AddRange(EceServices.Data);
            _referenceDataContext.SaveChanges();
        }
    }
}