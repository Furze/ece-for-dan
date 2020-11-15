using System;
using MoE.ECE.Domain.Model.ValueObject;

namespace MoE.ECE.Domain.Exceptions
{
    public static class DomainExceptions
    {
        public static BadRequestException EceServiceNotFound(int? organisationId) => new BadRequestException(
            ErrorCode.EceServiceNotFound, $"The supplied OrganisationId:'{organisationId}' could not be found.");

        public static BadRequestException InvalidMonthForFundingPeriod(int? monthNumber) => new BadRequestException(
            ErrorCode.InvalidMonthForFundingPeriod,
            $"The supplied month number '{monthNumber?.ToString()}' does not fall within the funding period for the RS3 application.");

        public static BadRequestException InvalidRollStatusForDiscard(RollStatus rollStatus) => new BadRequestException(
            ErrorCode.InvalidRollStatusForDiscard,
            $"It is not possible to discard the rs7 because it is in the status '{rollStatus}'.");

        public static BadRequestException InvalidRollStatusForSubmitRs7ForApproval(RollStatus rollStatus) =>
            new BadRequestException(ErrorCode.InvalidRollStatusForSubmitRs7ForApproval,
                $"It is not possible to submit the rs7 for approval because it is in the status '{rollStatus}'.");

        public static BadRequestException InvalidRollStatusForPeerRejectingRs7(RollStatus rollStatus) =>
            new BadRequestException(ErrorCode.InvalidRollStatusForPeerRejectingRs7,
                $"It is not possible to peer reject the rs7 because it is in the status '{rollStatus}'.");

        public static BadRequestException InvalidUpdateRs7StatusNew() => new BadRequestException(
            ErrorCode.InvalidUpdateRs7StatusNew,
            "Updates with New status are not allowed. Use Draft, or PendingApproval to submit");

        public static BadRequestException
            InvalidRollStatusTransition(RollStatus currentRollStatus, RollStatus attemptedRollStatus) =>
            new BadRequestException(ErrorCode.InvalidRollStatusTransition,
                $"RollStatus cannot roll backwards. The current '{currentRollStatus}' RollStatus could not be changed to '{attemptedRollStatus}'.");

        public static BadRequestException InvalidRollStatusForUpdate(RollStatus rollStatus) =>
            new BadRequestException(ErrorCode.InvalidRollStatusForUpdate,
                $"Invalid Roll status for update. The current status is {rollStatus}");

        public static Exception ResourceNotFoundException<T>(Guid businessEntityId) =>
            new ResourceNotFoundException($"{typeof(T).Name} with businessEntityId {businessEntityId} does not exist.");

        public static Exception ResourceNotFoundException<T>(int id) =>
            new ResourceNotFoundException($"{typeof(T).Name} with id {id} does not exist.");

        public static BadRequestException
            DuplicateResourceFundingPeriod<T>(int refOrganisationId, string? month, int? year) =>
            new BadRequestException(ErrorCode.DuplicateResourceFundingPeriod,
                $"{typeof(T).Name} for EceService ({refOrganisationId}) with fundingPeriod {month} for year {year} already exists.");

        public static BadRequestException
            EceServiceIneligibleBecauseStatusClosed(string? serviceName, int refOrganisationId) =>
            new BadRequestException(ErrorCode.EceServiceIneligibleBecauseStatusClosed,
                $"The EceService {serviceName} ({refOrganisationId}) is ineligible because it has been Closed for over 12 months");

        public static BadRequestException EceServiceIneligibleBecauseLicenceStatus(string? serviceName,
            int refOrganisationId, string? licenseStatusDescription, int? licenceStatusId) => new BadRequestException(
            ErrorCode.EceServiceIneligibleBecauseLicenceStatus,
            $"The EceService {serviceName} ({refOrganisationId}) is ineligible because its Licence status is {licenseStatusDescription} ({licenceStatusId})");
    }
}