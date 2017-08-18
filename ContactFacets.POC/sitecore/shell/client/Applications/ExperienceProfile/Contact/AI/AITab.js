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
                    if (jsonData.data.dataSet != null && jsonData.data.dataSet.ai.length > 0) {
                        var aiData = jsonData.data.dataSet.ai[0]                        
                        app.TrainingDataProvider.set("Data", jsonData);
                        if (aiData.Training != null) {
                            cintelUtil.setText(app.TrainingData, aiData.Training, true);
                        }
                        if (aiData.Result != null) {
                            cintelUtil.setText(app.DetailsData, aiData.Result, true);
                        }                       
                    }
                }, this));
        },

        saveData: function () {
            var app = this;
            var labels = app.TrainingData.get("text");

            app.AITabMessageBar.removeMessages();
            var message = {
                text: app.SavedText.get("text"),
                actions: [],
                temporary: false,
                closable: true
            };

            if (labels != "") {
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
                        message.text = app.SavedText.get("text");
                        app.AITabMessageBar.addMessage("notification", message);                        
                    },
                    error: function () {
                        message.text = app.ErrorSavingText.get("text");
                        app.AITabMessageBar.addMessage("error", message)
                    }
                });
            } else {
                message.text = app.ValidationText.get("text");
                app.AITabMessageBar.addMessage("warning", message);
            }
        }
    });
    return app;
});