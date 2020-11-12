using Snapshooter;

namespace MoE.ECE.Integration.Tests.Infrastructure
{
    public class IgnoreFieldsFor
    {
        public static MatchOptions Rs7Model(MatchOptions options)
        {
            return options
                    .IgnoreField("Id")
                    .IgnoreField("BusinessEntityId")
                    .IgnoreField("ReceivedDate")
                    .IgnoreField("RevisionDate")
                    .IgnoreField("RequestId");
        }
    }
}