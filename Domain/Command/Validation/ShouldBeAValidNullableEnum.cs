using System;
using FluentValidation.Validators;

namespace MoE.ECE.Domain.Command.Validation
{
    /// <summary>
    ///     Checks that an enum is actually defined. Its possible to pass in a rubbish string and it will still be parsed to an
    ///     enum.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class ShouldBeAValidNullableEnum<TEnum> : ShouldBeAValidEnum<TEnum>
        where TEnum : struct, IConvertible, IComparable, IFormattable
    // Generic constraint to attempt to constrain to Enum - workaround because .NET framework does not allow this natively.
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true;
            }

            TEnum propertyToValidate = (TEnum)context.PropertyValue;

            bool isValidEnum = Enum.IsDefined(typeof(TEnum), propertyToValidate);

            return isValidEnum;
        }
    }
}