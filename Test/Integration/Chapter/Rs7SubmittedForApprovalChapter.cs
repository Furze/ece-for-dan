using Bard;
using Events.Integration.Protobuf.Workflow;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using Moe.ECE.Events.Integration;
using ProtoBuf.Bcl;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class Rs7SubmittedForApprovalChapter : Chapter<ECEStoryData>
    {
        public Rs7PeerRejectedChapter The_rs7_has_been_peer_review_rejected() =>
            When(context =>
                {
                    Returned? rejected = new Returned
                    {
                        BusinessEntityId =
                            new Guid(context.StoryData.Rs7Model.BusinessEntityId.GetValueOrDefault()),
                        BusinessEntityType = Constants.BusinessEntityTypes.Rs7
                    };

                    context.PublishIntegrationEvent(rejected);

                    context.StoryData.Rs7Model = context.GetDomainEvent<Rs7PeerRejected>();
                })
                .ProceedToChapter<Rs7PeerRejectedChapter>();

        public Rs7PeerApprovedChapter The_rs7_has_been_peer_approved() =>
            When(context =>
            {
                Approved? approved = new Approved
                {
                    BusinessEntityId = new Guid(context.StoryData.Rs7Model.BusinessEntityId.GetValueOrDefault()),
                    BusinessEntityType = Constants.BusinessEntityTypes.Rs7
                };

                context.PublishIntegrationEvent(approved);

                context.StoryData.Rs7Model = context.GetDomainEvent<Rs7PeerApproved>();
            }).ProceedToChapter<Rs7PeerApprovedChapter>();

        public EndChapter<ECEStoryData> The_rs7_declaration_has_been_updated() =>
            When(context =>
            {
                UpdateRs7Declaration? command = new UpdateRs7Declaration
                {
                    Id = context.StoryData.Rs7Model.Id,
                    Role = "role",
                    ContactPhone = "123",
                    FullName = "joe bloggs",
                    IsDeclaredTrue = true
                };

                context.CqrsExecute(command);

                context.StoryData.Rs7Model = context.GetDomainEvent<Rs7DeclarationUpdated>();
            }).End();
    }
}