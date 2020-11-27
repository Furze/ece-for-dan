using Snapshooter;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public static class IgnoreFieldsFor
    {
        public static MatchOptions CollectionOperationalFundingRequestModel(MatchOptions options)
        {
            return options
                    .IgnoreFields("[*].BusinessEntityId")
                    .IgnoreFields("[*].RequestId");
        }
        public static MatchOptions Rs7Model(MatchOptions options)
        {
            return options
                    .IgnoreField("Id")
                    .IgnoreField("BusinessEntityId")
                    .IgnoreField("ReceivedDate")
                    .IgnoreField("RevisionDate")
                    .IgnoreField("RequestId");
        }

        public static MatchOptions CollectionModelRs7Model(MatchOptions options)
        {
            return options
                .IgnoreFields("Data.[*].Id")
                .IgnoreFields("Data.[*].BusinessEntityId")
                .IgnoreFields("Data.[*].ReceivedDate")
                .IgnoreFields("Data.[*].RevisionDate")
                .IgnoreFields("Data.[*].RequestId");
        }
    }
}