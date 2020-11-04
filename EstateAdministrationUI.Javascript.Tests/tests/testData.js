module.exports = {
    getTransactionsByDateViewModel: function() { return {
        transactionDateViewModels: [
            {
                "date": "2020-10-01",
                "numberOfTransactions": 10,
                "valueOfTransactions": 10000.00
            },
            {
                "date": "2020-10-02",
                "numberOfTransactions": 20,
                "valueOfTransactions": 20000.00
            },
            {
                "date": "2020-10-03",
                "numberOfTransactions": 30,
                "valueOfTransactions": 30000.00
            }
            ]
        }
    },

    getTransactionsByDateViewModelWithNullDateModels: function()
    {
        return {
            transactionDateViewModels: null
        }
    },

    getTransactionsByDateViewModelWithEmptyDateModels: function () {
        return {
            transactionDateViewModels: []
        }
    },

    getTransactionsByWeekViewModel: function () {
        return {
            transactionWeekViewModels: [
                {
                    "weekNumber": 1,
                    "year": 2020,
                    "numberOfTransactions": 10,
                    "valueOfTransactions": 10000.00
                },
                {
                    "weekNumber": 2,
                    "year": 2020,
                    "numberOfTransactions": 20,
                    "valueOfTransactions": 20000.00
                },
                {
                    "weekNumber": 3,
                    "year": 2020,
                    "numberOfTransactions": 30,
                    "valueOfTransactions": 30000.00
                }
            ]
        }
    },

    getTransactionsByWeekViewModelWithNullWeekModels: function () {
        return {
            transactionWeekViewModels: null
        }
    },

    getTransactionsByWeekViewModelWithEmptyWeekModels: function () {
        return {
            transactionWeekViewModels: []
        }
    },

    getTransactionsByMonthViewModel: function () {
        return {
            transactionMonthViewModels: [
                {
                    "monthNumber": 1,
                    "year": 2020,
                    "numberOfTransactions": 10,
                    "valueOfTransactions": 10000.00
                },
                {
                    "monthNumber": 2,
                    "year": 2020,
                    "numberOfTransactions": 20,
                    "valueOfTransactions": 20000.00
                },
                {
                    "monthNumber": 3,
                    "year": 2020,
                    "numberOfTransactions": 30,
                    "valueOfTransactions": 30000.00
                }
            ]
        }
    },

    getTransactionsByMonthViewModelWithNullMonthModels: function () {
        return {
            transactionMonthViewModels: null
        }
    },

    getTransactionsByMonthViewModelWithEmptyMonthModels: function () {
        return {
            transactionMonthViewModels: []
        }
    },
};