if (typeof module !== "undefined" && module.exports)
{
    module.exports.translateDailyTotals = translateDailyTotals;
    module.exports.translateWeeklyTotals = translateWeeklyTotals;
    module.exports.translateMonthTotals = translateMonthTotals;
    module.exports.translateMerchantTotals = translateMerchantTotals;
    module.exports.translateOperatorTotals = translateOperatorTotals;
}

function getChartColors()
    {
        return[
            "#5cbae5", "#b6d957", "#fac364", "#9ea8ad", "#1b7eac", "#759422", "#dd8e07", "#69767c", "#abdbf2",
            "#d7eaa2", "#fde5bd", "#d5dadc"
        ];
    }

function translateDailyTotals(data)
    {
        var dataArray = [];

        if (data === null) {
            return [];
        }

        if (data.transactionDateViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Date", type: "date" }, { label: "Value of Sales", type: "number" },
            { label: "Number of Sales", type: "number" }
        ]);

        data.transactionDateViewModels.forEach((day) =>
        {
            // Parse the date
            var date = new Date(day.date);

            var dateStringValue = "Date(" + date.getFullYear() + "," + (date.getMonth() + 1)  + "," + date.getDate() + ")";

            var item = {
                c: [{ v: dateStringValue }, { v: day.valueOfTransactions / 10 }, { v: day.numberOfTransactions }]
            };
            dataArray.push(item);
        });

        return dataArray;
    }

function translateWeeklyTotals(data)
    {
        var dataArray = [];

        if (data === null) {
            return [];
        }

        if (data.transactionWeekViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Week", type: "string" }, { label: "Value of Sales", type: "number" },
            { label: "Number of Sales", type: "number" }
        ]);

        data.transactionWeekViewModels.forEach((week) =>
        {
            var dateStringValue = "Wk " + week.weekNumber + " " + week.year;

            var item = {
                c: [{ v: dateStringValue }, { v: week.valueOfTransactions / 10 }, { v: week.numberOfTransactions }]
            };
            dataArray.push(item);
        });

        return dataArray;
    }

function translateMonthTotals(data)
    {
        var dataArray = [];

        if (data === null) {
            return [];
        }

        if (data.transactionMonthViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Month", type: "string" }, { label: "Value of Sales", type: "number" },
            { label: "Number of Sales", type: "number" }
        ]);

        data.transactionMonthViewModels.forEach((month) =>
        {
            var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
            var date = new Date(month.year, month.monthNumber - 1, 1);
            var dateStringValue = months[date.getMonth()] + " " + month.year;

            var item = {
                c: [{ v: dateStringValue }, { v: month.valueOfTransactions / 10 }, { v: month.numberOfTransactions }]
            };
            dataArray.push(item);
        });

        return dataArray;
}

function translateMerchantTotals(data, sortField)
{
    var dataArray = [];
    
    if (data === null)
    {
        return [];
    }

    if (data.transactionMerchantViewModels === null)
    {
        return [];
    }
    // Add the labels first
    if (sortField === '0')
    {
        dataArray.push([
            { label: "Merchant", type: "string" }, { label: "Value of Sales", type: "number" }
        ]);
    }
    else
    {
        dataArray.push([
            { label: "Merchant", type: "string" }, { label: "Number of Sales", type: "number" }
        ]);
    }

    data.transactionMerchantViewModels.forEach((merchant) =>
    {
        var merchantName = merchant.merchantName;
        var value = 0;
        if (sortField === '0')
        {
            value = merchant.valueOfTransactions;
        }
        else
        {
            value = merchant.numberOfTransactions;
        }
        var item = {
            c: [{ v: merchantName }, { v: value }]
        };
        dataArray.push(item);
    });
    return dataArray;
}

function translateOperatorTotals(data, sortField) {
    var dataArray = [];

    if (data === null) {
        return [];
    }

    if (data.transactionOperatorViewModels === null) {
        return [];
    }
    // Add the labels first
    if (sortField === '0') {
        dataArray.push([
            { label: "Operator", type: "string" }, { label: "Value of Sales", type: "number" }
        ]);
    }
    else {
        dataArray.push([
            { label: "Operator", type: "string" }, { label: "Number of Sales", type: "number" }
        ]);
    }

    data.transactionOperatorViewModels.forEach((operator) => {
        var operatorName = operator.operatorName;
        var value = 0;
        if (sortField === '0') {
            value = operator.valueOfTransactions;
        }
        else {
            value = operator.numberOfTransactions;
        }
        var item = {
            c: [{ v: operatorName }, { v: value }]
        };
        dataArray.push(item);
    });
    return dataArray;
}
