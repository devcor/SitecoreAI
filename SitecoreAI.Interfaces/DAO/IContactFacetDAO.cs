using System;

namespace SitecoreAI.Interfaces.DAO
{
    public interface IContactFacetDAO
    {
        string GetAIResult(Guid contactId);
        string GetAITraining(Guid contactId);
        bool SetAIResult(Guid contactId, string value);
        bool SetAITraining(Guid contactId, string value);
    }
}
