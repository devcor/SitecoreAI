define(["sitecore", "/-/speak/v1/experienceprofile/DataProviderHelper.js"], function (sc, providerHelper) {
    var app = sc.Definitions.App.extend({
        initialized: function () {
            var tableName = "sampleorders"; // Case Sensitive!
            var localUrl = "/intel/" + tableName;

            providerHelper.setupHeaders([
                { urlKey: localUrl + "?", headerValue: tableName }
            ]);

            var url = sc.Contact.baseUrl + localUrl;

            providerHelper.initProvider(this.SampleOrdersDataProvider, tableName, url, this.SampleOrdersTabMessageBar);
            providerHelper.subscribeSorting(this.SampleOrdersDataProvider, this.SampleOrders);
            providerHelper.getListData(this.SampleOrdersDataProvider);

            providerHelper.subscribeAccordionHeader(this.SampleOrdersDataProvider, this.SampleOrdersAccordion);

            sc.Contact.subscribeVisitDialog(this.SampleOrders);
        }
    });
    return app;
});