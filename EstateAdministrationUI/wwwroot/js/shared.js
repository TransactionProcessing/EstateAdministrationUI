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

function setDivClassBasedOnVariance(elementId, variance) {
    const myDiv = document.getElementById(elementId);
    //console.log(variance);
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
    if (variance > 0) {
        // Success
        myDiv.classList.add('bg-gradient-success');
    }
    else if (variance >= -0.20) {
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
    //var d = JSON.stringify(dataArray);
    //console.log(d);
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