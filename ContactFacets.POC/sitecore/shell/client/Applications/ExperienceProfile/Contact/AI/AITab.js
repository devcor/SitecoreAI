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

            providerHelper.getData(this.TrainingDataProvider,
                $.proxy(function (jsonData) {
                    var dataSetProperty = "Data";
                    if (jsonData.data.dataSet != null && jsonData.data.dataSet.ai.length > 0) {
                        var aiData = jsonData.data.dataSet.ai[0]
                        this.TrainingDataProvider.set(dataSetProperty, jsonData);
                        this.TrainingData.set("text", aiData.AITraining);
                        this.NoDetailsData.set("text", aiData.AIResult);
                        //this.AITabMessageBar.addMessage("notification", this.NoTrainingData.get("text"));
                    }/* else {
                        this.EmployeeIdLabel.set("isVisible", false);
                        this.AITabMessageBar.addMessage("notification", this.NoEmployeeData.get("text"));
                    }*/
                }, this));
        }
    });
    return app;
});