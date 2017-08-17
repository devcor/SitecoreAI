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
            var app = this;
            var aiEndPoint = "/api/sitecore/AI/SaveData";
            
            var trainingInfo = {
                id: "sdf",
                labels: this.TrainingData.get('text')
            };

            jQuery.ajax({
                type: "POST",
                url: aiEndPoint,
                data: { "aiTraining": JSON.stringify(trainingInfo) },
                success: function (success) {
                    app.AITabMessageBar.addMessage("notification", app.SavedText.get("text"))
                    app.AITabMessageBar.viewModel.$el.delay(2000).fadeOut();
                },
                error: function () {
                    app.AITabMessageBar.addMessage("notification", app.ErrorSavingText.get("text"))
                }
            });
        }
    });
    return app;
});