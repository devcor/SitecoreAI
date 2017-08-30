using Sitecore.Analytics.Model.Framework;

namespace SitecoreAI.Models
{
    public interface IAIFacet : IFacet
    {
        string Result { get; set; }
        string Training { get; set; }
    }
}
