function todaysSettlementResultHandler(results) {
    const formattedCurrency = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysSettlementValue);
    setInnerHtml(document.getElementById("todaysSettlementLabel"), formattedCurrency);
}

function comparisonDateTransactionsResultHandler(results) {
    // Today
    const formattedTodaysSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysValueOfTransactions);
    setInnerHtml(document.getElementById("todaysSalesLabel"), formattedTodaysSalesLabel);

    // Comparison Date
    setInnerHtml(document.getElementById("comparisonDateSalesLabelText"), results.label);
    const formattedComparisonDateSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonValueOfTransactions);
    setInnerHtml(document.getElementById("comparisonDateSalesLabel"), formattedComparisonDateSalesLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnerHtml(document.getElementById("salesVarianceLabelText"), formattedPercentage);
    setDivClassBasedOnVariance(document.getElementById("salesKpi"), results.variance)
}

function comparisonDateFailedTransactionsDueToLowCredit(results) {
    setInnerHtml(document.getElementById("comparisonDateFailedLowCreditSalesLabelText"), results.label);
    const formattedTodaysSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysCountOfTransactions);
    setInnerHtml(document.getElementById("todaysFailedLowCreditSalesLabel"), formattedTodaysSalesLabel);
    const formattedComparisonDateSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonCountOfTransactions);
    setInnerHtml(document.getElementById("comparisonDateFailedLowCreditSalesLabel"), formattedComparisonDateSalesLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnerHtml(document.getElementById("failedLowCreditSalesVarianceLabelText"), formattedPercentage);
    setDivClassBasedOnVariance(document.getElementById("failedSalesLowCreditKpi"), results.variance, true)
}

function merchantKpisResulthandler(results) {    
    setInnerHtml(document.getElementById("activeMerchantsLabel"), results.merchantsWithSaleInLastHour);
    setInnerHtml(document.getElementById("merchantsNoSalesInLastHourLabel"), results.merchantsWithNoSaleToday);
    setInnerHtml(document.getElementById("merchantsNoSalesInLast7DaysLabel"), results.merchantsWithNoSaleInLast7Days);
}

function comparisonDateSettlementResultHandler(results) {
    // Today
    const formattedTodaysSettlementLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysSettlementValue);
    setInnerHtml(document.getElementById("todaysSettlementLabel"), formattedTodaysSettlementLabel);

    // Comparison Date
    setInnerHtml(document.getElementById("comparisonDateSettlementLabelText"), results.label);
    const formattedComparisonDateSettlementLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonSettlementValue);
    setInnerHtml(document.getElementById("comparisonDateSettlementLabel"), formattedComparisonDateSettlementLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnerHtml(document.getElementById("settlementVarianceLabelText"), formattedPercentage);
    setDivClassBasedOnVariance(document.getElementById("settlementKpi"), results.variance, false)
}

function lastSettlementResultHandler(results) {
    const formattedSalesValueLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.salesValue);
    const formattedFeesValueLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.feesValue);
    const dateObject = new Date(results.settlementDate);
    const formattedDate = `${dateObject.getUTCDate().toString().padStart(2, '0')}-${(dateObject.getUTCMonth() + 1).toString().padStart(2, '0')}-${dateObject.getUTCFullYear()}`;
    console.log(formattedDate); // Output: "14-02-2024"

    setInnerHtml(document.getElementById("lastSettlementDateLabel"), formattedDate);
    setInnerHtml(document.getElementById("lastSettlementSalesValueLabel"), formattedSalesValueLabel);
    //setInnerHtml(document.getElementById("lastSettlementSalesCountLabel"), 'Sales Count: ' + results.salesCount);
    setInnerHtml(document.getElementById("lastSettlementFeesValueLabel"), formattedFeesValueLabel);
}