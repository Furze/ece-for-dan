using System;
using System.Linq;
using System.Reflection;
using FluentValidation.Validators;

namespace MoE.ECE.Domain.Command.Validation
{
    /// <summary>
    /// Checks that an enum is actually defined. Its possible to pass in a rubbish string and it will still be parsed to an enum.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class ShouldBeAValidEnum<TEnum> : PropertyValidator
        where TEnum : struct, IConvertible, IComparable, IFormattable
    // Generic constraint to attempt to constrain to Enum - workaround because .NET framework does not allow this natively.
    {
        public ShouldBeAValidEnum() : base("{PropertyValue} is invalid. These are the valid options: " +
                                           string.Join(", ", Enum.GetValues(typeof(TEnum)).Cast<int>()))
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
                throw new ArgumentException("TEnum must be an enum.");
        }
        
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var propertyToValidate = (TEnum) context.PropertyValue;

            var isValidEnum = Enum.IsDefined(typeof(TEnum), propertyToValidate);

            return isValidEnum;
        }
    }
}