namespace SitecoreAI.Interfaces.BusinessRules
{
    public interface IContact
    {
        string GetAIResult(string contactId);
        string GetAITraining(string contactId);
        bool SetAIResult(string contactId, string value);
        bool SetAITraining(string contactId, string value);
        string GetLabelsGreaterThan(string currentLabels, double minValue);
    }
}
