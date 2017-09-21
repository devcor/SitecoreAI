using System;

namespace SitecoreAI.Interfaces.BusinessRules
{
    public interface IAIFacet
    {
        string GetAIResult(Guid contactId);
        string GetAIResult(Guid contactId, double minValue);
        string GetAITraining(Guid contactId);
        bool SetAIResult(Guid contactId, string value);
        bool SetAITraining(Guid contactId, string value);
    }
}
