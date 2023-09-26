function todaysSettlementResultHandler(results) {
    const formattedCurrency = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysSettlementValue);
    setInnnerHtml("todaysSettlementLabel", formattedCurrency);
}

function comparisonDateTransactionsResultHandler(results) {
    // Today
    const formattedTodaysSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysValueOfTransactions);
    setInnnerHtml("todaysSalesLabel", formattedTodaysSalesLabel);

    // Comparison Date
    setInnnerHtml("comparisonDateSalesLabelText", results.label);
    const formattedComparisonDateSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonValueOfTransactions);
    setInnnerHtml("comparisonDateSalesLabel", formattedComparisonDateSalesLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnnerHtml("salesVarianceLabelText", formattedPercentage);
    setDivClassBasedOnVariance("salesKpi", results.variance)
}

function comparisonDateFailedTransactionsDueToLowCredit(results) {
    setInnnerHtml("comparisonDateFailedLowCreditSalesLabelText", results.label);
    const formattedTodaysSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysCountOfTransactions);
    setInnnerHtml("todaysFailedLowCreditSalesLabel", formattedTodaysSalesLabel);
    const formattedComparisonDateSalesLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonCountOfTransactions);
    setInnnerHtml("comparisonDateFailedLowCreditSalesLabel", formattedComparisonDateSalesLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnnerHtml("failedLowCreditSalesVarianceLabelText", formattedPercentage);
    setDivClassBasedOnVariance("failedSalesLowCreditKpi", results.variance, true)
}

function merchantKpisResulthandler(results) {    
    setInnnerHtml("activeMerchantsLabel", results.merchantsWithSaleInLastHour);
    setInnnerHtml("merchantsNoSalesInLastHourLabel", results.merchantsWithNoSaleToday);
    setInnnerHtml("merchantsNoSalesInLast7DaysLabel", results.merchantsWithNoSaleInLast7Days);
}

function comparisonDateSettlementResultHandler(results) {
    // Today
    const formattedTodaysSettlementLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.todaysSettlementValue);
    setInnnerHtml("todaysSettlementLabel", formattedTodaysSettlementLabel);

    // Comparison Date
    setInnnerHtml("comparisonDateSettlementLabelText", results.label);
    const formattedComparisonDateSettlementLabel = new Intl.NumberFormat('en-KE', { style: 'currency', currency: 'KES' }).format(results.comparisonSettlementValue);
    setInnnerHtml("comparisonDateSettlementLabel", formattedComparisonDateSettlementLabel);
    const formattedPercentage = (results.variance).toLocaleString(undefined, {
        style: 'percent',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    });
    setInnnerHtml("settlementVarianceLabelText", formattedPercentage);
    setDivClassBasedOnVariance("settlementKpi", results.variance, false)
}