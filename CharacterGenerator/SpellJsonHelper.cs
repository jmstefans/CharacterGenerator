﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var spellJsonHelper = SpellJsonHelper.FromJson(jsonString);

namespace CharacterGenerator
{

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Globalization;

    public partial class SpellJsonHelper
    {
        [JsonProperty("SPELLS")]
        public Spells Spells { get; set; }
    }

    public class Spells
    {
        [JsonProperty("1st level (4 slots)")]
        public string The1StLevel4Slots { get; set; }

        [JsonProperty("2nd level (3 slots)")]
        public string The2NdLevel3Slots { get; set; }

        [JsonProperty("3rd level (3 slots)")]
        public string The3RdLevel3Slots { get; set; }

        [JsonProperty("4th level (3 slots)")]
        public string The4ThLevel3Slots { get; set; }

        [JsonProperty("5th level (1 slots)")]
        public string The5ThLevel1Slots { get; set; }

        [JsonProperty("Cantrips (at will)")]
        public string CantripsAtWill { get; set; }

        [JsonProperty("Caster")]
        public string Caster { get; set; }
    }

    public partial class SpellJsonHelper
    {
        public static SpellJsonHelper FromJson(string json) => JsonConvert.DeserializeObject<SpellJsonHelper>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SpellJsonHelper self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
