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

function translateDailyTotals(data, valueLabel, countLabel)
    {
        var dataArray = [];

        if (data === null ) {
            return [];
        }

        if (data.dataDateViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Date", type: "date" }, { label: valueLabel, type: "number" },
            { label: countLabel, type: "number" }
        ]);

        data.dataDateViewModels.forEach((day) =>
        {
            // Parse the date
            var date = new Date(day.date);

            console.log(date);

            var dateStringValue = "Date(" + date.getFullYear() + "," + date.getMonth()  + "," + date.getDate() + ")";
            console.log(dateStringValue);
            var item = {
                c: [{ v: dateStringValue }, { v: day.value / 10 }, { v: day.count }]
            };
            dataArray.push(item);
        });

        return dataArray;
    }

function translateWeeklyTotals(data, valueLabel, countLabel)
    {
        var dataArray = [];

        if (data === null) {
            return [];
        }

        if (data.dataWeekViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Week", type: "string" }, { label: valueLabel, type: "number" },
            { label: countLabel, type: "number" }
        ]);

        data.dataWeekViewModels.forEach((week) =>
        {
            var dateStringValue = "Wk " + week.weekNumber + " " + week.year;

            var item = {
                c: [{ v: dateStringValue }, { v: week.value / 10 }, { v: week.count }]
            };
            dataArray.push(item);
        });

        return dataArray;
    }

function translateMonthTotals(data, valueLabel, countLabel)
    {
        var dataArray = [];

        if (data === null) {
            return [];
        }

        if (data.dataMonthViewModels === null) {
            return [];
        }

        // Add the labels first
        dataArray.push([
            { label: "Month", type: "string" }, { label: valueLabel, type: "number" },
            { label: countLabel, type: "number" }
        ]);

        data.dataMonthViewModels.forEach((month) =>
        {
            var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
            var date = new Date(month.year, month.monthNumber - 1, 1);
            var dateStringValue = months[date.getMonth()] + " " + month.year;

            var item = {
                c: [{ v: dateStringValue }, { v: month.value / 10 }, { v: month.count }]
            };
            dataArray.push(item);
        });

        return dataArray;
}

function translateMerchantTotals(data, sortField, valueLabel, countLabel)
{
    var dataArray = [];
    
    if (data === undefined ||data === null)
    {
        return [];
    }

    if (data.dataMerchantViewModels === null)
    {
        return [];
    }
    // Add the labels first
    if (sortField === '0')
    {
        dataArray.push([
            { label: "Merchant", type: "string" }, { label: valueLabel, type: "number" }
        ]);
    }
    else
    {
        dataArray.push([
            { label: "Merchant", type: "string" }, { label: countLabel, type: "number" }
        ]);
    }

    data.dataMerchantViewModels.forEach((merchant) =>
    {
        var merchantName = merchant.merchantName;
        var value = 0;
        if (sortField === '0')
        {
            value = merchant.value;
        }
        else
        {
            value = merchant.count;
        }
        var item = {
            c: [{ v: merchantName }, { v: value }]
        };
        dataArray.push(item);
    });
    return dataArray;
}

function translateOperatorTotals(data, sortField, valueLabel, countLabel) {
    var dataArray = [];

    if (data === undefined || data === null) {
        return [];
    }

    if (data.dataOperatorViewModels === null) {
        return [];
    }
    // Add the labels first
    if (sortField === '0') {
        dataArray.push([
            { label: "Operator", type: "string" }, { label: valueLabel, type: "number" }
        ]);
    }
    else {
        dataArray.push([
            { label: "Operator", type: "string" }, { label: countLabel, type: "number" }
        ]);
    }

    data.dataOperatorViewModels.forEach((operator) => {
        var operatorName = operator.operatorName;
        var value = 0;
        if (sortField === '0') {
            value = operator.value;
        }
        else {
            value = operator.count;
        }
        var item = {
            c: [{ v: operatorName }, { v: value }]
        };
        dataArray.push(item);
    });
    return dataArray;
}
