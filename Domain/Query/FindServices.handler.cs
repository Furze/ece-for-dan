using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MoE.ECE.Domain.Infrastructure.EntityFramework;
using MoE.ECE.Domain.Infrastructure.Extensions;
using MoE.ECE.Domain.Model.ReferenceData;
using MoE.ECE.Domain.Read.Model;
using MoE.ECE.Domain.Read.Model.Services;
using Moe.Library.Cqrs;

namespace MoE.ECE.Domain.Query
{
    public class FindServicesHandler : IQueryHandler<FindServices, CollectionModel<SearchEceServiceModel>>
    {
        private readonly ReferenceDataContext _referenceDataContext;
        private readonly IMapper _mapper;

        public FindServicesHandler(ReferenceDataContext referenceDataContext, IMapper mapper)
        {
            _referenceDataContext = referenceDataContext;
            _mapper = mapper;
        }

        public Task<CollectionModel<SearchEceServiceModel>> Handle(FindServices query, CancellationToken cancellationToken)
        {
            // //sanitise the given search term. trimming, lowercase, and taking the input back to pure ASCII (no macrons)
            // //NOTE: alternatively, would've preffered to use sanitisedSearchTerm = query.SearchTerm.Trim().ToLower().Normalize() 
            // // but to work, it would require the query to use StringComparison.Ordinal with either StartsWith(), Contains() or Equals().
            // // Unfortunately those Linq overloads are not supported by EntityFramework.
            // Encoding encoding = Encoding.GetEncoding(1252);
            // var searchTermBytes = encoding.GetBytes(query.SearchTerm.Trim().ToLower());
            // var sanitisedSearchTerm = Encoding.ASCII.GetString(searchTermBytes);

            var sanitisedSearchTerm = query.SearchTerm.Trim().ToLower();
                
            IQueryable<EceService> searchQuery = _referenceDataContext.EceServices;

            if (sanitisedSearchTerm != string.Empty)
                searchQuery = searchQuery
                    .Where(service =>
                        service.OrganisationName.StartsWith(sanitisedSearchTerm)
                        || service.OrganisationNumber.ToLower().StartsWith(sanitisedSearchTerm)
                        || service.EceServiceProviderNumber != null && service.EceServiceProviderNumber.ToLower().StartsWith(sanitisedSearchTerm)
                    );

            var serviceModelQuery = searchQuery
                .ProjectTo<SearchEceServiceModel>(_mapper.ConfigurationProvider)
                .OrderBy(service => service.ServiceName);

            var services = serviceModelQuery.Paginate(query, cancellationToken);

            return services;
        }
    }
}