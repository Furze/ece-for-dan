using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoE.ECE.Domain.Infrastructure
{
    /// <summary>
    ///     Apply this converter to a property to force the property to be serialized with default options.
    /// </summary>
    /// <typeparam name="T">the property's declared return type</typeparam>
    public class JsonNoEnumConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            JsonSerializer.Deserialize<T>(ref reader);

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
            JsonSerializer.Serialize(writer, value);
    }
}