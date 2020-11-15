using System.Collections.Generic;
using System.Linq;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    /// <summary>
    ///     Static Helper class for providing ReferenceData into our database where we don't have ADF working such
    ///     as local developer databases & integration test databases.
    /// </summary>
    public class EceServiceReferenceData
    {
        private const string EducationAndCareDescription = "Education & Care Service";
        private const string MontessoriOrganisationNumber = "46543";
        private const string TestClosedServiceOrganisationNumber = "666";
        private const string TeKohangaReoTypeDescription = "Te Kohanga Reo";
        private const string PlaycentreTypeDescription = "Playcentre";
        private const string FamilyTiesOrganisationNumber = "83070";
        private const string NurtureMe2OrganisationNumber = "46811";

        public EceService[] Data =>
            new[]
            {
                new EceService
                {
                    RefOrganisationId = 114895,
                    OrganisationName = "Montessori Little Hands",
                    OrganisationNumber = MontessoriOrganisationNumber,
                    OrganisationTypeId = OrganisationType.EducationAndCare,
                    OrganisationTypeDescription = EducationAndCareDescription,
                    EceServiceProviderNumber = "ECA3216",
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full",
                    OperatingSessions = new List<EceOperatingSession>
                    {
                        new
                        {
                            SessionDayId = 325087,
                            SessionDayDescription = "Monday",
                            MaxChildren = 35,
                            MaxChildrenUnder2 = 8,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 9
                        },
                        new
                        {
                            SessionDayId = 325088,
                            SessionDayDescription = "Tuesday",
                            MaxChildren = 35,
                            MaxChildrenUnder2 = 8,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 9
                        },
                        new
                        {
                            SessionDayId = 325089,
                            SessionDayDescription = "Wednesday",
                            MaxChildren = 35,
                            MaxChildrenUnder2 = 8,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 9
                        },
                        new
                        {
                            SessionDayId = 325090,
                            SessionDayDescription = "Thursday",
                            MaxChildren = 35,
                            MaxChildrenUnder2 = 8,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 9
                        },
                        new
                        {
                            SessionDayId = 325091,
                            SessionDayDescription = "Friday",
                            MaxChildren = 35,
                            MaxChildrenUnder2 = 8,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 9
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
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 284,
                    OrganisationNumber = "16144",
                    OrganisationName = "Rawene Playcentre",
                    EceServiceProviderNumber = "ECA3333",
                    OrganisationTypeId = OrganisationType.Playcentre,
                    OrganisationTypeDescription = PlaycentreTypeDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Probationary,
                    LicenceStatusDescription = "Probationary"
                },
                new EceService
                {
                    RefOrganisationId = 285,
                    OrganisationNumber = "18350",
                    OrganisationName = "Te Kōhanga Reo o Waikare",
                    EceServiceProviderNumber = "ECA445",
                    OrganisationTypeId = OrganisationType.TeKohangaReo,
                    OrganisationTypeDescription = TeKohangaReoTypeDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 83115,
                    OrganisationNumber = "45547",
                    OrganisationName = "Te Kāinga Pōtiki - Choices Early Childhood Centre",
                    EceServiceProviderNumber = "ECA1932",
                    OrganisationTypeId = OrganisationType.EducationAndCare,
                    OrganisationTypeDescription = EducationAndCareDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 121497,
                    OrganisationNumber = "46859",
                    OrganisationName = "Te Kāinga Pōtiki ō Pāharakeke Early Childhood Centre",
                    EceServiceProviderNumber = "ECA1932",
                    OrganisationTypeId = OrganisationType.EducationAndCare,
                    OrganisationTypeDescription = EducationAndCareDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 10817,
                    OrganisationNumber = "10071",
                    OrganisationName = "Te Kōhanga Reo o Tahuri Mai",
                    EceServiceProviderNumber = "ECA445",
                    OrganisationTypeId = OrganisationType.TeKohangaReo,
                    OrganisationTypeDescription = TeKohangaReoTypeDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 42483,
                    OrganisationNumber = "50099",
                    OrganisationName = "Te Kohanga Reo o Mana Tamariki",
                    EceServiceProviderNumber = "ECA445",
                    OrganisationTypeId = OrganisationType.EducationAndCare,
                    OrganisationTypeDescription = EducationAndCareDescription,
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 9018,
                    OrganisationNumber = "25150",
                    OrganisationName = "AUT Centre for Refugee Education Early Childhood Centre",
                    EceServiceProviderNumber = "025150",
                    OrganisationTypeId = 10000,
                    OrganisationTypeDescription = "Casual-Education and Care",
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 72883,
                    OrganisationNumber = "45183",
                    OrganisationName = "Starship Play Service Ward 25AB",
                    EceServiceProviderNumber = "ECA1739",
                    OrganisationTypeId = 10008,
                    OrganisationTypeDescription = "Hospitalbased",
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 7764,
                    OrganisationNumber = "5331",
                    OrganisationName = "Maungaraki Kindergarten",
                    EceServiceProviderNumber = "ECA030",
                    OrganisationTypeId = 10001,
                    OrganisationTypeDescription = "Free Kindergarten",
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
                },
                new EceService
                {
                    RefOrganisationId = 301,
                    OrganisationNumber = "30143",
                    OrganisationName = "PORSE Nelson/Marlborough/West Coast S1",
                    EceServiceProviderNumber = "ECA344",
                    OrganisationTypeId = 10004,
                    OrganisationTypeDescription = "Homebased Network",
                    OrganisationStatusId = OrganisationStatus.Open,
                    OrganisationStatusDescription = "Open",
                    LicenceStatusId = LicenceStatus.Full,
                    LicenceStatusDescription = "Full"
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
                    OperatingSessions = new List<EceOperatingSession>
                    {
                        new
                        {
                            SessionDayId = 344335,
                            SessionDayDescription = "Monday",
                            MaxChildren = 25,
                            MaxChildrenUnder2 = 10,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 10
                        },
                        new
                        {
                            SessionDayId = 344336,
                            SessionDayDescription = "Tuesday",
                            MaxChildren = 25,
                            MaxChildrenUnder2 = 10,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 10
                        },
                        new
                        {
                            SessionDayId = 344337,
                            SessionDayDescription = "Wednesday",
                            MaxChildren = 25,
                            MaxChildrenUnder2 = 10,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 10
                        },
                        new
                        {
                            SessionDayId = 344338,
                            SessionDayDescription = "Thursday",
                            MaxChildren = 25,
                            MaxChildrenUnder2 = 10,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 10
                        },
                        new
                        {
                            SessionDayId = 344339,
                            SessionDayDescription = "Friday",
                            MaxChildren = 25,
                            MaxChildrenUnder2 = 10,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 10
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
                    OperatingSessions = new List<EceOperatingSession>
                    {
                        new
                        {
                            SessionDayId = 339999,
                            SessionDayDescription = "Monday",
                            MaxChildren = 80,
                            MaxChildrenUnder2 = 80,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 13
                        },
                        new
                        {
                            SessionDayId = 340000,
                            SessionDayDescription = "Tuesday",
                            MaxChildren = 80,
                            MaxChildrenUnder2 = 80,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 13
                        },
                        new
                        {
                            SessionDayId = 340001,
                            SessionDayDescription = "Wednesday",
                            MaxChildren = 80,
                            MaxChildrenUnder2 = 80,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 13
                        },
                        new
                        {
                            SessionDayId = 340002,
                            SessionDayDescription = "Thursday",
                            MaxChildren = 80,
                            MaxChildrenUnder2 = 80,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 13
                        },
                        new
                        {
                            SessionDayId = 340003,
                            SessionDayDescription = "Friday",
                            MaxChildren = 80,
                            MaxChildrenUnder2 = 80,
                            SessionTypeId = 25000,
                            SessionTypeDescription = "All Day",
                            SessionProvisionTypeId = 24001,
                            SessionProvisionTypeDescription = "Teacher Led",
                            FundedHours = 6,
                            OperatingHours = 13
                        }
                    }
                }
                // TODO(ERST-11367): Originally part of ERST-11035, but couldn't be implemented until Closed status records actually imported from First.
                //new EceService
                //{
                //    RefOrganisationId = 666,
                //    OrganisationNumber = TestClosedServiceOrganisationNumber,
                //    OrganisationName = "Test Closed Service,
                //    EceServiceProviderNumber = "ECA666",
                //    OrganisationTypeId = OrganisationType.EducationAndCare,
                //    OrganisationTypeDescription = EducationAndCareDescription,
                //    OrganisationStatusId = OrganisationStatus.Closed,
                //    OrganisationStatusDescription = "Closed",
                //    LicenceStatusId = LicenceStatus.Full,
                //    LicenceStatusDescription = "Full",
                //},
            };

        public EceService MontessoriLittleHands =>
            Data.Single(service => service.OrganisationNumber == MontessoriOrganisationNumber);

        public EceService TestClosedService =>
            Data.Single(service => service.OrganisationNumber == TestClosedServiceOrganisationNumber);

        public EceService FamilyTiesEducare =>
            Data.Single(service => service.OrganisationNumber == FamilyTiesOrganisationNumber);

        public EceService NurtureMe2 =>
            Data.Single(service => service.OrganisationNumber == NurtureMe2OrganisationNumber);

        public EceService GetByType(int organisationTypeId) =>
            Data.First(service => service.OrganisationTypeId == organisationTypeId);

        public EceService Get(int organisationTypeId, int licenceClassId) => Data.First(service =>
            service.OrganisationTypeId == organisationTypeId && service.LicenceClassId == licenceClassId);
    }
}