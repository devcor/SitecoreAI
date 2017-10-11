﻿using System;

namespace SitecoreAI.Interfaces.BusinessRules
{
    public interface IContactFacet
    {
        string GetAIResult(Guid contactId);
        string GetAITraining(Guid contactId);
        bool SetAIResult(Guid contactId, string value);
        bool SetAITraining(Guid contactId, string value);
    }
}
