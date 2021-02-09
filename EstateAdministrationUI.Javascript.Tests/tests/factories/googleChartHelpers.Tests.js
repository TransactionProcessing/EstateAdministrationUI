var chai = require("chai");
var testData = require("../testData.js");
var googleChartHelpers = require("../../../EstateAdministrationUI/wwwroot/js/googleChartHelpers.js");

describe("Daily Factory Tests", function () {
    it("Daily Dataset is converted", function()
    {
        var viewModel = testData.getTransactionsByDateViewModel();

        var googleModel = googleChartHelpers.translateDailyTotals(viewModel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Date");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");

        chai.expect(googleModel[1].c[0].v).to.equal("Date(2020,10,1)");
        chai.expect(googleModel[1].c[1].v).to.equal(1000);
        chai.expect(googleModel[1].c[2].v).to.equal(10);
        
        chai.expect(googleModel[2].c[0].v).to.equal("Date(2020,10,2)");
        chai.expect(googleModel[2].c[1].v).to.equal(2000);
        chai.expect(googleModel[2].c[2].v).to.equal(20);

        chai.expect(googleModel[3].c[0].v).to.equal("Date(2020,10,3)");
        chai.expect(googleModel[3].c[1].v).to.equal(3000);
        chai.expect(googleModel[3].c[2].v).to.equal(30);
    });

    it("Null Daily Dataset is converted", function () {
        var viewModel = null;

        var googleModel = googleChartHelpers.translateDailyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Daily Dataset Null Date Models is converted", function () {
        var viewModel = testData.getTransactionsByDateViewModelWithNullDateModels();

        var googleModel = googleChartHelpers.translateDailyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Daily Dataset Empty Date Models is converted", function () {
        var viewModel = testData.getTransactionsByDateViewModelWithEmptyDateModels();

        var googleModel = googleChartHelpers.translateDailyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Date");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");
    });
});

describe("Weekly Factory Tests", function () {
    it("Weekly Dataset is converted", function () {
        var viewModel = testData.getTransactionsByWeekViewModel();

        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Week");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");

        chai.expect(googleModel[1].c[0].v).to.equal("Wk 1 2020");
        chai.expect(googleModel[1].c[1].v).to.equal(1000);
        chai.expect(googleModel[1].c[2].v).to.equal(10);

        chai.expect(googleModel[2].c[0].v).to.equal("Wk 2 2020");
        chai.expect(googleModel[2].c[1].v).to.equal(2000);
        chai.expect(googleModel[2].c[2].v).to.equal(20);

        chai.expect(googleModel[3].c[0].v).to.equal("Wk 3 2020");
        chai.expect(googleModel[3].c[1].v).to.equal(3000);
        chai.expect(googleModel[3].c[2].v).to.equal(30);
    });

    it("Null Weekly Dataset is converted", function () {
        var viewModel = null;

        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Weekly Dataset Null Week Models is converted", function () {
        var viewModel = testData.getTransactionsByWeekViewModelWithNullWeekModels();

        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Weekly Dataset Empty Week Models is converted", function () {
        var viewModel = testData.getTransactionsByWeekViewModelWithEmptyWeekModels();

        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Week");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");
    });
});

describe("Monthly Factory Tests", function () {
    it("Monthly Dataset is converted", function () {
        var viewModel = testData.getTransactionsByMonthViewModel();

        var googleModel = googleChartHelpers.translateMonthTotals(viewModel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Month");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");

        chai.expect(googleModel[1].c[0].v).to.equal("Jan 2020");
        chai.expect(googleModel[1].c[1].v).to.equal(1000);
        chai.expect(googleModel[1].c[2].v).to.equal(10);

        chai.expect(googleModel[2].c[0].v).to.equal("Feb 2020");
        chai.expect(googleModel[2].c[1].v).to.equal(2000);
        chai.expect(googleModel[2].c[2].v).to.equal(20);

        chai.expect(googleModel[3].c[0].v).to.equal("Mar 2020");
        chai.expect(googleModel[3].c[1].v).to.equal(3000);
        chai.expect(googleModel[3].c[2].v).to.equal(30);
    });

    it("Null Monthly Dataset is converted", function () {
        var viewModel = null;

        var googleModel = googleChartHelpers.translateMonthTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Monthly Dataset Null Week Models is converted", function () {
        var viewModel = testData.getTransactionsByMonthViewModelWithNullMonthModels();

        var googleModel = googleChartHelpers.translateMonthTotals(viewModel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Monthly Dataset Empty Week Models is converted", function () {
        var viewModel = testData.getTransactionsByMonthViewModelWithEmptyMonthModels();

        var googleModel = googleChartHelpers.translateMonthTotals(viewModel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Month");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");
        chai.expect(googleModel[0][2].label).to.equal("Number of Sales");
    });
});

describe("Operator Factory Tests", function () {
    it("Operator Dataset is converted - By Value", function () {
        var viewModel = testData.getTransactionsByOperatorViewModel();
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel, '0');
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal("Value of Sales");

        chai.expect(googleModel[1].c[0].v).to.equal("Test Operator 1");
        chai.expect(googleModel[1].c[1].v).to.equal(62184);

        chai.expect(googleModel[2].c[0].v).to.equal("Test Operator 2");
        chai.expect(googleModel[2].c[1].v).to.equal(56943);
    });

    it("Operator Dataset is converted - By Count", function () {
        var viewModel = testData.getTransactionsByOperatorViewModel();
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel, '1');
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal("Number of Sales");

        chai.expect(googleModel[1].c[0].v).to.equal("Test Operator 1");
        chai.expect(googleModel[1].c[1].v).to.equal(434);

        chai.expect(googleModel[2].c[0].v).to.equal("Test Operator 2");
        chai.expect(googleModel[2].c[1].v).to.equal(404);
    });
})