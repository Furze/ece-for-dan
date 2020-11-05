using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using MoE.ECE.Domain.Infrastructure;

namespace MoE.ECE.Domain.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }

        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static ResourceNotFoundException Create<TEntity>(Expression<Func<TEntity, object?>> expression, object id)
        {
            var propertyName = PropertyExpressionHelper.GetPropertyName(expression);

            return new ResourceNotFoundException($"{typeof(TEntity).Name} with {propertyName} {id} does not exist.");
        }
    }
}