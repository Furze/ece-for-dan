using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using MoE.ECE.Domain.Model;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
   public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> ResolveUsing<TSource, TDestination, TDestinationMember>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            Expression<Func<TDestination, TDestinationMember>> destinationMember,
            IValueResolver<TSource, TDestination, TDestinationMember> valueResolver)
        {
            return mappingExpression.ForMember(destinationMember, options => options.MapFrom(valueResolver));
        }

        public static TDestination[] MapToArray<TDestination>(this IMapper mapper,
            object source)
        {
            return mapper.Map<TDestination[]>(source);
        }

        public static List<TDestination> MapToList<TDestination>(this IMapper mapper,
            object source)
        {
            return mapper.Map<List<TDestination>>(source);
        }

        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IMapper mapper,
            IEnumerable<TSource> sourceCollection, IEnumerable<TDestination> destinationCollection,
            Func<TDestination, TSource, bool> equalsPredicate)
        {
            return mapper.MapToList(sourceCollection, destinationCollection,
                (collection, sourceItem) =>
                    collection.FirstOrDefault(destItem => equalsPredicate(destItem, sourceItem)));
        }

        public static IEnumerable<TDestination> MapToList<TSource, TDestination>(this IMapper mapper,
            IEnumerable<TSource> sourceCollection, IEnumerable<TDestination> destinationCollection,
            Func<IEnumerable<TDestination>, TSource, TDestination> destinationResolver)
        {
            var updatedDestinationCollection = new List<TDestination>();
            var destList = destinationCollection.ToList();

            foreach (var source in sourceCollection)
            {
                var existingDestination = destinationResolver(destList, source);

                var updatedPosition = existingDestination != null
                    ? mapper.Map(source, existingDestination)
                    : mapper.Map<TDestination>(source);

                updatedDestinationCollection.Add(updatedPosition);
            }

            return updatedDestinationCollection;
        }

        public static IMappingExpression<TSource, TDestination> Map<TSource, TSourceMember, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            Expression<Func<TDestination, object?>> destinationMember,
            Expression<Func<TSource, TSourceMember>> sourceMember)
        {
            return mappingExpression.ForMember(destinationMember, options => options.MapFrom(sourceMember));
        }
       
        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            Expression<Func<TDestination, object?>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, options => options.Ignore());
        }
        
        public static IMappingExpression<TSource, TDomainEntity> IgnoreDomainEntityProperties<TSource, TDomainEntity>(this IMappingExpression<TSource, TDomainEntity> mappingExpression
        ) where TDomainEntity : DomainEntity
        {
            mappingExpression.ForMember(entity => entity.Id, options => options.Ignore());
            mappingExpression.ForMember(entity => entity.RowVersion, options => options.Ignore());

            return mappingExpression;
        }
    }
}