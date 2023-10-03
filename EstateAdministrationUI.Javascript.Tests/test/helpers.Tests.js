var chai = require("chai");
var helpers = require("../../EstateAdministrationUI/wwwroot/js/helpers.js");
describe('Helpers Tests', function () {
    describe("getAlertHtml Tests",
        () =>
        {
            const theories = [
                ["danger", "alert alert-danger"],
                ["warning", "alert alert-warning"],
                ["info", "alert alert-info"],
                ["success", "alert alert-success"]
            ];

            theories.forEach(([type, className]) =>
            {
                it(`Alert Html should have the correct type ${type} and values - message has pipe`,
                    () =>
                    {
                        var html = helpers.getAlertHtml(type, "Test Title", "Datatables|customerror");

                        chai.expect(html).to.include(className);
                        chai.expect(html).to.include('Test Title');
                        chai.expect(html).to.include('customerror');
                        chai.expect(html).to.not.include('Datatables');
                    });
            });

            theories.forEach(([type, className]) =>
                {
                    it(`Alert Html should have the correct type ${type} and values - message has no pipe`,
                        () =>
                        {
                            var html = helpers.getAlertHtml(type, "Test Title", "customerror");

                            chai.expect(html).to.include(className);
                            chai.expect(html).to.include('Test Title');
                            chai.expect(html).to.include('customerror');
                        });
                });
        });
});