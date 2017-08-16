define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js"], function (sc, providerHelper) {
    var app = sc.Definitions.App.extend({
        initialized: function () {
            $('.sc-progressindicator').first().show().hide();
            var tableName = "ai"; // Case Sensitive!
            var localUrl = "/intel/" + tableName;

            providerHelper.setupHeaders([
                { urlKey: localUrl + "?", headerValue: tableName }
            ]);

            var url = sc.Contact.baseUrl + localUrl;

            providerHelper.initProvider(this.TrainingDataProvider, tableName, url, this.AITabMessageBar);
            //providerHelper.subscribeAccordionHeader(this.TrainingDataProvider, this.TrainingAccordion);
            /**/

            providerHelper.getData(this.TrainingDataProvider,
                $.proxy(function (jsonData) {
                    var dataSetProperty = "Data";
                    if (jsonData.data.dataSet != null && jsonData.data.dataSet.ai.length > 0) {
                        var aiData = jsonData.data.dataSet.ai[0]
                        this.TrainingDataProvider.set(dataSetProperty, jsonData);
                        this.TrainingData.set("text", aiData.AITraining);
                        this.NoDetailsData.set("text", aiData.AIResult);
                    }/* else {
                        this.EmployeeIdLabel.set("isVisible", false);
                        this.AITabMessageBar.addMessage("notification", this.NoEmployeeData.get("text"));
                    }*/
                }, this));
        },

        saveData: function () {
            var aiEndPoint = "/api/sitecore/AI/SaveData";

            var trainingInfo = {
                labels: this.TrainingData.get('text')
            };

            jQuery.ajax({
                type: "POST",
                url: aiEndPoint,
                data: { "AITraining": JSON.stringify(trainingInfo) },
                success: function (success) {
                    this.AITabMessageBar.addMessage("notification", this.SavedText.get("text"))
                },
                error: function () {
                    this.AITabMessageBar.addMessage("notification", this.ErrorSavingText.get("text"))
                }
            });
        }
    });
    return app;
});