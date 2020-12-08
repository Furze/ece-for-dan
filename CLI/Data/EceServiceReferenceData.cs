using System;
using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    /// <summary>
    ///     Static Helper class for providing ReferenceData into our database where we don't have ADF working such
    ///     as local developer databases & integration test databases.
    /// </summary>
    public class EceServiceReferenceData
    {
        private const string PorseMarlboroughOrganisationNumber = "30143";
        private const string FoxtonPlaycentreOrganisationNumber = "52001";
        private const string EducationAndCareDescription = "Education & Care Service";
        private const string MontessoriOrganisationNumber = "46543";
        private const string ThreeKingsOrganisationNumber = "20557";
        private const string TestClosedServiceOrganisationNumber = "666";
        private const string TeKohangaReoTypeDescription = "Te Kohanga Reo";
        private const string PlaycentreTypeDescription = "Playcentre";
        private const string FamilyTiesOrganisationNumber = "83070";
        private const string NurtureMe2OrganisationNumber = "46811";
        private const string LeestonPlaycentreOrganisationNumber = "70078";
        private const string TeKohangaReoOWaikareOrganisationNumber = "18350";
        private const string MixedLicenceClassDescription = "Mixed";
        private const string AllDayLicenceClassDescription = "All Day";
        private const string SessionalLicenceClassDescription = "Sessional";
        // Service Provision Types
        private const string TeacherLedServiceProvisionTypeDescription = "Teacher Led";

        private const string ParentLedServiceProvisionTypeDescription = "Parent Led";
        
        public EceService[] Data
        {
            get
            {
                return new[]
                {
                    new EceService
                    {
                        RefOrganisationId = 114895,
                        OrganisationName = "Montessori Little Hands",
                        OrganisationNumber = MontessoriOrganisationNumber,
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        EceServiceProviderNumber = "ECA3216",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OpenDate = DateTime.Now.AddYears(-20),
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,   
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325088, SessionDayDescription = "Tuesday", 
                                MaxChildren = 10,
                                MaxChildrenUnder2 = 2,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325089, SessionDayDescription = "Wednesday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325090, SessionDayDescription = "Thursday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Friday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            }
                        }
                    },
                    new EceService
                    {
                        RefOrganisationId = 9933,
                        OrganisationName = "Three Kings Community Kindergarten Inc",
                        OrganisationNumber = ThreeKingsOrganisationNumber,
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        EceServiceProviderNumber = "137743",
                        LicenceClassDescription = MixedLicenceClassDescription,
                        LicenceClassId = LicenceClass.Mixed,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325088, SessionDayDescription = "Tuesday",
                                MaxChildren = 10,
                                MaxChildrenUnder2 = 2,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325089, SessionDayDescription = "Wednesday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325090, SessionDayDescription = "Thursday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Friday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25001, SessionTypeDescription = "",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Saturday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = SessionType.DoesNotOperate, SessionTypeDescription = "does not operate",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led", 
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Sunday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = SessionType.DoesNotOperate, SessionTypeDescription = "does not operate",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led", //check this with Carmelle
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            }
                        }
                    },
                    new EceService
                    {
                        RefOrganisationId = 283,
                        OrganisationNumber = "10227",
                        OrganisationName = "Simply Kids Preschool",
                        EceServiceProviderNumber = "010227",
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true
                    },
                    new EceService
                    {
                        RefOrganisationId = 284,
                        OrganisationNumber = "16144",
                        OrganisationName = "Rawene Playcentre",
                        EceServiceProviderNumber = "ECA3333",
                        OrganisationTypeId = OrganisationType.Playcentre,
                        OrganisationTypeDescription = PlaycentreTypeDescription,
                        LicenceClassDescription = SessionalLicenceClassDescription,
                        LicenceClassId = LicenceClass.Sessional,
                        ServiceProvisionTypeId = ServiceProvisionType.ParentLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Probationary,
                        LicenceStatusDescription = "Probationary",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = true,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true
                    },
                    new EceService
                    {
                        RefOrganisationId = 5297,
                        OrganisationNumber = LeestonPlaycentreOrganisationNumber,
                        OrganisationName = "Leeston Playcentre",
                        EceServiceProviderNumber = "ECA3333",
                        OrganisationTypeId = OrganisationType.Playcentre,
                        OrganisationTypeDescription = PlaycentreTypeDescription,
                        LicenceClassDescription = SessionalLicenceClassDescription,
                        LicenceClassId = LicenceClass.Sessional,
                        ServiceProvisionTypeId = ServiceProvisionType.ParentLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        OpenDate = DateTime.Parse("1858-11-16 11:00:00.0000000 +00:00"),
                        EceServiceStatusDate = DateTime.Parse("1858-11-16 11:00:00.0000000 +00:00"),
                        LicenceStatusId = LicenceStatus.Probationary,
                        LicenceStatusDescription = "Probationary",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = true,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25001, SessionTypeDescription = "Teacher Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325087, SessionDayDescription = "Monday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25001, SessionTypeDescription = "Teacher Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325088, SessionDayDescription = "Tuesday",
                                MaxChildren = 10,
                                MaxChildrenUnder2 = 2,
                                SessionTypeId = SessionType.DoesNotOperate, SessionTypeDescription = "Parent Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325089, SessionDayDescription = "Wednesday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25001, SessionTypeDescription = "Teacher Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325090, SessionDayDescription = "Thursday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = SessionType.DoesNotOperate, SessionTypeDescription = "Parent Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Friday", MaxChildren = 35,
                                MaxChildrenUnder2 = 8,
                                SessionTypeId = 25001, SessionTypeDescription = "Teacher Led",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 9,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            }
                        }
                    },
                    new EceService
                    {
                        RefOrganisationId = 258,
                        OrganisationNumber = TeKohangaReoOWaikareOrganisationNumber,
                        OrganisationName = "Te Kōhanga Reo o Waikare",
                        EceServiceProviderNumber = "ECA445",
                        OrganisationTypeId = OrganisationType.TeKohangaReo,
                        OrganisationTypeDescription = TeKohangaReoTypeDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.ParentLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = true,
                        EcQualityLevelDescription = QualityLevel.QualityLevelTKR,
                        EcQualityLevelId = 21001,
                        EquityIndexId = 19001,
                        EquityIndexDescription = "EQ 1",
                        IsolationIndex = 1.93m,
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 194512, SessionDayDescription = "Monday", MaxChildren = 30,
                                MaxChildrenUnder2 = 6,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 6,
                                OperatingHours = 6,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 12, 0, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 194513, SessionDayDescription = "Tuesday", MaxChildren = 30,
                                MaxChildrenUnder2 = 6,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 6,
                                OperatingHours = 6,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325089, SessionDayDescription = "Wednesday", MaxChildren = 30,
                                MaxChildrenUnder2 = 6,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 6,
                                OperatingHours = 6,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325090, SessionDayDescription = "Thursday", MaxChildren = 30,
                                MaxChildrenUnder2 = 6,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 6,
                                OperatingHours = 6,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 325091, SessionDayDescription = "Friday", MaxChildren = 30,
                                MaxChildrenUnder2 = 6,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 6,
                                OperatingHours = 6,
                                StartTime = new DateTime(1899, 12, 30, 6, 30, 0).ToNzDateTimeOffSet(),
                                EndTime = new DateTime(1899, 12, 30, 15, 30, 0).ToNzDateTimeOffSet()
                            }
                        }
                    },
                    new EceService
                    { 
                        RefOrganisationId = 83115,
                        OrganisationNumber = "45547",
                        OrganisationName = "Te Kāinga Pōtiki - Choices Early Childhood Centre",
                        EceServiceProviderNumber = "ECA1932",
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 121497,
                        OrganisationNumber = "46859",
                        OrganisationName = "Te Kāinga Pōtiki ō Pāharakeke Early Childhood Centre",
                        EceServiceProviderNumber = "ECA1932",
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 10817,
                        OrganisationNumber = "10071",
                        OrganisationName = "Te Kōhanga Reo o Tahuri Mai",
                        EceServiceProviderNumber = "ECA445",
                        OrganisationTypeId = OrganisationType.TeKohangaReo,
                        OrganisationTypeDescription = TeKohangaReoTypeDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = true,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 10397,
                        OrganisationNumber = "64058",
                        OrganisationName = "Te Kōhanga Reo o Tararua ki Paraparaumu",
                        EceServiceProviderNumber = "ECA445",
                        OrganisationTypeId = OrganisationType.TeKohangaReo,
                        OrganisationTypeDescription = TeKohangaReoTypeDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.ParentLed,
                        ServiceProvisionTypeDescription = ParentLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 42483,
                        OrganisationNumber = "50099",
                        OrganisationName = "Te Kohanga Reo o Mana Tamariki",
                        EceServiceProviderNumber = "ECA445",
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 9018,
                        OrganisationNumber = "25150",
                        OrganisationName = "AUT Centre for Refugee Education Early Childhood Centre",
                        EceServiceProviderNumber = "025150",
                        OrganisationTypeId = OrganisationType.CasualEducationAndCare,
                        OrganisationTypeDescription = "Casual-Education and Care",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 72883,
                        OrganisationNumber = "45183",
                        OrganisationName = "Starship Play Service Ward 25AB",
                        EceServiceProviderNumber = "ECA1739",
                        OrganisationTypeId = OrganisationType.Hospitalbased,
                        OrganisationTypeDescription = "Hospitalbased",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 7764,
                        OrganisationNumber = "5331",
                        OrganisationName = "Maungaraki Kindergarten",
                        EceServiceProviderNumber = "ECA030",
                        OrganisationTypeId = OrganisationType.FreeKindergarten,
                        OrganisationTypeDescription = "Free Kindergarten",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 301,
                        OrganisationNumber = PorseMarlboroughOrganisationNumber,
                        OrganisationName = "PORSE Nelson/Marlborough/West Coast S1",
                        EceServiceProviderNumber = "ECA344",
                        OrganisationTypeId = OrganisationType.HomebasedNetwork,
                        OrganisationTypeDescription = "Homebased Network",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                        InstallmentPayments = true
                    },
                    new EceService
                    {
                        RefOrganisationId = 135077,
                        OrganisationNumber = "47091",
                        OrganisationName = "Aurora Tamariki Early Years Care and Kindergarten",
                        EceServiceProviderNumber = "ECA2936",
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = "Homebased Network",
                        LicenceClassDescription = AllDayLicenceClassDescription,
                        LicenceClassId = LicenceClass.AllDay,
                        ServiceProvisionTypeId = ServiceProvisionType.TeacherLed,
                        ServiceProvisionTypeDescription = TeacherLedServiceProvisionTypeDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        TeacherLedEligibleToOfferFree = true,
                        ParentLedEligibleToOfferFree = false,
                        EcQualityLevelDescription = QualityLevel.NotApplicable,
                        EcQualityLevelId = 21002,
                        IsFunded = true,
                    },
                    new EceService
                    {
                        RefOrganisationId = 10541,
                        OrganisationName = "Family Ties Educare - Eskvale",
                        OrganisationNumber = FamilyTiesOrganisationNumber,
                        OrganisationTypeId = OrganisationType.EducationAndCare,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        EceServiceProviderNumber = "ECA357",
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Cancelled,
                        LicenceStatusDescription = "Cancelled",
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 344335, SessionDayDescription = "Monday", MaxChildren = 25,
                                MaxChildrenUnder2 = 10,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 10
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 344336, SessionDayDescription = "Tuesday", MaxChildren = 25,
                                MaxChildrenUnder2 = 10,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 10
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 344337, SessionDayDescription = "Wednesday", MaxChildren = 25,
                                MaxChildrenUnder2 = 10,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 10
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 344338, SessionDayDescription = "Thursday", MaxChildren = 25,
                                MaxChildrenUnder2 = 10,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 10
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 344339, SessionDayDescription = "Friday", MaxChildren = 25,
                                MaxChildrenUnder2 = 10,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 10,
                            }
                        }
                    },
                    new EceService
                    {
                        RefOrganisationId = 120874,
                        OrganisationName = "Nurture Me 2",
                        OrganisationNumber = NurtureMe2OrganisationNumber,
                        OrganisationTypeId = OrganisationType.HomebasedNetwork,
                        OrganisationTypeDescription = EducationAndCareDescription,
                        EceServiceProviderNumber = "ECA2558",
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Suspended,
                        LicenceStatusDescription = "Suspended",
                        IsFunded = true,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 339999, SessionDayDescription = "Monday", MaxChildren = 80,
                                MaxChildrenUnder2 = 80,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 13
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 340000, SessionDayDescription = "Tuesday", MaxChildren = 80,
                                MaxChildrenUnder2 = 80,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 13
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 340001, SessionDayDescription = "Wednesday", MaxChildren = 80,
                                MaxChildrenUnder2 = 80,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 13
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 340002, SessionDayDescription = "Thursday", MaxChildren = 80,
                                MaxChildrenUnder2 = 80,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 13
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 340003, SessionDayDescription = "Friday", MaxChildren = 80,
                                MaxChildrenUnder2 = 80,
                                SessionTypeId = 25000, SessionTypeDescription = "All Day",
                                SessionProvisionTypeId = 24001, SessionProvisionTypeDescription = "Teacher Led",
                                FundedHours = 6,
                                OperatingHours = 13
                            }
                        }
                    },
                     new EceService
                    {
                        RefOrganisationId = 10007,
                        OrganisationName = "Foxton Playcentre",
                        OrganisationNumber = FoxtonPlaycentreOrganisationNumber,
                        OrganisationTypeId = OrganisationType.Playcentre,
                        OrganisationTypeDescription = PlaycentreTypeDescription,
                        EceServiceProviderNumber = "ECA3333",
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = "Open",
                        LicenceStatusId = LicenceStatus.Full,
                        LicenceStatusDescription = "Full",
                        IsFunded = false,
                        OperatingSessions = new List<EceOperatingSession>
                        {
                            new EceOperatingSession
                            {
                                SessionDayId = 26003, SessionDayDescription = "Thursday", MaxChildren = 25,
                                MaxChildrenUnder2 = 15,
                                SessionTypeId = 25001, SessionTypeDescription = "Sessional",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 3,
                                OperatingHours = 3
                            },
                            new EceOperatingSession
                            {
                                SessionDayId = 26004, SessionDayDescription = "Friday", MaxChildren = 25,
                                MaxChildrenUnder2 = 15,
                                SessionTypeId = 25001, SessionTypeDescription = "Sessional",
                                SessionProvisionTypeId = 136001, SessionProvisionTypeDescription = "Parent Led",
                                FundedHours = 2,
                                OperatingHours = 2
                            }
                        }
                    },
                };
            }
        }
        public EceService MontessoriLittleHands =>
            Data.Single(service => service.OrganisationNumber == MontessoriOrganisationNumber);

        public EceService TestClosedService =>
            Data.Single(service => service.OrganisationNumber == TestClosedServiceOrganisationNumber);

        public EceService FamilyTiesEducare =>
            Data.Single(service => service.OrganisationNumber == FamilyTiesOrganisationNumber);

        public EceService NurtureMe2 =>
            Data.Single(service => service.OrganisationNumber == NurtureMe2OrganisationNumber);

        public EceService LeestonPlaycentre =>
            Data.Single(service => service.OrganisationNumber == LeestonPlaycentreOrganisationNumber);

        public EceService TeKohangaReoOWaikare =>
            Data.Single(service => service.OrganisationNumber == TeKohangaReoOWaikareOrganisationNumber);
        
        /// <summary>
        /// This playcentre is not funded.
        /// </summary>
        public EceService FoxtonPlaycentre =>
            Data.Single(service => service.OrganisationNumber == FoxtonPlaycentreOrganisationNumber);

        /// <summary>
        /// This service receives monthly/installment payments.
        /// </summary>
        public EceService PorseMarlboroughHomebasedNetwork =>
            Data.Single(service => service.OrganisationNumber == PorseMarlboroughOrganisationNumber);
        
        public EceService GetByType(int organisationTypeId) =>
            Data.First(service => service.OrganisationTypeId == organisationTypeId);

        public EceService Get(int organisationTypeId, int licenceClassId) => Data.First(service =>
            service.OrganisationTypeId == organisationTypeId && service.LicenceClassId == licenceClassId);
    }
}