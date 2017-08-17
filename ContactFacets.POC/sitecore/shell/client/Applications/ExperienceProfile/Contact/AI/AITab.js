define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js", "/-/speak/v1/experienceprofile/CintelUtl.js"],
function (sc, providerHelper, cintelUtil) {
    var app = sc.Definitions.App.extend({
        initialized: function () {
            var app = this;
            var tableName = "ai"; 
            var localUrl = "/intel/" + tableName;            
             
            providerHelper.setupHeaders([
                { urlKey: localUrl + "?", headerValue: tableName }
            ]);

            var url = sc.Contact.baseUrl + localUrl;
            providerHelper.initProvider(app.TrainingDataProvider, tableName, url, app.AITabMessageBar);

            providerHelper.getData(app.TrainingDataProvider,
                $.proxy(function (jsonData) {
                    var dataSetProperty = "Data";
                    if (jsonData.data.dataSet != null && jsonData.data.dataSet.ai.length > 0) {
                        var aiData = jsonData.data.dataSet.ai[0]                        
                        app.TrainingDataProvider.set(dataSetProperty, jsonData);
                        cintelUtil.setText(app.TrainingData, aiData.AITraining, true);
                        cintelUtil.setText(app.NoDetailsData, aiData.AIResult, true);
                    }/* else {
                        app.EmployeeIdLabel.set("isVisible", false);
                        app.AITabMessageBar.addMessage("notification", app.NoEmployeeData.get("text"));
                    }*/
                }, this));
        },

        saveData: function () {
            var app = this;
            var aiEndPoint = "/api/sitecore/AI/SaveData";            
            
            var TrainingInfo = {
                Id: cintelUtil.getQueryParam("cid"),
                Labels: app.TrainingData.get('text')
            };

            jQuery.ajax({
                type: "POST",
                url: aiEndPoint,
                data: TrainingInfo,
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