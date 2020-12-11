using System.Linq;
using MoE.ECE.Domain.Model.ReferenceData;

namespace MoE.ECE.CLI.Data
{
    public class EceServiceProviderReferenceData
    {
        private const string LittleHandsLimitedOrganisationNumber = "ECA3216";
        private const string ThreeKingsCommunityKindergartenIncorporatedOrganisationNumber = "ECA2977";
        private const string IrinaLiebigOrganisationNumber = "010227";
        private const string TeWhanauTupuNgatahiOAotearoaPlaycentreAotearoaOrganisationNumber = "ECA3333";
        private const string KahungunuHealthServicesCharitableTrustOrganisationNumber = "ECA1932";
        private const string TeKohangaReoNationalTrustOrganisationNumber = "ECA445";
        private const string AutUniversityForCentreOfRefugeeEducationOrganisationNumber = "025150";
        private const string AucklandDistrictHealthBoardOrganisationNumber = "ECA1739";
        private const string HuttCityKindergartensAssociationIncorporatedOrganisationNumber = "ECA030";
        private const string PorseInHomeChildcareNzLtdOrganisationNumber = "ECA344";
        private const string SouthernStarsWaldorfTrustOrganisationNumber = "ECA2936";
        private const string FamilyTiesEducareOrganisationNumber = "ECA357";
        private const string NurtureMeEducationLimitedOrganisationNumber = "ECA2558";

        private const string KindergartenAssociationDescription = "Kindergarten Association";
        private const string PlaycentreAssociationDescription = "Playcentre Association";
        private const string CommercialEceServiceProviderDescription = "Commercial ECE Service Provider";
        private const string OtherECEServiceProviderDescription = "Other ECE Service Provider";
        private const string OpenDescription = "Open";

        public EceServiceProvider[] Data
        {
            get
            {
                return new[]
                {
                    new EceServiceProvider
                    {
                        RefOrganisationId = 147343,
                        OrganisationName = "Little Hands Limited",
                        OrganisationNumber = LittleHandsLimitedOrganisationNumber,
                        OrganisationTypeId = OrganisationType.CommercialEceServiceProvider,
                        OrganisationTypeDescription = CommercialEceServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 137743,
                        OrganisationName = "Three Kings Community Kindergarten Incorporated",
                        OrganisationNumber = ThreeKingsCommunityKindergartenIncorporatedOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 13942,
                        OrganisationName = "Irina Liebig",
                        OrganisationNumber = IrinaLiebigOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 150138,
                        OrganisationName = "Te Whānau Tupu Ngātahi o Aotearoa - Playcentre Aotearoa",
                        OrganisationNumber = TeWhanauTupuNgatahiOAotearoaPlaycentreAotearoaOrganisationNumber,
                        OrganisationTypeId = OrganisationType.PlaycentreAssociation,
                        OrganisationTypeDescription = PlaycentreAssociationDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 83113,
                        OrganisationName = "Kahungunu Health Services Charitable Trust",
                        OrganisationNumber = KahungunuHealthServicesCharitableTrustOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 14471,
                        OrganisationName = "Te Kōhanga Reo National Trust",
                        OrganisationNumber = TeKohangaReoNationalTrustOrganisationNumber,
                        OrganisationTypeId = OrganisationType.CommercialEceServiceProvider,
                        OrganisationTypeDescription = CommercialEceServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 13599,
                        OrganisationName = "AUT University for Centre of Refugee Education",
                        OrganisationNumber = AutUniversityForCentreOfRefugeeEducationOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 72884,
                        OrganisationName = "Auckland District Health Board",
                        OrganisationNumber = AucklandDistrictHealthBoardOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 14628,
                        OrganisationName = "Hutt City Kindergartens Association Incorporated",
                        OrganisationNumber = HuttCityKindergartensAssociationIncorporatedOrganisationNumber,
                        OrganisationTypeId = OrganisationType.KindergartenAssociation,
                        OrganisationTypeDescription = KindergartenAssociationDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 14361,
                        OrganisationName = "PORSE In-Home Childcare (NZ) Ltd",
                        OrganisationNumber = PorseInHomeChildcareNzLtdOrganisationNumber,
                        OrganisationTypeId = OrganisationType.CommercialEceServiceProvider,
                        OrganisationTypeDescription = CommercialEceServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 135080,
                        OrganisationName = "Southern Stars Waldorf Trust",
                        OrganisationNumber = SouthernStarsWaldorfTrustOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 14400,
                        OrganisationName = "Family Ties Educare",
                        OrganisationNumber = FamilyTiesEducareOrganisationNumber,
                        OrganisationTypeId = OrganisationType.OtherEceServiceProvider,
                        OrganisationTypeDescription = OtherECEServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    },
                    new EceServiceProvider
                    {
                        RefOrganisationId = 114459,
                        OrganisationName = "Nurture Me Education Limited",
                        OrganisationNumber = NurtureMeEducationLimitedOrganisationNumber,
                        OrganisationTypeId = OrganisationType.CommercialEceServiceProvider,
                        OrganisationTypeDescription = CommercialEceServiceProviderDescription,
                        OrganisationStatusId = OrganisationStatus.Open,
                        OrganisationStatusDescription = OpenDescription
                    }
                };
            }
        }

        public EceServiceProvider LittleHandsLimited =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == LittleHandsLimitedOrganisationNumber);

        public EceServiceProvider ThreeKingsCommunityKindergartenIncorporated =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == ThreeKingsCommunityKindergartenIncorporatedOrganisationNumber);

        public EceServiceProvider IrinaLiebig =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == IrinaLiebigOrganisationNumber);

        public EceServiceProvider TeWhanauTupuNgatahiOAotearoaPlaycentreAotearoa =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == TeWhanauTupuNgatahiOAotearoaPlaycentreAotearoaOrganisationNumber);

        public EceServiceProvider KahungunuHealthServicesCharitableTrust =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == KahungunuHealthServicesCharitableTrustOrganisationNumber);

        public EceServiceProvider TeKohangaReoNationalTrust =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == TeKohangaReoNationalTrustOrganisationNumber);

        public EceServiceProvider AutUniversityForCentreOfRefugeeEducation =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == AutUniversityForCentreOfRefugeeEducationOrganisationNumber);

        public EceServiceProvider AucklandDistrictHealthBoard =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == AucklandDistrictHealthBoardOrganisationNumber);

        public EceServiceProvider HuttCityKindergartensAssociationIncorporated =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == HuttCityKindergartensAssociationIncorporatedOrganisationNumber);

        public EceServiceProvider PorseInHomeChildcareNzLtd =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == PorseInHomeChildcareNzLtdOrganisationNumber);

        public EceServiceProvider SouthernStarsWaldorfTrust =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == SouthernStarsWaldorfTrustOrganisationNumber);

        public EceServiceProvider FamilyTiesEducare =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == FamilyTiesEducareOrganisationNumber);

        public EceServiceProvider NurtureMeEducationLimited =>
            Data.Single(serviceProvider => serviceProvider.OrganisationNumber == NurtureMeEducationLimitedOrganisationNumber);

    }
}