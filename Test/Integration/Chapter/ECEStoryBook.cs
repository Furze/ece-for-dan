using System;
using Bard;
using MoE.ECE.CLI.Data;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Event;
using MoE.ECE.Integration.Tests.Infrastructure;

namespace MoE.ECE.Integration.Tests.Chapter
{
    public class ECEStoryBook : StoryBook<ECEStoryData>
    {
        public Rs7CreatedChapter A_rs7_has_been_created(Action<CreateRs7>? setUpCommand = null)
        {
            return When(context =>
            {
                var command = ModelBuilder.CreateRs7(setUpCommand);

                setUpCommand?.Invoke(command);

                context.CqrsExecute(command);

                context.StoryData.Rs7Created = context.GetDomainEvent<Rs7Created>();
            }).ProceedToChapter<Rs7CreatedChapter>();
        }

        public Rs7CreatedChapter An_rs7_has_been_created_for_an_organisation_type(int organisationTypeId,
            Action<CreateRs7>? setUpCommand = null)
        {
            return When(context =>
            {
                var command = ModelBuilder.CreateRs7(setUpCommand);

                setUpCommand?.Invoke(command);

                var organisation = ReferenceData.EceServices
                    .GetByType(organisationTypeId);

                command.OrganisationId = organisation.RefOrganisationId;

                context.CqrsExecute(command);

                context.StoryData.Rs7Created = context.GetDomainEvent<Rs7Created>();
            }).ProceedToChapter<Rs7CreatedChapter>();
        }
    }
}