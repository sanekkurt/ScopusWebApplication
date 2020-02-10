// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var jsonWebData = JsonWebData.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JsonWebData
    {
        [JsonProperty("search-results")]
        public SearchResults SearchResults { get; set; }
    }

    public partial class SearchResults
    {
        [JsonProperty("opensearch:totalResults")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long OpensearchTotalResults { get; set; }

        [JsonProperty("opensearch:startIndex")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long OpensearchStartIndex { get; set; }

        [JsonProperty("opensearch:itemsPerPage")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long OpensearchItemsPerPage { get; set; }

        [JsonProperty("opensearch:Query")]
        public OpensearchQuery OpensearchQuery { get; set; }

        [JsonProperty("link")]
        public Dictionary<string, string>[] Link { get; set; }

        [JsonProperty("entry")]
        public Entry[] Entry { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("@_fa")]
        public Fa Fa { get; set; }

        [JsonProperty("prism:url")]
        public string PrismUrl { get; set; }

        [JsonProperty("dc:identifier")]
        public string DcIdentifier { get; set; }
    }

    public partial class OpensearchQuery
    {
        [JsonProperty("@role")]
        public string Role { get; set; }

        [JsonProperty("@searchTerms")]
        public string SearchTerms { get; set; }

        [JsonProperty("@startPage")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long StartPage { get; set; }
    }

    public enum Fa { True };

    public partial class JsonWebData
    {
        public static JsonWebData FromJson(string json) => JsonConvert.DeserializeObject<JsonWebData>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this JsonWebData self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                FaConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class FaConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Fa) || t == typeof(Fa?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "true")
            {
                return Fa.True;
            }
            throw new Exception("Cannot unmarshal type Fa");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Fa)untypedValue;
            if (value == Fa.True)
            {
                serializer.Serialize(writer, "true");
                return;
            }
            throw new Exception("Cannot marshal type Fa");
        }

        public static readonly FaConverter Singleton = new FaConverter();
    }

    internal class ParseIntegerConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseIntegerConverter Singleton = new ParseIntegerConverter();
    }
}
