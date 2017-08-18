using Sitecore.Analytics.Model.Framework;

namespace ContactFacets.POC.Models
{
    public interface IAIFacet : IFacet
    {
        string Result { get; set; }
        string Training { get; set; }
    }
}
