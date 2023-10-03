if (typeof module !== "undefined" && module.exports) {
    module.exports.setInnerHtml = setInnerHtml;
    module.exports.getChartColors = getChartColors;
    module.exports.setDivClassBasedOnVariance = setDivClassBasedOnVariance;
    module.exports.translateSalesValueByHour = translateSalesValueByHour;
    module.exports.translateSalesCountByHour = translateSalesCountByHour;
}

function getChartColors() {
    return [
        "#5cbae5", "#b6d957", "#fac364", "#9ea8ad", "#1b7eac", "#759422", "#dd8e07", "#69767c", "#abdbf2",
        "#d7eaa2", "#fde5bd", "#d5dadc"
    ];
}

function setInnerHtml(element, newvalue) {
    element.innerHTML = newvalue;
    return element;
}

function setDivClassBasedOnVariance(myDiv, variance, lessisgood) {   
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
        // 0 is info
        // < 0 = success
        // > 0 and less than 20 = warning
        // > 20 = danger
        switch (true) {
            case variance < 0:
                myDiv.classList.add('bg-gradient-success');
                break;
            case variance == 0:
                myDiv.classList.add('bg-gradient-info');
                break;            
            case variance > 0 && variance <= 0.20:
                myDiv.classList.add('bg-gradient-warning');
                break;
            case variance > 0.20:
                myDiv.classList.add('bg-gradient-danger');
                break;
        }        
    }
    else {
        // > 0 is success
        // 0 is info
        // > 0 and >= 20 - warning
        // < 20 - danger
        console.log(variance);
        switch (true) {
            case variance > 0:
                myDiv.classList.add('bg-gradient-success');
                break;
            case variance == 0:
                myDiv.classList.add('bg-gradient-info');
                break;
            case variance < 0 && variance >= -0.20:
                myDiv.classList.add('bg-gradient-warning');
                break;
            case variance < -0.20:
                console.log('here');
                myDiv.classList.add('bg-gradient-danger');
                break;
        }        
    }

    return myDiv;
}

function translateSalesValueByHour(data) {
    var dataArray = [];
    if (data === null) {
        console.log('here');
        return [];
    }

    if (data.transactionHourViewModels === null) {
        return [];
    }

    // Add the labels first
    dataArray.push([
        { label: "Hour", type: "number" },
        { label: "Todays Sales Value", type: "number" },
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