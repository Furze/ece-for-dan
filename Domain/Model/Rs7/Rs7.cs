using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Model.Rs7
{
    public class Rs7 : BusinessEntity
    {
        public FundingPeriodMonth FundingPeriod { get; set; }

        public int FundingYear { get; set; }

        public int FundingPeriodYear { get; set; }

        public int OrganisationId { get; set; }

        public RollStatus RollStatus { get; set; }

        public string RequestId => Id.ToString();

        /// <summary>
        /// The Date was first submitted to the Ministry.
        /// </summary>
        public DateTimeOffset? ReceivedDate { get; set; }

        public virtual ICollection<Rs7Revision> Revisions { get; set; } = new HashSet<Rs7Revision>();

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

        /// <summary>
        /// Creates a first Revision if one doesn't already exist.
        /// </summary>
        /// <param name="systemClock"></param>
        /// <returns>Returns the first revision just created, or one that already existed</returns>
        /// <exception cref="Exception">Exception is thrown if the Rs7 already has more Revisions than the first</exception>
        public Rs7Revision CreateFirstRevision(ISystemClock systemClock)
        {
            if (Revisions.Count > 1)
            {
                throw new Exception("Rs7 already has multiple revisions");
            }

            return Revisions.Count == 0 ? CreateNewRevision(systemClock) : Revisions.First();
        }

        public Rs7Revision CreateNewRevision(ISystemClock systemClock)
        {
            var rs7Revision = new Rs7Revision
            {
                Rs7 = this,
                RevisionNumber = Revisions.Count + 1,
                RevisionDate = systemClock.UtcNow
            };

            Revisions.Add(rs7Revision);

            rs7Revision.CreateMonthsForPeriod();

            return rs7Revision;
        }

        public Rs7Revision CloneNextRevision(ISystemClock systemClock)
        {
            var oldRevision = CurrentRevision;

            var rs7Revision = new Rs7Revision
            {
                Rs7 = this,
                Rs7Id = Id,
                RevisionNumber = oldRevision.RevisionNumber + 1,
                RevisionDate = systemClock.UtcNow,
                AdvanceMonths = oldRevision.AdvanceMonths.Select(am => am.Clone()).ToHashSet(),
                EntitlementMonths = oldRevision.EntitlementMonths.Select(em => em.Clone()).ToHashSet(),
                IsAttested = oldRevision.IsAttested,
                IsZeroReturn = oldRevision.IsZeroReturn,
                Source = oldRevision.Source,
                Declaration = oldRevision.Declaration.Clone(),
            };



            Revisions.Add(rs7Revision);

            return rs7Revision;
        }

        public void PeerApprove(ISystemClock systemClock)
        {
            SetStatus(RollStatus.InternalReadyForReview,  systemClock);
        }

        public void PeerReject(ISystemClock systemClock)
        {
            if (RollStatus != RollStatus.ExternalSubmittedForApproval)
            {
                throw DomainExceptions.InvalidRollStatusForPeerRejectingRs7(RollStatus);
            }

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
    }
}