using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Internal;
using MoE.ECE.Domain.Exceptions;
using MoE.ECE.Domain.Model.ValueObject;
using Newtonsoft.Json;

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
                MoE.ECE.Domain.Model.FundingPeriod.FundingPeriod.GetFundingYearForFundingPeriod(fundingPeriodMonth,
                    fundingPeriodYear);
        }

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
        /// Creates a first Revision if one doesn't already exist.
        /// </summary>
        /// <param name="now">the current time</param>
        /// <returns>Returns the first revision just created, or one that already existed</returns>
        /// <exception cref="Exception">Exception is thrown if the Rs7 already has more Revisions than the first</exception>
        public Rs7Revision CreateFirstRevision(DateTimeOffset now)
        {
            if (Revisions.Count > 1)
            {
                throw new Exception("Rs7 already has multiple revisions");
            }

            var revision = Revisions.Count == 0 ? CreateNewRevision(now) : Revisions.First();

            revision.Source = Source.Internal;

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

        public Rs7Revision CloneNextRevision(ISystemClock systemClock)
        {
            var oldRevision = CurrentRevision;

            var rs7Revision = new Rs7Revision
            {
                Rs7Id = Id,
                Id = oldRevision.RevisionNumber + 1,
                RevisionNumber = oldRevision.RevisionNumber + 1,
                RevisionDate = systemClock.UtcNow,
                AdvanceMonths = oldRevision.AdvanceMonths.Select(am => am.Clone()).ToList(),
                EntitlementMonths = oldRevision.EntitlementMonths.Select(em => em.Clone()).ToList(),
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

        public Rs7Revision GetRevision(int revisionNumber)
        {
            return Revisions.Single(revision => revision.RevisionNumber == revisionNumber);
        }

        public void SetAsZeroReturn(in DateTimeOffset now)
        {
            RollStatus = RollStatus.InternalReadyForReview;
            ReceivedDate = now;

            // set the first revision
            var rs7Revision = CreateFirstRevision(now);
            
            rs7Revision.IsZeroReturn = true;
            rs7Revision.IsAttested = false;
        }

        public void CreatedByExternalUser(in DateTimeOffset now)
        {
            RollStatus = RollStatus.ExternalNew;
            ReceivedDate = now;
            // build the first revision
            CreateFirstRevision(now);
        }

        public void CreatedFromExternalSystem(in DateTimeOffset now)
        {
            // go straight to PendingApproval and received
            RollStatus = RollStatus.InternalReadyForReview;
            ReceivedDate = now;

            // build the first revision
            CreateFirstRevision(now);
        }
    }
}