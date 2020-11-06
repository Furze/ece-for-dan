using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    public class ReferenceData
    {
        private readonly ReferenceDataContext _referenceDataContext;
       

        public ReferenceData(ReferenceDataContext referenceDataContext)
        {
            _referenceDataContext = referenceDataContext;
        }

        public static Rs7Data Rs7Data => new Rs7Data();
        
        public static EceServiceReferenceData EceServices => new EceServiceReferenceData();

        public void SeedData()
        {
            // _rollContext.Rs7S.AddRange(Rs7Data.Data);
            // _rollContext.SaveChanges();
            
            _referenceDataContext.ExecuteWithIdentityInsertOn<EceService>(context => context.EceServices.AddRange(EceServices.Data));
            _referenceDataContext.SaveChanges();

            _schoolContext.SchoolRolls.AddRange(SchoolRolls.Data);
            _schoolContext.SaveChanges();
        }
    }
}