using Sitecore.Analytics.Model.Framework;

namespace ContactFacets.POC.Models
{
    public interface IAIContactPersonalInfo : IFacet
    {
        string AIResult { get; set; }
        string AITraining { get; set; }
    }
}
