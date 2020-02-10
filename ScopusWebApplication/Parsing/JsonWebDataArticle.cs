// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var jsonWebDataArticle = JsonWebDataArticle.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JsonWebDataArticle
    {
        [JsonProperty("abstracts-retrieval-response")]
        public AbstractsRetrievalResponse AbstractsRetrievalResponse { get; set; }
    }

    public partial class AbstractsRetrievalResponse
    {
        [JsonProperty("coredata")]
        public Dictionary<string, string> Coredata { get; set; }

        [JsonProperty("authors")]
        public Authors Authors { get; set; }
    }

    public partial class Authors
    {
        [JsonProperty("author")]
        public Author[] Author { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("ce:given-name")]
        public string CeGivenName { get; set; }

        [JsonProperty("preferred-name")]
        public PreferredName PreferredName { get; set; }

        [JsonProperty("@seq")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long Seq { get; set; }

        [JsonProperty("ce:initials")]
        public string CeInitials { get; set; }

        [JsonProperty("@_fa")]
        public string Fa { get; set; }

        /*[JsonProperty("affiliation")]
        public Dictionary<string, string> Affiliation { get; set; }*/

        [JsonProperty("ce:surname")]
        public string CeSurname { get; set; }

        [JsonProperty("@auid")]
        public string Auid { get; set; }

        [JsonProperty("author-url")]
        public string AuthorUrl { get; set; }

        [JsonProperty("ce:indexed-name")]
        public string CeIndexedName { get; set; }
    }

    public partial class PreferredName
    {
        [JsonProperty("ce:given-name")]
        public string CeGivenName { get; set; }

        [JsonProperty("ce:initials")]
        public string CeInitials { get; set; }

        [JsonProperty("ce:surname")]
        public string CeSurname { get; set; }

        [JsonProperty("ce:indexed-name")]
        public string CeIndexedName { get; set; }
    }

    public partial class JsonWebDataArticle
    {
        public static JsonWebDataArticle FromJson(string json) => JsonConvert.DeserializeObject<JsonWebDataArticle>(json, QuickType.Converter.Settings);
    }

    
        
    
}

