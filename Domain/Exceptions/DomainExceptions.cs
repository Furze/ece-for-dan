using System;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Exceptions
{
    public static class DomainExceptions
    {
        public static BadRequestException EceServiceNotFound(int? organisationId)
        {
            return new BadRequestException(ErrorCode.EceServiceNotFound, $"The supplied OrganisationId:'{organisationId}' could not be found.");
        }

        public static BadRequestException InvalidMonthForFundingPeriod(int? monthNumber)
        {
            return new BadRequestException(ErrorCode.InvalidMonthForFundingPeriod, $"The supplied month number '{monthNumber?.ToString()}' does not fall within the funding period for the RS3 application.");
        }

        public static BadRequestException InvalidRollStatusForDiscard(RollStatus rollStatus)
        {
            return new BadRequestException(ErrorCode.InvalidRollStatusForDiscard, $"It is not possible to discard the rs7 because it is in the status '{rollStatus}'.");
        }

        public static BadRequestException InvalidRollStatusForSubmitRs7ForApproval(RollStatus rollStatus)
        {
            return new BadRequestException(ErrorCode.InvalidRollStatusForSubmitRs7ForApproval, $"It is not possible to submit the rs7 for approval because it is in the status '{rollStatus}'.");
        }

        public static BadRequestException InvalidRollStatusForPeerRejectingRs7(RollStatus rollStatus)
        {
            return new BadRequestException(ErrorCode.InvalidRollStatusForPeerRejectingRs7, $"It is not possible to peer reject the rs7 because it is in the status '{rollStatus}'.");
        }

        public static BadRequestException InvalidUpdateRs7StatusNew()
        {
            return new BadRequestException(ErrorCode.InvalidUpdateRs7StatusNew, "Updates with New status are not allowed. Use Draft, or PendingApproval to submit");
        }

        public static BadRequestException InvalidRollStatusTransition(RollStatus currentRollStatus, RollStatus attemptedRollStatus)
        {
            return new BadRequestException(ErrorCode.InvalidRollStatusTransition, $"RollStatus cannot roll backwards. The current '{currentRollStatus}' RollStatus could not be changed to '{attemptedRollStatus}'.");
        }

        public static BadRequestException InvalidRollStatusForUpdate(RollStatus rollStatus)
        {
            return new BadRequestException(ErrorCode.InvalidRollStatusForUpdate,
                $"Invalid Roll status for update. The current status is {rollStatus}");
        }

        public static Exception ResourceNotFoundException<T>(Guid businessEntityId)
        {
            return new ResourceNotFoundException($"{typeof(T).Name} with businessEntityId {businessEntityId} does not exist.");
        }

        public static Exception ResourceNotFoundException<T>(int id)
        {
            return new ResourceNotFoundException($"{typeof(T).Name} with id {id} does not exist.");
        }

        public static BadRequestException DuplicateResourceFundingPeriod<T>(int refOrganisationId, string? month, int? year)
        {
            return new BadRequestException(ErrorCode.DuplicateResourceFundingPeriod, $"{typeof(T).Name} for EceService ({refOrganisationId}) with fundingPeriod {month} for year {year} already exists.");
        }

        public static BadRequestException EceServiceIneligibleBecauseStatusClosed(string? serviceName, int refOrganisationId)
        {
            return new BadRequestException(ErrorCode.EceServiceIneligibleBecauseStatusClosed, $"The EceService {serviceName} ({refOrganisationId}) is ineligible because it has been Closed for over 12 months");
        }

        public static BadRequestException EceServiceIneligibleBecauseLicenceStatus(string? serviceName, int refOrganisationId, string? licenseStatusDescription, int? licenceStatusId)
        {
            return new BadRequestException(ErrorCode.EceServiceIneligibleBecauseLicenceStatus, $"The EceService {serviceName} ({refOrganisationId}) is ineligible because its Licence status is {licenseStatusDescription} ({licenceStatusId})");
        }
    }
}