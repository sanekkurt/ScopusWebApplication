using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopusWebApplication.Models
{
    public class MainAuthorInfo
    {
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public string PrismUrl { get; set; }
        public string Id { get; set; }
        public string Eid { get; set; }
        public string Orcid { get; set; }
        public string DocumentCount { get; set; }
        public string AffiliationName { get; set; }
        public string AffiliationCity { get; set; }
        public string AffiliationCountry { get; set; }
        public MainAuthorInfo()
        {
            Surname = "-";
            GivenName = "-";
            PrismUrl = "-";
            Id = "-";
            Eid = "-";
            Orcid = "-";
            DocumentCount = "-";
            AffiliationName = "-";
            AffiliationCity = "-";
            AffiliationCountry = "-";
        }
    }
}
