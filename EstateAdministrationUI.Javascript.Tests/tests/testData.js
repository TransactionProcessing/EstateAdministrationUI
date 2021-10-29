module.exports = {
    getDataByDateViewModel: function() { return {

            // Note: in javascript the months are 0 bases (11 = October)
            dataDateViewModels: [
            {
                "date": "2020-11-01",
                "count": 10,
                "value": 10000.00
            },
            {
                "date": "2020-11-02",
                "count": 20,
                "value": 20000.00
            },
            {
                "date": "2020-11-03",
                "count": 30,
                "value": 30000.00
            }
            ]
        }
    },

    getDataByDateViewModelWithNullDateModels: function()
    {
        return {
            dataDateViewModels: null
        }
    },

    getDataByDateViewModelWithEmptyDateModels: function () {
        return {
            dataDateViewModels: []
        }
    },

    getDataByWeekViewModel: function () {
        return {
            dataWeekViewModels: [
                {
                    "weekNumber": 1,
                    "year": 2020,
                    "count": 10,
                    "value": 10000.00
                },
                {
                    "weekNumber": 2,
                    "year": 2020,
                    "count": 20,
                    "value": 20000.00
                },
                {
                    "weekNumber": 3,
                    "year": 2020,
                    "count": 30,
                    "value": 30000.00
                }
            ]
        }
    },

    getDataByWeekViewModelWithNullWeekModels: function () {
        return {
            dataWeekViewModels: null
        }
    },

    getDataByWeekViewModelWithEmptyWeekModels: function () {
        return {
            dataWeekViewModels: []
        }
    },

    getDataByMonthViewModel: function () {
        return {
            dataMonthViewModels: [
                {
                    "monthNumber": 1,
                    "year": 2020,
                    "count": 10,
                    "value": 10000.00
                },
                {
                    "monthNumber": 2,
                    "year": 2020,
                    "count": 20,
                    "value": 20000.00
                },
                {
                    "monthNumber": 3,
                    "year": 2020,
                    "count": 30,
                    "value": 30000.00
                }
            ]
        }
    },

    getDataByMonthViewModelWithNullMonthModels: function () {
        return {
            dataMonthViewModels: null
        }
    },

    getDataByMonthViewModelWithEmptyMonthModels: function () {
        return {
            dataMonthViewModels: []
        }
    },

    getDataByMerchantViewModel: function()
    {
        return {
            dataMerchantViewModels: [
                {
                    "currencyCode": "",
                    "merchantId": "a05d6531-4379-46ae-9771-608aeede05ea",
                    "merchantName": "Test Merchant 3",
                    "count": 434,
                    "value": 62184.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "439f76f3-81c1-4551-bc2e-55d193e5841d",
                    "merchantName": "Emulator Merchant",
                    "count": 404,
                    "value": 56943.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "96b61e55-23e3-416a-9c39-d0b8cf84310a",
                    "merchantName": "Xperia Merchant",
                    "count": 395,
                    "value": 56553.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "689f724c-143e-4c8f-ad17-1b115e8b0fea",
                    "merchantName": "Test Merchant 1",
                    "count": 384,
                    "value": 54715.0
                },
                {
                    "currencyCode": "",
                    "merchantId": "14f26bad-b0b5-40bf-9666-8d8b9cf5f8d2",
                    "merchantName": "S7 Merchant",
                    "count": 372,
                    "value": 53870.0
                }
            ]
        };
    },

    getDataByMerchantViewModelWithNullMerchantModels: function () {
        return {
            dataMerchantViewModels: null
        }
    },

    getDataByMerchantViewModelWithEmptyMerchantModels: function () {
        return {
            dataMerchantViewModels: []
        }
    },

    getDataByOperatorViewModel: function () {
        return {
            dataOperatorViewModels: [
                {
                    "currencyCode": "",
                    "operatorName": "Test Operator 1",
                    "count": 434,
                    "value": 62184.0
                },
                {
                    "currencyCode": "",
                    "operatorName": "Test Operator 2",
                    "count": 404,
                    "value": 56943.0
                }
            ]
        };
    },

    getDataByOperatorViewModelWithNullOperatorModels: function () {
        return {
            dataOperatorViewModels: null
        }
    },

    getDataByOperatorViewModelWithEmptyOperatorModels: function () {
        return {
            dataOperatorViewModels: []
        }
    },
};