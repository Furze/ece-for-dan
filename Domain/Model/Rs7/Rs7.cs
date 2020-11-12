using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Internal;
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
                    throw new Exception("Rs7 does not contain any Revisions");

                return Revisions
                    .OrderByDescending(r => r.RevisionNumber)
                    .First();
            }
        }

        public bool IsZeroReturn => CurrentRevision.IsZeroReturn;

        /// <summary>
        ///     Creates a first Revision if one doesn't already exist.
        /// </summary>
        /// <param name="now">the current time</param>
        /// <param name="source"></param>
        /// <returns>Returns the first revision just created, or one that already existed</returns>
        /// <exception cref="Exception">Exception is thrown if the Rs7 already has more Revisions than the first</exception>
        public Rs7Revision CreateFirstRevision(DateTimeOffset now, string? source)
        {
            if (Revisions.Count > 1) throw new Exception("Rs7 already has multiple revisions");

            var revision = Revisions.Count == 0 ? CreateNewRevision(now) : Revisions.First();

            if (source != null)
                revision.Source = source;

            return revision;
        }

        public Rs7Revision CreateNewRevision(DateTimeOffset now)
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

        public Rs7Revision CloneNextRevision(DateTimeOffset now)
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

            return rs7Revision;
        }

        public void PeerApprove(ISystemClock systemClock)
        {
            SetStatus(RollStatus.InternalReadyForReview, systemClock);
        }

        public void PeerReject(ISystemClock systemClock)
        {
            if (RollStatus != RollStatus.ExternalSubmittedForApproval)
                throw DomainExceptions.InvalidRollStatusForPeerRejectingRs7(RollStatus);

            SetStatus(RollStatus.ExternalReturnedForEdit, systemClock);
        }

        public void ApproveInternally(ISystemClock systemClock)
        {
            SetStatus(RollStatus.InternalApproved, systemClock);
            ReceivedDate = systemClock.UtcNow;
        }

        public void Decline(ISystemClock systemClock)
        {
            SetStatus(RollStatus.Declined, systemClock);
        }

        private void SetStatus(RollStatus rollStatus, ISystemClock systemClock)
        {
            RollStatus = rollStatus;

            CurrentRevision.UpdateRevisionDate(systemClock);
        }

        public Rs7Revision GetRevision(int revisionNumber)
        {
            return Revisions.Single(revision => revision.RevisionNumber == revisionNumber);
        }

        public void SetAsZeroReturn(in DateTimeOffset now)
        {
            RollStatus = RollStatus.InternalReadyForReview;
            ReceivedDate = now;

            // set the first revision
            var rs7Revision = CreateFirstRevision(now, Source.Internal);

            rs7Revision.IsZeroReturn = true;
            rs7Revision.IsAttested = false;
        }

        public void CreatedByExternalUser(in DateTimeOffset now)
        {
            RollStatus = RollStatus.ExternalNew;
            ReceivedDate = now;
            // build the first revision
            CreateFirstRevision(now, Source.Internal);
        }

        public void CreatedFromExternalSystem(in DateTimeOffset now)
        {
            // go straight to PendingApproval and received
            RollStatus = RollStatus.InternalReadyForReview;
            ReceivedDate = now;

            // build the first revision
            CreateFirstRevision(now, null);
        }

        public void EntitlementMonthUpdated(in DateTimeOffset now)
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
            //CurrentRevision.UpdateRevisionDate(now);
        }

        public bool CanBeDiscarded()
        {
            return RollStatus == RollStatus.ExternalDraft || RollStatus == RollStatus.ExternalNew ||
                   RollStatus == RollStatus.ExternalReturnedForEdit;
        }

        public void UpdateDeclaration(UpdateRs7Declaration command)
        {
            if (RollStatus != RollStatus.ExternalSubmittedForApproval)
            {
                throw InvalidRollStatusForUpdate(RollStatus);
            }
            
            var revision = CurrentRevision;
            
            revision.Declaration ??= new Declaration();
            
            revision.Declaration.Role = command.Role ?? string.Empty;
            revision.Declaration.ContactPhone = command.ContactPhone ?? string.Empty;
            revision.Declaration.FullName = command.FullName ?? string.Empty;
            revision.Declaration.IsDeclaredTrue = command.IsDeclaredTrue;
        }
    }
}