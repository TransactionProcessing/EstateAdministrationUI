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

    getTransactionsByMerchantViewModel: function()
    {
        return {
            transactionMerchantViewModels: [
                {
                    "currencyCode": "",
                    "merchantId": "a05d6531-4379-46ae-9771-608aeede05ea",
                    "merchantName": "Test Merchant 3",
                    "numberOfTransactions": 434,
                    "valueOfTransactions": 62184.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "439f76f3-81c1-4551-bc2e-55d193e5841d",
                    "merchantName": "Emulator Merchant",
                    "numberOfTransactions": 404,
                    "valueOfTransactions": 56943.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "96b61e55-23e3-416a-9c39-d0b8cf84310a",
                    "merchantName": "Xperia Merchant",
                    "numberOfTransactions": 395,
                    "valueOfTransactions": 56553.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "689f724c-143e-4c8f-ad17-1b115e8b0fea",
                    "merchantName": "Test Merchant 1",
                    "numberOfTransactions": 384,
                    "valueOfTransactions": 54715.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "14f26bad-b0b5-40bf-9666-8d8b9cf5f8d2",
                    "merchantName": "S7 Merchant",
                    "numberOfTransactions": 372,
                    "valueOfTransactions": 53870.0
                }
            ]
        };
    }
};