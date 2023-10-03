var chai = require("chai");
const jsdom = require('jsdom')
var testData = require("./testData.js");
const { JSDOM } = jsdom;
var shared = require("../../EstateAdministrationUI/wwwroot/js/shared.js");

describe('Shared Tests', function () {
    describe('SetInnerHtml Tests ', function() {    
        beforeEach(() => {
            const dom = new JSDOM(
                `<html>
                   <body>
                   </body>
                 </html>`,
                 { url: 'http://localhost' },
              );
            
              global.window = dom.window;
              global.document = dom.window.document;
        });

        it("sets an element's inner html from value passed in", function () {
            var element = document.createElement('div');
            shared.setInnerHtml(element, "Test Value");
            chai.expect(element.innerHTML).equal('Test Value');
        });           
    });

    describe('getChartColors Tests', function() {  
            
        it("correct colours returned", function () {
            var result = shared.getChartColors();
            chai.expect(result).contains('#5cbae5');  
            chai.expect(result).contains('#b6d957');
            chai.expect(result).contains('#fac364');
            chai.expect(result).contains('#9ea8ad');
            chai.expect(result).contains('#1b7eac');
            chai.expect(result).contains('#759422');
            chai.expect(result).contains('#dd8e07');
            chai.expect(result).contains('#69767c');
            chai.expect(result).contains('#abdbf2');
            chai.expect(result).contains('#d7eaa2');
            chai.expect(result).contains('#fde5bd');
            chai.expect(result).contains('#d5dadc');
        }); 
    });
    
    describe('setDivClassBasedOnVariance Tests', function() {               
        const theories = [
            ["bg-gradient-success", 0, true, 'bg-gradient-info'], 
            ["bg-gradient-info", 0, true, 'bg-gradient-info'], 
            ["bg-gradient-warning", 0, true, 'bg-gradient-info'], 
            ["bg-gradient-danger", 0, true, 'bg-gradient-info'], 

            ["bg-gradient-success", -0.1, true, 'bg-gradient-success'], 
            ["bg-gradient-info", -0.1, true, 'bg-gradient-success'], 
            ["bg-gradient-warning", -0.1, true, 'bg-gradient-success'], 
            ["bg-gradient-danger", -0.1, true, 'bg-gradient-success'], 
          
            ["bg-gradient-success", 0.2, true, 'bg-gradient-warning'], 
            ["bg-gradient-info", 0.2, true, 'bg-gradient-warning'], 
            ["bg-gradient-warning", 0.2, true, 'bg-gradient-warning'], 
            ["bg-gradient-danger", 0.2, true, 'bg-gradient-warning'], 

            ["bg-gradient-success", 0.5, true, 'bg-gradient-danger'], 
            ["bg-gradient-info", 0.5, true, 'bg-gradient-danger'], 
            ["bg-gradient-warning", 0.5, true, 'bg-gradient-danger'], 
            ["bg-gradient-danger", 0.5, true, 'bg-gradient-danger'], 

            ["bg-gradient-success", 0, false, 'bg-gradient-info'], 
            ["bg-gradient-info", 0, false, 'bg-gradient-info'], 
            ["bg-gradient-warning", 0, false, 'bg-gradient-info'], 
            ["bg-gradient-danger", 0, false, 'bg-gradient-info'], 

            ["bg-gradient-success", 0.1, false, 'bg-gradient-success'], 
            ["bg-gradient-info", 0.1, false, 'bg-gradient-success'], 
            ["bg-gradient-warning", 0.1, false, 'bg-gradient-success'], 
            ["bg-gradient-danger", 0.1, false, 'bg-gradient-success'], 
          
            ["bg-gradient-success", -0.2, false, 'bg-gradient-warning'], 
            ["bg-gradient-info", -0.2, false, 'bg-gradient-warning'], 
            ["bg-gradient-warning", -0.2, false, 'bg-gradient-warning'], 
            ["bg-gradient-danger", -0.2, false, 'bg-gradient-warning'], 

            ["bg-gradient-success", -0.5, false, 'bg-gradient-danger'], 
            ["bg-gradient-info", -0.5, false, 'bg-gradient-danger'], 
            ["bg-gradient-warning", -0.5, false, 'bg-gradient-danger'], 
            ["bg-gradient-danger", -0.5, false, 'bg-gradient-danger'], 
        ];

        beforeEach(() => {
            const dom = new JSDOM(
                `<html>
                   <body>
                   </body>
                 </html>`,
                 { url: 'http://localhost' },
              );
            
              global.window = dom.window;
              global.document = dom.window.document;
        });
        theories.forEach(([startclass, variance, lessisgood, expectedclass]) =>{

            it(`with variance ${variance} and lessisgood ${lessisgood} class list contains ${expectedclass} and not ${startclass}`,
            () =>
            {                       
                var element = document.createElement('div');                    
                element.setAttribute("id","testdiv");
                element.classList.add(startclass);
                          
                element = shared.setDivClassBasedOnVariance(element, variance, lessisgood);                
                console.log(element.classList[0]);
                console.log(element.classList[1]);
                chai.expect(element.classList.length).equal(1);
                chai.expect(element.classList[0]).equal(expectedclass);
                
            });
        });
    });

    describe('translateSalesValueByHour tests', function() {
        it("Container is null empty array returned", function () {
            var data = testData.getSalesValueByHour_ContainerIsNull();
            var result = shared.translateSalesValueByHour(data);
            chai.expect(result.length).equal(0);
        }),

        it("data is null empty array returned", function () {
            var data = testData.getSalesValueByHour_DataArrayIsNull();
            var result = shared.translateSalesValueByHour(data);
            chai.expect(result.length).equal(0);
        }),

        it("data is empty array with labels returned", function () {
            var data = testData.getSalesValueByHour_DataArrayIsEmpty();
            var result = shared.translateSalesValueByHour(data);
            chai.expect(result.length).equal(1);
        }),
        it("data for all 24 hours in array correct results returned", function () {
            var data = testData.getSalesValueByHour_HasData();
            var result = shared.translateSalesValueByHour(data);
            chai.expect(result.length).equal(25);

            // Compare the labels
            chai.expect(result[0][0].label).to.equal("Hour");
            chai.expect(result[0][1].label).to.equal("Todays Sales Value");
            chai.expect(result[0][2].label).to.equal("Comparison Date Sales Value");
            
            // Compare the hours
            for (let index = 0; index < 24; index++) {
                const actual = result[index+1];
                const expected = data.transactionHourViewModels[index];

                chai.expect(actual.c[0].v).to.equal(expected.hour);
                chai.expect(actual.c[1].v).to.equal(expected.todaysValue);
                chai.expect(actual.c[2].v).to.equal(expected.comparisonValue);
            }            
        })
    });

    describe('translateSalesCountByHour tests', function() {
        it("Container is null empty array returned", function () {
            var data = testData.getSalesCountByHour_ContainerIsNull();
            var result = shared.translateSalesCountByHour(data);
            chai.expect(result.length).equal(0);
        }),

        it("data is null empty array returned", function () {
            var data = testData.getSalesCountByHour_DataArrayIsNull();
            var result = shared.translateSalesCountByHour(data);
            chai.expect(result.length).equal(0);
        }),

        it("data is empty array with labels returned", function () {
            var data = testData.getSalesCountByHour_DataArrayIsEmpty();
            var result = shared.translateSalesCountByHour(data);
            chai.expect(result.length).equal(1);
        }),
        it("data for all 24 hours in array correct results returned", function () {
            var data = testData.getSalesCountByHour_HasData();
            var result = shared.translateSalesCountByHour(data);
            chai.expect(result.length).equal(25);

            // Compare the labels
            chai.expect(result[0][0].label).to.equal("Hour");
            chai.expect(result[0][1].label).to.equal("Todays Sales Count");
            chai.expect(result[0][2].label).to.equal("Comparison Date Sales Count");
            
            // Compare the hours
            for (let index = 0; index < 24; index++) {
                const actual = result[index+1];
                const expected = data.transactionHourViewModels[index];

                chai.expect(actual.c[0].v).to.equal(expected.hour);
                chai.expect(actual.c[1].v).to.equal(expected.todaysCount);
                chai.expect(actual.c[2].v).to.equal(expected.comparisonCount);
            }            
        })
    });
    
    //drawLineChart
    //drawBarChart
    //setupChartOptions
});

