using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CharacterGenerator
{
    public class Spell
    {
        [JsonProperty("casting_time")]
        public string CastingTime { get; set; }

        [JsonProperty("components")]
        public string Components { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("school")]
        public School School { get; set; }

        public static Dictionary<string, Spell> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Spell>>(json, Converter.Settings);

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Level}-level {School}\r\nCasting Time: {CastingTime}\r\nRange: {Range}\r\n"
                + $"Components: {Components}\r\nDuration: {Duration}\r\n\r\n{Description}";
        }

        #endregion Overrides of Object
    }

    public enum School { Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, Transmutation };
    
    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, Spell> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SchoolConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SchoolConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(School) || t == typeof(School?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Abjuration":
                    return School.Abjuration;
                case "Conjuration":
                    return School.Conjuration;
                case "Divination":
                    return School.Divination;
                case "Enchantment":
                    return School.Enchantment;
                case "Evocation":
                    return School.Evocation;
                case "Illusion":
                    return School.Illusion;
                case "Necromancy":
                    return School.Necromancy;
                case "Transmutation":
                    return School.Transmutation;
            }
            throw new Exception("Cannot unmarshal type School");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (School)untypedValue;
            switch (value)
            {
                case School.Abjuration:
                    serializer.Serialize(writer, "Abjuration");
                    return;
                case School.Conjuration:
                    serializer.Serialize(writer, "Conjuration");
                    return;
                case School.Divination:
                    serializer.Serialize(writer, "Divination");
                    return;
                case School.Enchantment:
                    serializer.Serialize(writer, "Enchantment");
                    return;
                case School.Evocation:
                    serializer.Serialize(writer, "Evocation");
                    return;
                case School.Illusion:
                    serializer.Serialize(writer, "Illusion");
                    return;
                case School.Necromancy:
                    serializer.Serialize(writer, "Necromancy");
                    return;
                case School.Transmutation:
                    serializer.Serialize(writer, "Transmutation");
                    return;
            }
            throw new Exception("Cannot marshal type School");
        }

        public static readonly SchoolConverter Singleton = new SchoolConverter();
    }
}