using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Text;

namespace CharacterGenerator
{
    public class Spell
    {
        [JsonProperty("casting_time")]
        public string CastingTime { get; set; }

        [JsonProperty("classes")]
        public Class[] Classes { get; set; }

        [JsonProperty("components")]
        public Components Components { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("level")]
        public Level Level { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("ritual")]
        public bool Ritual { get; set; }

        [JsonProperty("school")]
        public School School { get; set; }

        [JsonProperty("tags")]
        public Class[] Tags { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("higher_levels", NullValueHandling = NullValueHandling.Ignore)]
        public string HigherLevels { get; set; }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Type);

            return sb + $"Casting Time: {CastingTime}\r\nRange: {Range}\r\n"
                   + $"Components: {Components.Raw}\r\nDuration: {Duration}\r\n\r\n{Description}";
        }

        #endregion Overrides of Object

        public static Spell[] FromJson(string json) => JsonConvert.DeserializeObject<Spell[]>(json, Converter.Settings);
    }

    public class Components
    {
        [JsonProperty("material")]
        public bool Material { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("somatic")]
        public bool Somatic { get; set; }

        [JsonProperty("verbal")]
        public bool Verbal { get; set; }

        [JsonProperty("materials_needed", NullValueHandling = NullValueHandling.Ignore)]
        public string[] MaterialsNeeded { get; set; }
    }

    public enum Class { Bard, Cantrip, Cleric, ClericTrickery, Druid, Level1, Level2, Level3, Level4, Level5, Level6, Level7, Level8, Level9, Paladin, Ranger, Sorcerer, Warlock, Wizard };

    public enum School { Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, Transmutation };

    public struct Level
    {
        public Class? Enum;
        public long? Integer;

        public static implicit operator Level(Class Enum) => new Level { Enum = Enum };
        public static implicit operator Level(long Integer) => new Level { Integer = Integer };
    }

    public static class Serialize
    {
        public static string ToJson(this Spell[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ClassConverter.Singleton,
                LevelConverter.Singleton,
                SchoolConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ClassConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Class) || t == typeof(Class?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "bard":
                    return Class.Bard;
                case "cantrip":
                    return Class.Cantrip;
                case "cleric":
                    return Class.Cleric;
                case "cleric (trickery)":
                    return Class.ClericTrickery;
                case "druid":
                    return Class.Druid;
                case "level1":
                    return Class.Level1;
                case "level2":
                    return Class.Level2;
                case "level3":
                    return Class.Level3;
                case "level4":
                    return Class.Level4;
                case "level5":
                    return Class.Level5;
                case "level6":
                    return Class.Level6;
                case "level7":
                    return Class.Level7;
                case "level8":
                    return Class.Level8;
                case "level9":
                    return Class.Level9;
                case "paladin":
                    return Class.Paladin;
                case "ranger":
                    return Class.Ranger;
                case "sorcerer":
                    return Class.Sorcerer;
                case "warlock":
                    return Class.Warlock;
                case "wizard":
                    return Class.Wizard;
            }
            throw new Exception("Cannot unmarshal type Class");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Class)untypedValue;
            switch (value)
            {
                case Class.Bard:
                    serializer.Serialize(writer, "bard");
                    return;
                case Class.Cantrip:
                    serializer.Serialize(writer, "cantrip");
                    return;
                case Class.Cleric:
                    serializer.Serialize(writer, "cleric");
                    return;
                case Class.ClericTrickery:
                    serializer.Serialize(writer, "cleric (trickery)");
                    return;
                case Class.Druid:
                    serializer.Serialize(writer, "druid");
                    return;
                case Class.Level1:
                    serializer.Serialize(writer, "level1");
                    return;
                case Class.Level2:
                    serializer.Serialize(writer, "level2");
                    return;
                case Class.Level3:
                    serializer.Serialize(writer, "level3");
                    return;
                case Class.Level4:
                    serializer.Serialize(writer, "level4");
                    return;
                case Class.Level5:
                    serializer.Serialize(writer, "level5");
                    return;
                case Class.Level6:
                    serializer.Serialize(writer, "level6");
                    return;
                case Class.Level7:
                    serializer.Serialize(writer, "level7");
                    return;
                case Class.Level8:
                    serializer.Serialize(writer, "level8");
                    return;
                case Class.Level9:
                    serializer.Serialize(writer, "level9");
                    return;
                case Class.Paladin:
                    serializer.Serialize(writer, "paladin");
                    return;
                case Class.Ranger:
                    serializer.Serialize(writer, "ranger");
                    return;
                case Class.Sorcerer:
                    serializer.Serialize(writer, "sorcerer");
                    return;
                case Class.Warlock:
                    serializer.Serialize(writer, "warlock");
                    return;
                case Class.Wizard:
                    serializer.Serialize(writer, "wizard");
                    return;
            }
            throw new Exception("Cannot marshal type Class");
        }

        public static readonly ClassConverter Singleton = new ClassConverter();
    }

    internal class LevelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Level) || t == typeof(Level?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "bard":
                            return new Level { Enum = Class.Bard };
                        case "cantrip":
                            return new Level { Enum = Class.Cantrip };
                        case "cleric":
                            return new Level { Enum = Class.Cleric };
                        case "cleric (trickery)":
                            return new Level { Enum = Class.ClericTrickery };
                        case "druid":
                            return new Level { Enum = Class.Druid };
                        case "level1":
                            return new Level { Enum = Class.Level1 };
                        case "level2":
                            return new Level { Enum = Class.Level2 };
                        case "level3":
                            return new Level { Enum = Class.Level3 };
                        case "level4":
                            return new Level { Enum = Class.Level4 };
                        case "level5":
                            return new Level { Enum = Class.Level5 };
                        case "level6":
                            return new Level { Enum = Class.Level6 };
                        case "level7":
                            return new Level { Enum = Class.Level7 };
                        case "level8":
                            return new Level { Enum = Class.Level8 };
                        case "level9":
                            return new Level { Enum = Class.Level9 };
                        case "paladin":
                            return new Level { Enum = Class.Paladin };
                        case "ranger":
                            return new Level { Enum = Class.Ranger };
                        case "sorcerer":
                            return new Level { Enum = Class.Sorcerer };
                        case "warlock":
                            return new Level { Enum = Class.Warlock };
                        case "wizard":
                            return new Level { Enum = Class.Wizard };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new Level { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type Level");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Level)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case Class.Bard:
                        serializer.Serialize(writer, "bard");
                        return;
                    case Class.Cantrip:
                        serializer.Serialize(writer, "cantrip");
                        return;
                    case Class.Cleric:
                        serializer.Serialize(writer, "cleric");
                        return;
                    case Class.ClericTrickery:
                        serializer.Serialize(writer, "cleric (trickery)");
                        return;
                    case Class.Druid:
                        serializer.Serialize(writer, "druid");
                        return;
                    case Class.Level1:
                        serializer.Serialize(writer, "level1");
                        return;
                    case Class.Level2:
                        serializer.Serialize(writer, "level2");
                        return;
                    case Class.Level3:
                        serializer.Serialize(writer, "level3");
                        return;
                    case Class.Level4:
                        serializer.Serialize(writer, "level4");
                        return;
                    case Class.Level5:
                        serializer.Serialize(writer, "level5");
                        return;
                    case Class.Level6:
                        serializer.Serialize(writer, "level6");
                        return;
                    case Class.Level7:
                        serializer.Serialize(writer, "level7");
                        return;
                    case Class.Level8:
                        serializer.Serialize(writer, "level8");
                        return;
                    case Class.Level9:
                        serializer.Serialize(writer, "level9");
                        return;
                    case Class.Paladin:
                        serializer.Serialize(writer, "paladin");
                        return;
                    case Class.Ranger:
                        serializer.Serialize(writer, "ranger");
                        return;
                    case Class.Sorcerer:
                        serializer.Serialize(writer, "sorcerer");
                        return;
                    case Class.Warlock:
                        serializer.Serialize(writer, "warlock");
                        return;
                    case Class.Wizard:
                        serializer.Serialize(writer, "wizard");
                        return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type Level");
        }

        public static readonly LevelConverter Singleton = new LevelConverter();
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
                case "abjuration":
                    return School.Abjuration;
                case "conjuration":
                    return School.Conjuration;
                case "divination":
                    return School.Divination;
                case "enchantment":
                    return School.Enchantment;
                case "evocation":
                    return School.Evocation;
                case "illusion":
                    return School.Illusion;
                case "necromancy":
                    return School.Necromancy;
                case "transmutation":
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
                    serializer.Serialize(writer, "abjuration");
                    return;
                case School.Conjuration:
                    serializer.Serialize(writer, "conjuration");
                    return;
                case School.Divination:
                    serializer.Serialize(writer, "divination");
                    return;
                case School.Enchantment:
                    serializer.Serialize(writer, "enchantment");
                    return;
                case School.Evocation:
                    serializer.Serialize(writer, "evocation");
                    return;
                case School.Illusion:
                    serializer.Serialize(writer, "illusion");
                    return;
                case School.Necromancy:
                    serializer.Serialize(writer, "necromancy");
                    return;
                case School.Transmutation:
                    serializer.Serialize(writer, "transmutation");
                    return;
            }
            throw new Exception("Cannot marshal type School");
        }

        public static readonly SchoolConverter Singleton = new SchoolConverter();
    }
}