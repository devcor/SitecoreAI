using System;
using System.Collections.Generic;

namespace ContactFacets.POC.MongoDB
{
    public class TrainingProcessor
    {
        private const string LabelSeparator = "|";
        private const string ValueSeparator = ":";
        private const double MinLabelValue = 80;

        public static string GetTrainingValue(string aiResult)
        {
            if (aiResult == string.Empty)
                return aiResult;

            var labels = aiResult.Split(new[] { LabelSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var newLabels = new List<string>();

            foreach (var label in labels)
            {
                var value = label.Split(new[] { ValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (double.Parse(value[1].Replace("%", "")) >= MinLabelValue)
                    newLabels.Add(value[0]);
            }
            return string.Join(", ", newLabels);
        }
    }
}