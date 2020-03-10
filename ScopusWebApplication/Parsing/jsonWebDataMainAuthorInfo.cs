// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var jsonWebDataMainAuthorInfo = JsonWebDataMainAuthorInfo.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JsonWebDataMainAuthorInfo
    {
        [JsonProperty("search-results")]
        public SearchResults SearchResults { get; set; }
    }

    

    public partial class Entry
    {
        
        [JsonProperty("eid")]
        public string Eid { get; set; }

        [JsonProperty("orcid")]
        public string Orcid { get; set; }

        [JsonProperty("preferred-name")]
        public PreferredName PreferredName { get; set; }

        [JsonProperty("document-count")]
        [JsonConverter(typeof(ParseIntegerConverter))]
        public long DocumentCount { get; set; }

        [JsonProperty("affiliation-current")]
        public AffiliationCurrent AffiliationCurrent { get; set; }
    }

    public partial class AffiliationCurrent
    {
        [JsonProperty("affiliation-name")]
        public string AffiliationName { get; set; }

        [JsonProperty("affiliation-city")]
        public string AffiliationCity { get; set; }

        [JsonProperty("affiliation-country")]
        public string AffiliationCountry { get; set; }
    }

    public partial class PreferredName
    {
        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("given-name")]
        public string GivenName { get; set; }
    }

   

    public partial class JsonWebDataMainAuthorInfo
    {
        public static JsonWebDataMainAuthorInfo FromJson(string json) => JsonConvert.DeserializeObject<JsonWebDataMainAuthorInfo>(json, QuickType.Converter.Settings);
    }

    

       
    
}
