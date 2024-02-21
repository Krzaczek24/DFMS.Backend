using KrzaqTools.StringNotationExtension;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DFMS.Shared.Converters
{
    public enum NameAlterMode
    {
        None,
        ToCamel,
        ToFlat,
        ToKebab,
        ToLower,
        ToSnake,
        ToUpper,
        ToUpperFlat,
        ToUpperKebab,
        ToUpperSnake
    }

    public class EnumToStringAttribute : Attribute
    {
        public NameAlterMode Mode { get; }

        public EnumToStringAttribute() { }

        public EnumToStringAttribute(NameAlterMode mode)
        {
            Mode = mode;
        }
    }

    public class EnumToStringConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new EnumToStringConverter();
        }
    }

    public class EnumToStringConverter : JsonConverter<Enum>
    {
        private static readonly IReadOnlyDictionary<NameAlterMode, Func<string, string>> converters = new Dictionary<NameAlterMode, Func<string, string>>()
        {
            [NameAlterMode.None] = (name) => name,
            [NameAlterMode.ToCamel] = (name) => name.ToCamelCase(),
            [NameAlterMode.ToFlat] = (name) => name.ToFlatCase(),
            [NameAlterMode.ToKebab] = (name) => name.ToKebabCase(),
            [NameAlterMode.ToLower] = (name) => name.ToLower(),
            [NameAlterMode.ToSnake] = (name) => name.ToSnakeCase(),
            [NameAlterMode.ToUpper] = (name) => name.ToUpper(),
            [NameAlterMode.ToUpperFlat] = (name) => name.ToFlatCase().ToUpper(),
            [NameAlterMode.ToUpperKebab] = (name) => name.ToKebabCase().ToUpper(),
            [NameAlterMode.ToUpperSnake] = (name) => name.ToSnakeCase().ToUpper(),
        };

        public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Enum.TryParse(typeToConvert, reader.GetString(), true, out object? result) ? (Enum)result : default!;
        }

        public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
        {
            var convertMode = value.GetType().GetCustomAttribute<EnumToStringAttribute>()?.Mode ?? NameAlterMode.None;
            writer.WriteStringValue(converters[convertMode](value.ToString()));
        }
    }
}
