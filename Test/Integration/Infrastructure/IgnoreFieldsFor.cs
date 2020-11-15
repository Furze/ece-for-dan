using Snapshooter;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class IgnoreFieldsFor
    {
        public static MatchOptions Rs7Model(MatchOptions options) =>
            options
                .IgnoreField("Id")
                .IgnoreField("BusinessEntityId")
                .IgnoreField("ReceivedDate")
                .IgnoreField("RevisionDate")
                .IgnoreField("RequestId");

        public static MatchOptions CollectionModelRs7Model(MatchOptions options) =>
            options
                .IgnoreFields("Data.[*].Id")
                .IgnoreFields("Data.[*].BusinessEntityId")
                .IgnoreFields("Data.[*].ReceivedDate")
                .IgnoreFields("Data.[*].RevisionDate")
                .IgnoreFields("Data.[*].RequestId");
    }
}