var chai = require("chai");
var testData = require("../testData.js");
var googleChartHelpers = require("../../../EstateAdministrationUI/wwwroot/js/googleChartHelpers.js");

describe("Merchant Factory Tests",
    function()
    {
        it("Merchant Dataset is converted - By Value",
            function()
            {
                var viewModel = testData.getDataByMerchantViewModel();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel, '0', valueLabel, countLabel);
                chai.expect(googleModel[0].length).to.equal(2);
                chai.expect(googleModel[0][0].label).to.equal("Merchant");
                chai.expect(googleModel[0][1].label).to.equal(valueLabel);

                chai.expect(googleModel[1].c[0].v).to.equal("Test Merchant 3");
                chai.expect(googleModel[1].c[1].v).to.equal(62184);

                chai.expect(googleModel[2].c[0].v).to.equal("Emulator Merchant");
                chai.expect(googleModel[2].c[1].v).to.equal(56943);

                chai.expect(googleModel[3].c[0].v).to.equal("Xperia Merchant");
                chai.expect(googleModel[3].c[1].v).to.equal(56553);

                chai.expect(googleModel[4].c[0].v).to.equal("Test Merchant 1");
                chai.expect(googleModel[4].c[1].v).to.equal(54715);

                chai.expect(googleModel[5].c[0].v).to.equal("S7 Merchant");
                chai.expect(googleModel[5].c[1].v).to.equal(53870);
            });

        it("Merchant Dataset is converted - By Count",
            function()
            {
                var viewModel = testData.getDataByMerchantViewModel();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel, '1', valueLabel, countLabel);
                chai.expect(googleModel[0].length).to.equal(2);
                chai.expect(googleModel[0][0].label).to.equal("Merchant");
                chai.expect(googleModel[0][1].label).to.equal(countLabel);

                chai.expect(googleModel[1].c[0].v).to.equal("Test Merchant 3");
                chai.expect(googleModel[1].c[1].v).to.equal(434);

                chai.expect(googleModel[2].c[0].v).to.equal("Emulator Merchant");
                chai.expect(googleModel[2].c[1].v).to.equal(404);

                chai.expect(googleModel[3].c[0].v).to.equal("Xperia Merchant");
                chai.expect(googleModel[3].c[1].v).to.equal(395);

                chai.expect(googleModel[4].c[0].v).to.equal("Test Merchant 1");
                chai.expect(googleModel[4].c[1].v).to.equal(384);

                chai.expect(googleModel[5].c[0].v).to.equal("S7 Merchant");
                chai.expect(googleModel[5].c[1].v).to.equal(372);
            });

        it("Merchant Dataset Null Merchant Models is converted - By Value",
            function()
            {
                var viewModel = testData.getDataByMerchantViewModelWithNullMerchantModels();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel,'0', valueLabel,countLabel);

                chai.expect(googleModel.length).to.equal(0);
            });

        it("Merchant Dataset Null Merchant Models is converted - By Count",
            function () {
                var viewModel = testData.getDataByMerchantViewModelWithNullMerchantModels();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel, '1',valueLabel,countLabel);

                chai.expect(googleModel.length).to.equal(0);
            });

        it("Merchant Dataset Empty Merchant Models is converted - By Value",
            function () {
                var viewModel = testData.getDataByMerchantViewModelWithEmptyMerchantModels();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel, '0',valueLabel,countLabel);

                chai.expect(googleModel.length).to.equal(1);
                chai.expect(googleModel[0].length).to.equal(2);
                chai.expect(googleModel[0][0].label).to.equal("Merchant");
                chai.expect(googleModel[0][1].label).to.equal(valueLabel);
            });

        it("Merchant Dataset Empty Merchant Models is converted - By Count",
            function()
            {
                var viewModel = testData.getDataByMerchantViewModelWithEmptyMerchantModels();
                var valueLabel = "Value of Sales";
                var countLabel = "Number Of Sales";
                var googleModel = googleChartHelpers.translateMerchantTotals(viewModel,'1',valueLabel,countLabel);

                chai.expect(googleModel.length).to.equal(1);
                chai.expect(googleModel[0].length).to.equal(2);
                chai.expect(googleModel[0][0].label).to.equal("Merchant");
                chai.expect(googleModel[0][1].label).to.equal(countLabel);
            });
    });

