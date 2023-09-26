if (typeof module !== "undefined" && module.exports) {
    
}

function getChartColors() {
    return [
        "#5cbae5", "#b6d957", "#fac364", "#9ea8ad", "#1b7eac", "#759422", "#dd8e07", "#69767c", "#abdbf2",
        "#d7eaa2", "#fde5bd", "#d5dadc"
    ];
}

function setInnnerHtml(elementId, newvalue) {
    var element = document.getElementById(elementId);
    element.innerHTML = newvalue;
}

function setDivClassBasedOnVariance(elementId, variance, lessisgood) {
    const myDiv = document.getElementById(elementId);
    if (myDiv.classList.contains('bg-gradient-success')) {
        myDiv.classList.remove('bg-gradient-success');
    }

    if (myDiv.classList.contains('bg-gradient-info')) {
        myDiv.classList.remove('bg-gradient-info');
    }

    if (myDiv.classList.contains('bg-gradient-warning')) {
        myDiv.classList.remove('bg-gradient-warning');
    }

    if (myDiv.classList.contains('bg-gradient-danger')) {
        myDiv.classList.remove('bg-gradient-danger');
    }

    if (lessisgood) {
        if (variance > 0) {
            // Success
            myDiv.classList.add('bg-gradient-danger');
        }
        else if (variance >= -0.20 && lessisgood) {
            myDiv.classList.add('bg-gradient-warning');
        }
        else if (variance >= -0.50 && variance < -0.20) {
            // Warning
            myDiv.classList.add('bg-gradient-info');
        }
        else {
            // Error
            myDiv.classList.add('bg-gradient-success');
        }
    }
    else {
        if (variance > 0) {
            // Success
            myDiv.classList.add('bg-gradient-success');
        }
        else if (variance >= -0.20 && lessisgood) {
            myDiv.classList.add('bg-gradient-info');
        }
        else if (variance >= -0.50 && variance < -0.20) {
            // Warning
            myDiv.classList.add('bg-gradient-warning');
        }
        else {
            // Error
            myDiv.classList.add('bg-gradient-danger');
        }
    }
}

function translateSalesValueByHour(data) {
    var dataArray = [];
    if (data === null) {
        return [];
    }

    if (data.transactionHourViewModels === null) {
        return [];
    }

    // Add the labels first
    dataArray.push([
        { label: "Hour", type: "number" }, { label: "Todays Sales Value", type: "number" },
        { label: "Comparison Date Sales Value", type: "number" } // TODO: make label configurable
    ]);

    data.transactionHourViewModels.forEach((hour) => {
        var item = {
            c: [{ v: hour.hour }, { v: hour.todaysValue }, { v: hour.comparisonValue }]
        };
        dataArray.push(item);
    });
    return dataArray;
}

function translateSalesCountByHour(data) {
    var dataArray = [];

    if (data === null) {
        return [];
    }

    if (data.transactionHourViewModels === null) {
        return [];
    }

    // Add the labels first
    dataArray.push([
        { label: "Hour", type: "number" }, { label: "Todays Sales Count", type: "number" },
        { label: "Comparison Date Sales Count", type: "number" } // TODO: make label configurable
    ]);

    data.transactionHourViewModels.forEach((hour) => {
        var item = {
            c: [{ v: hour.hour }, { v: hour.todaysCount }, { v: hour.comparisonCount }]
        };
        dataArray.push(item);
    });
    return dataArray;
}

function drawLineChart(options, data, chartElement, emptyMessage) {
    if (data.getNumberOfRows() > 0) {
        var chart = new google.charts.Line(chartElement);
        chart.draw(data, google.charts.Line.convertOptions(options));
    }
    else {
        chartElement.empty();
        chartElement.append(emptyMessage);
    }
}

function drawBarChart(options, data, chartElement, emptyMessage) {
    if (data.getNumberOfRows() > 0) {
        var chart = new google.charts.Bar(chartElement);
        chart.draw(data, google.charts.Bar.convertOptions(options));
    }
    else {
        chartElement.empty();
        chartElement.append(emptyMessage);
    }
}

function setupChartOptions(chartTitle, hAxisTitle) {
    var options = {
        title: chartTitle,
        //curveType: 'function',
        legend: { position: 'bottom' },
        hAxis: {
            title: hAxisTitle,
            maxValue: 24
        },
        colors: getChartColors(),
        animation: {
            startup: true,
            duration: 1000,
            easing: 'out'
        },
    };

    return options;
}

function makeHttpPOSTWithHandler(url, resultHandler) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: 'json',
        async: false
    }).done(resultHandler)
}

function makeHttpPOST(url) {
    var responseJson = $.ajax({
        url: url,
        type: "POST",
        dataType: 'json',
        async: false
    }).responseText;
    return responseJson;
}