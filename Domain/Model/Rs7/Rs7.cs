using System;
using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Command.Rs7;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;
using Newtonsoft.Json;
using static MoE.ECE.Domain.Exceptions.DomainExceptions;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7 : BusinessEntity
    {
        public Rs7()
        {
        }

        public Rs7(int? organisationId, FundingPeriodMonth fundingPeriodMonth, int fundingPeriodYear)
        {
            OrganisationId = organisationId.GetValueOrDefault();
            FundingPeriod = fundingPeriodMonth;
            FundingPeriodYear = fundingPeriodYear;
            FundingYear =
                Model.FundingPeriod.FundingPeriod.GetFundingYearForFundingPeriod(fundingPeriodMonth,
                    fundingPeriodYear);
        }

        public FundingPeriodMonth FundingPeriod { get; set; }

        public int FundingYear { get; set; }

        public int FundingPeriodYear { get; set; }

        public int OrganisationId { get; set; }

        public RollStatus RollStatus { get; set; }

        public string RequestId => Id.ToString();

        /// <summary>
        ///     The Date was first submitted to the Ministry.
        /// </summary>
        public DateTimeOffset? ReceivedDate { get; set; }

        public ICollection<Rs7Revision> Revisions { get; set; } = new List<Rs7Revision>();

        [JsonIgnore]
        public Rs7Revision CurrentRevision
        {
            get
            {
                if (Revisions.Count == 0)
                    throw new ECEApplicationException("Rs7 does not contain any Revisions");

                return Revisions
                    .OrderByDescending(r => r.RevisionNumber)
                    .First();
            }
        }

        public bool IsZeroReturn => CurrentRevision.IsZeroReturn;

        
        public void PeerApprove(DateTimeOffset now)
        {
            UpdateWorkflowStatus(RollStatus.InternalReadyForReview, now);
        }

        public void PeerReject(DateTimeOffset now)
        {
            if (RollStatus != RollStatus.ExternalSubmittedForApproval)
                throw InvalidRollStatusForPeerRejectingRs7(RollStatus);

            UpdateWorkflowStatus(RollStatus.ExternalReturnedForEdit, now);
        }

        public void ApproveInternally(DateTimeOffset now)
        {
            UpdateWorkflowStatus(RollStatus.InternalApproved, now);
        }

        public void Decline(DateTimeOffset now)
        {
            UpdateWorkflowStatus(RollStatus.Declined, now);
        }

        public Rs7Revision GetRevision(int revisionNumber)
        {
            return Revisions.Single(revision => revision.RevisionNumber == revisionNumber);
        }

        public void CreateZeroReturn(in DateTimeOffset now)
        {
            RollStatus = RollStatus.InternalReadyForReview;
            ReceivedDate = now;

            // set the first revision
            var rs7Revision = CreateFirstRevision(now, Source.Internal);

            rs7Revision.IsZeroReturn = true;
            rs7Revision.IsAttested = false;
        }
        
        public void CreateSkeleton(in DateTimeOffset now, RollStatus initialStatus)
        {
            RollStatus = initialStatus;
            ReceivedDate = now;
            // build the first revision
            CreateFirstRevision(now, Source.Internal);
        }

        public void WasUpdated(in DateTimeOffset now)
        {
            // if the current Rs7 had already passed Draft status we create a new revision for the updates
            if (RollStatus > RollStatus.ExternalDraft)
            {
                CloneNextRevision(now);

                //only change the Source on a new revision, because we want the original Revision's Source to
                //retain whatever value it was created with.
                CurrentRevision.Source = Source.Internal;
            }

            CurrentRevision.IsZeroReturn = false;
            CurrentRevision.RevisionDate = now;
        }

        public bool CanBeDiscarded()
        {
            return new[]
            {
                RollStatus.ExternalDraft, 
                RollStatus.ExternalNew,
                RollStatus.ExternalReturnedForEdit, 
                RollStatus.ExternalSubmittedForApproval 
            }.Contains(RollStatus);
        }

        public void UpdateDeclaration(UpdateRs7Declaration command)
        {
            if (RollStatus != RollStatus.ExternalSubmittedForApproval) throw InvalidRollStatusForUpdate(RollStatus);

            var revision = CurrentRevision;

            revision.Declaration ??= new Declaration();

            revision.Declaration.Role = command.Role ?? string.Empty;
            revision.Declaration.ContactPhone = command.ContactPhone ?? string.Empty;
            revision.Declaration.FullName = command.FullName ?? string.Empty;
            revision.Declaration.IsDeclaredTrue = command.IsDeclaredTrue;
        }

        public void UpdateRollStatus(RollStatus newRollStatus, DateTimeOffset now)
        {
            // Don't allow save with New status. The consumer should provide Draft or PendingApproval
            if (newRollStatus == RollStatus.ExternalNew)
                throw InvalidUpdateRs7StatusNew();

            // Don't let the status move back into Draft!
            if (newRollStatus == RollStatus.ExternalDraft && RollStatus > RollStatus.ExternalDraft)
                throw InvalidRollStatusTransition(RollStatus, newRollStatus);

            // Once submitted, can not update roll data
            if (RollStatus != RollStatus.ExternalNew
                && RollStatus != RollStatus.ExternalDraft
                && RollStatus != RollStatus.ExternalSubmittedForApproval
                && RollStatus != RollStatus.ExternalReturnedForEdit)
                throw InvalidRollStatusForUpdate(RollStatus);

            WasUpdated(now);

            RollStatus = newRollStatus;
        }

        private void UpdateWorkflowStatus(RollStatus rollStatus, DateTimeOffset now)
        {
            RollStatus = rollStatus;

            CurrentRevision.RevisionDate = now;
        }
        
        /// <summary>
        ///     Creates a first Revision if one doesn't already exist.
        /// </summary>
        /// <param name="now">the current time</param>
        /// <param name="source"></param>
        /// <returns>Returns the first revision just created, or one that already existed</returns>
        /// <exception cref="Exception">Exception is thrown if the Rs7 already has more Revisions than the first</exception>
        private Rs7Revision CreateFirstRevision(DateTimeOffset now, string? source)
        {
            if (Revisions.Count > 1) throw new ECEApplicationException("Rs7 already has multiple revisions");

            var revision = Revisions.Count == 0 ? CreateNewRevision(now) : Revisions.First();

            if (source != null)
                revision.Source = source;

            return revision;
        }

        private Rs7Revision CreateNewRevision(DateTimeOffset now)
        {
            var rs7Revision = new Rs7Revision
            {
                Id = Revisions.Count + 1,
                RevisionNumber = Revisions.Count + 1,
                RevisionDate = now
            };

            Revisions.Add(rs7Revision);

            rs7Revision.CreateMonthsForPeriod(FundingPeriod, FundingPeriodYear);

            return rs7Revision;
        }

        private void CloneNextRevision(DateTimeOffset now)
        {
            var oldRevision = CurrentRevision;

            var rs7Revision = new Rs7Revision
            {
                Rs7Id = Id,
                Id = oldRevision.RevisionNumber + 1,
                RevisionNumber = oldRevision.RevisionNumber + 1,
                RevisionDate = now,
                AdvanceMonths = oldRevision.AdvanceMonths.Select(am => am.Clone()).ToList(),
                EntitlementMonths = oldRevision.EntitlementMonths.Select(em => em.Clone()).ToList(),
                IsAttested = oldRevision.IsAttested,
                IsZeroReturn = oldRevision.IsZeroReturn,
                Source = oldRevision.Source,
                Declaration = oldRevision.Declaration?.Clone()
            };

            Revisions.Add(rs7Revision);
        }

    }
}