describe("Daily Factory Tests", function () {
    it("Daily Dataset is converted", function()
    {
        var viewModel = testData.getDataByDateViewModel();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateDailyTotals(viewModel, valueLabel, countLabel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Date");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);

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
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateDailyTotals(viewModel, valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Daily Dataset Null Date Models is converted", function () {
        var viewModel = testData.getDataByDateViewModelWithNullDateModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateDailyTotals(viewModel, valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Daily Dataset Empty Date Models is converted", function () {
        var viewModel = testData.getDataByDateViewModelWithEmptyDateModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateDailyTotals(viewModel, valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Date");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);
    });
});

describe("Weekly Factory Tests", function () {
    it("Weekly Dataset is converted", function () {
        var viewModel = testData.getDataByWeekViewModel();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel, valueLabel, countLabel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Week");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);

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
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel, valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Weekly Dataset Null Week Models is converted", function () {
        var viewModel = testData.getDataByWeekViewModelWithNullWeekModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel, valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Weekly Dataset Empty Week Models is converted", function () {
        var viewModel = testData.getDataByWeekViewModelWithEmptyWeekModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateWeeklyTotals(viewModel, valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Week");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);
    });
});

describe("Monthly Factory Tests", function () {
    it("Monthly Dataset is converted", function () {
        var viewModel = testData.getDataByMonthViewModel();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateMonthTotals(viewModel,valueLabel,countLabel);

        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Month");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);

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
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateMonthTotals(viewModel, valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Monthly Dataset Null Week Models is converted", function () {
        var viewModel = testData.getDataByMonthViewModelWithNullMonthModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateMonthTotals(viewModel, valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Monthly Dataset Empty Week Models is converted", function () {
        var viewModel = testData.getDataByMonthViewModelWithEmptyMonthModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateMonthTotals(viewModel,valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(3);
        chai.expect(googleModel[0][0].label).to.equal("Month");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
        chai.expect(googleModel[0][2].label).to.equal(countLabel);
    });
});

describe("Operator Factory Tests", function () {
    it("Operator Dataset is converted - By Value", function () {
        var viewModel = testData.getDataByOperatorViewModel();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel, '0', valueLabel, countLabel);
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);

        chai.expect(googleModel[1].c[0].v).to.equal("Test Operator 1");
        chai.expect(googleModel[1].c[1].v).to.equal(62184);

        chai.expect(googleModel[2].c[0].v).to.equal("Test Operator 2");
        chai.expect(googleModel[2].c[1].v).to.equal(56943);
    });

    it("Operator Dataset is converted - By Count", function () {
        var viewModel = testData.getDataByOperatorViewModel();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel, '1', valueLabel, countLabel);
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal(countLabel);

        chai.expect(googleModel[1].c[0].v).to.equal("Test Operator 1");
        chai.expect(googleModel[1].c[1].v).to.equal(434);

        chai.expect(googleModel[2].c[0].v).to.equal("Test Operator 2");
        chai.expect(googleModel[2].c[1].v).to.equal(404);
    });

    it("Operator Dataset Null Operator Models is converted - By Value", function () {
        var viewModel = testData.getDataByOperatorViewModelWithNullOperatorModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel,'0', valueLabel,countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Operator Dataset Null Operator Models is converted - By Count", function () {
        var viewModel = testData.getDataByOperatorViewModelWithNullOperatorModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel,'1', valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(0);
    });

    it("Operator Dataset Empty Operator Models is converted - By Value", function () {
        var viewModel = testData.getDataByOperatorViewModelWithEmptyOperatorModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel,'0', valueLabel, countLabel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal(valueLabel);
    });

    it("Operator Dataset Empty Operator Models is converted - By Count", function () {
        var viewModel = testData.getDataByOperatorViewModelWithEmptyOperatorModels();
        var valueLabel = "Value of Sales";
        var countLabel = "Number Of Sales";
        var googleModel = googleChartHelpers.translateOperatorTotals(viewModel,'1',valueLabel,countLabel);

        console.log(googleModel);

        chai.expect(googleModel.length).to.equal(1);
        chai.expect(googleModel[0].length).to.equal(2);
        chai.expect(googleModel[0][0].label).to.equal("Operator");
        chai.expect(googleModel[0][1].label).to.equal(countLabel);
    });
})