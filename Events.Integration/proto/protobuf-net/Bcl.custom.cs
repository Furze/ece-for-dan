// ReSharper disable once CheckNamespace
namespace ProtoBuf.Bcl
{
    public partial class Guid
    {
        public Guid(System.Guid initialValue)
        {
            Value = initialValue.ToString();
        }

        public static implicit operator System.Guid(Guid protoGuid)
        {
            return System.Guid.Parse(protoGuid.Value);
        }
    }

    /// <summary>
    /// Implementation provided as per Microsoft documentation
    /// https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types#decimals
    /// </summary>
    public partial class Decimal
    {
        private const decimal NanoFactor = 1_000_000_000;

        public Decimal(decimal value)
        {
            Units = decimal.ToInt64(value);
            Nanos = decimal.ToInt32((value - Units) * NanoFactor);
        }

        private Decimal(long units, int nanos)
        {
            Units = units;
            Nanos = nanos;
        }

        public static implicit operator decimal(Decimal grpcDecimal)
        {
            return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
        }

        public static implicit operator Decimal(decimal value)
        {
            return ToDecimal(value);
        }

        private static Decimal ToDecimal(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new Decimal(units, nanos);
        }
    }
    
    /// <summary>
    /// Add support for casting to nullable decimal
    /// </summary>
    public partial class Decimal
    {
        public static implicit operator decimal?(Decimal? grpcDecimal)
        {
            return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
        }
    }
}