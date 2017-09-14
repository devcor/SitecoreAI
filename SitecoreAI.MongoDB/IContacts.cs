namespace SitecoreAI.MongoDB
{
    public interface IContacts
    {
        string GetAIResult(string contactId);
        string GetAITraining(string contactId);
        bool SetAIResult(string contactId, string value);
        bool SetAITraining(string contactId, string value);
    }
}
