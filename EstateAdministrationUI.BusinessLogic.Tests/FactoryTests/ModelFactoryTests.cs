namespace EstateAdministrationUI.BusinessLogic.Tests.FactoryTests{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EstateAdministrationUI.Factories;
    using EstateManagement.DataTransferObjects.Requests;
    using EstateManagement.DataTransferObjects.Responses;
    using Factories;
    using FileProcessor.DataTransferObjects.Responses;
    using Models;
    using Shouldly;
    using Testing;
    using TransactionProcessor.DataTransferObjects;
    using Xunit;
    using CalculationType = EstateManagement.DataTransferObjects.CalculationType;
    using FeeType = EstateManagement.DataTransferObjects.FeeType;
    using FileLineProcessingResult = Models.FileLineProcessingResult;

    public class ModelFactoryTests{
        #region Methods

        [Fact]
        public void ModelFactory_ConvertFrom_AddMerchantDeviceModel_IsConverted(){
            AddMerchantDeviceModel model = new AddMerchantDeviceModel{
                                                                         DeviceIdentifier = TestData.DeviceIdentifier
                                                                     };

            AddMerchantDeviceRequest apiRequest = ModelFactory.ConvertFrom(model);

            apiRequest.ShouldNotBeNull();
            apiRequest.DeviceIdentifier.ShouldBe(model.DeviceIdentifier);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddMerchantDeviceModel_ModelIsNull_ErrorThrown(){
            AddMerchantDeviceModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddMerchantDeviceResponse_IsConverted(){
            AddMerchantDeviceResponse response = new AddMerchantDeviceResponse{
                                                                                  MerchantId = TestData.MerchantId,
                                                                                  DeviceId = TestData.DeviceId,
                                                                                  EstateId = TestData.EstateId
                                                                              };

            AddMerchantDeviceResponseModel model = ModelFactory.ConvertFrom(response);

            model.ShouldNotBeNull();
            model.MerchantId.ShouldBe(response.MerchantId);
            model.DeviceId.ShouldBe(response.DeviceId);
            model.EstateId.ShouldBe(response.EstateId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddMerchantDeviceResponse_ResponseIsNull_ErrorThrown(){
            AddMerchantDeviceResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddProductToContractModel_NullModel_ErrorThrown(){
            AddProductToContractModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddProductToContractModel_WithNullValue_IsConverted(){
            AddProductToContractModel model = TestData.AddProductToContractModelWithNullValue;

            AddProductToContractRequest request = ModelFactory.ConvertFrom(model);

            request.Value.ShouldBeNull();
            request.ProductName.ShouldBe(model.ProductName);
            request.DisplayText.ShouldBe(model.DisplayText);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddProductToContractModel_WithValue_IsConverted(){
            AddProductToContractModel model = TestData.AddProductToContractModelWithValue;

            AddProductToContractRequest request = ModelFactory.ConvertFrom(model);

            request.Value.ShouldNotBeNull();
            request.Value.ShouldBe(model.Value);
            request.ProductName.ShouldBe(model.ProductName);
            request.DisplayText.ShouldBe(model.DisplayText);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddProductToContractResponse_IsConverted(){
            AddProductToContractResponse response = TestData.AddProductToContractResponse;

            AddProductToContractResponseModel model = ModelFactory.ConvertFrom(response);

            model.ProductId.ShouldBe(response.ProductId);
            model.ContractId.ShouldBe(response.ContractId);
            model.EstateId.ShouldBe(response.EstateId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddProductToContractResponse_NullResponse_ErrorThrown(){
            AddProductToContractResponse model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddTransactionFeeForProductToContractResponse_IsConverted(){
            AddTransactionFeeForProductToContractResponse response = TestData.AddTransactionFeeForProductToContractResponse;

            AddTransactionFeeToContractProductResponseModel model = ModelFactory.ConvertFrom(response);

            model.EstateId.ShouldBe(response.EstateId);
            model.ProductId.ShouldBe(response.ProductId);
            model.TransactionFeeId.ShouldBe(response.TransactionFeeId);
            model.ContractId.ShouldBe(response.ContractId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddTransactionFeeForProductToContractResponse_NullResponse_ErrorThrown(){
            AddTransactionFeeForProductToContractResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddTransactionFeeToContractProductModel_IsConverted(){
            AddTransactionFeeToContractProductModel model = TestData.AddTransactionFeeToContractProductModel;

            AddTransactionFeeForProductToContractRequest request = ModelFactory.ConvertFrom(model);

            CalculationType calculationType =
                Enum.Parse<CalculationType>(model.CalculationType.ToString(), true);
            FeeType feeType = Enum.Parse<FeeType>(model.FeeType.ToString(), true);

            request.Value.ShouldBe(model.Value);
            request.Description.ShouldBe(model.Description);
            request.FeeType.ShouldBe(feeType);
            request.CalculationType.ShouldBe(calculationType);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AddTransactionFeeToContractProductModel_NullModel_ErrorThrown(){
            AddTransactionFeeToContractProductModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AssignOperatorResponse_IsConverted(){
            AssignOperatorResponse response = new AssignOperatorResponse{
                                                                            EstateId = TestData.EstateId,
                                                                            MerchantId = TestData.MerchantId,
                                                                            OperatorId = TestData.OperatorId
                                                                        };

            AssignOperatorToMerchantResponseModel assignOperatorToMerchantResponseModel = ModelFactory.ConvertFrom(response);

            assignOperatorToMerchantResponseModel.ShouldNotBeNull();
            assignOperatorToMerchantResponseModel.EstateId.ShouldBe(response.EstateId);
            assignOperatorToMerchantResponseModel.MerchantId.ShouldBe(response.MerchantId);
            assignOperatorToMerchantResponseModel.OperatorId.ShouldBe(response.OperatorId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AssignOperatorResponse_ModelIsNull_ErrorThrown(){
            AssignOperatorResponse response = null;

            AssignOperatorToMerchantResponseModel assignOperatorToMerchantResponseModel = ModelFactory.ConvertFrom(response);

            assignOperatorToMerchantResponseModel.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AssignOperatorToMerchantModel_IsConverted(){
            AssignOperatorToMerchantModel model = new AssignOperatorToMerchantModel{
                                                                                       MerchantNumber = TestData.MerchantNumber,
                                                                                       TerminalNumber = TestData.TerminalNumber,
                                                                                       OperatorId = TestData.OperatorId
                                                                                   };
            AssignOperatorRequest assignOperatorRequest = ModelFactory.ConvertFrom(model);

            assignOperatorRequest.ShouldNotBeNull();
            assignOperatorRequest.MerchantNumber.ShouldBe(model.MerchantNumber);
            assignOperatorRequest.TerminalNumber.ShouldBe(model.TerminalNumber);
            assignOperatorRequest.OperatorId.ShouldBe(model.OperatorId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_AssignOperatorToMerchantModel_ModelIsNull_ErrorThrown(){
            AssignOperatorToMerchantModel model = null;

            AssignOperatorRequest assignOperatorRequest = ModelFactory.ConvertFrom(model);

            assignOperatorRequest.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_EmptyProducts_IsConverted(){
            ContractResponse response = TestData.ContractResponseEmptyProducts;

            ContractModel model = ModelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(0);
            model.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_IsConverted(){
            ContractResponse response = TestData.ContractResponse;

            ContractModel model = ModelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_NullProducts_IsConverted(){
            ContractResponse response = TestData.ContractResponseNullProducts;

            ContractModel model = ModelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(0);
            model.ContractProducts.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_NullResponse_ErrorThrown(){
            ContractResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_ProductWithEmptyFees_IsConverted(){
            ContractResponse response = TestData.ContractResponseProductWithEmptyFees;

            ContractModel model = ModelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
            model.ContractProducts.Single().NumberOfTransactionFees.ShouldBe(0);
            model.ContractProducts.Single().ContractProductTransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponse_ProductWithNullFees_IsConverted(){
            ContractResponse response = TestData.ContractResponseProductWithNullFees;

            ContractModel model = ModelFactory.ConvertFrom(response);

            model.Description.ShouldBe(response.Description);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.OperatorName.ShouldBe(response.OperatorName);
            model.EstateId.ShouldBe(response.EstateId);
            model.NumberOfProducts.ShouldBe(response.Products.Count);
            model.ContractProducts.Count.ShouldBe(response.Products.Count);
            model.ContractProducts.Single().NumberOfTransactionFees.ShouldBe(0);
            model.ContractProducts.Single().ContractProductTransactionFees.ShouldBeEmpty();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponseList_IsConverted(){
            List<ContractResponse> responseList = new List<ContractResponse>{
                                                                                TestData.ContractResponse
                                                                            };

            List<ContractModel> model = ModelFactory.ConvertFrom(responseList);
            model.ShouldHaveSingleItem();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_ContractResponseList_NullList_ErrorThrown(){
            List<ContractResponse> responseList = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(responseList); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractModel_IsConverted(){
            CreateContractModel model = TestData.CreateContractModel;

            CreateContractRequest request = ModelFactory.ConvertFrom(model);

            request.Description.ShouldBe(model.Description);
            request.OperatorId.ShouldBe(model.OperatorId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractModel_NullModel_ErrorThrown(){
            CreateContractModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractResponse_IsConverted(){
            CreateContractResponse response = TestData.CreateContractResponse;

            CreateContractResponseModel model = ModelFactory.ConvertFrom(response);

            model.EstateId.ShouldBe(response.EstateId);
            model.OperatorId.ShouldBe(response.OperatorId);
            model.ContractId.ShouldBe(response.ContractId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateContractResponse_NullResponse_ErrorThrown(){
            CreateContractResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Theory]
        [InlineData(SettlementSchedule.Immediate, EstateManagement.DataTransferObjects.SettlementSchedule.Immediate)]
        [InlineData(SettlementSchedule.Weekly, EstateManagement.DataTransferObjects.SettlementSchedule.Weekly)]
        [InlineData(SettlementSchedule.Monthly, EstateManagement.DataTransferObjects.SettlementSchedule.Monthly)]
        public void ModelFactory_ConvertFrom_CreateMerchantModel_ModelIsConverted(SettlementSchedule settlementSchedule,
                                                                                  EstateManagement.DataTransferObjects.SettlementSchedule expectedSettlementSchedule){
            CreateMerchantModel model = TestData.CreateMerchantModel(settlementSchedule);

            CreateMerchantRequest request = ModelFactory.ConvertFrom(model);

            request.Contact.ShouldNotBeNull();
            request.Contact.EmailAddress.ShouldBe(model.Contact.ContactEmailAddress);
            request.Contact.PhoneNumber.ShouldBe(model.Contact.ContactPhoneNumber);
            request.Contact.ContactName.ShouldBe(model.Contact.ContactName);

            request.Address.ShouldNotBeNull();
            request.Address.AddressLine1.ShouldBe(model.Address.AddressLine1);
            request.Address.AddressLine2.ShouldBe(model.Address.AddressLine2);
            request.Address.AddressLine3.ShouldBe(model.Address.AddressLine3);
            request.Address.AddressLine4.ShouldBe(model.Address.AddressLine4);
            request.Address.Town.ShouldBe(model.Address.Town);
            request.Address.Region.ShouldBe(model.Address.Region);
            request.Address.Country.ShouldBe(model.Address.Country);
            request.Address.PostalCode.ShouldBe(model.Address.PostalCode);

            request.Name.ShouldBe(model.MerchantName);
            request.SettlementSchedule.ShouldBe(expectedSettlementSchedule);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantModel_NullModel_ErrorThrown(){
            CreateMerchantModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantResponse_ModelIsConverted(){
            CreateMerchantResponse response = TestData.CreateMerchantResponse;

            CreateMerchantResponseModel model = ModelFactory.ConvertFrom(response);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.EstateId.ShouldBe(response.EstateId);
            model.AddressId.ShouldBe(response.AddressId);
            model.ContactId.ShouldBe(response.ContactId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateMerchantResponse_NullResponse_ErrorThrown(){
            CreateMerchantResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorModel_ModelIsConverted(){
            CreateOperatorModel model = TestData.CreateOperatorModel;

            CreateOperatorRequest request = ModelFactory.ConvertFrom(model);

            request.RequireCustomTerminalNumber.ShouldBe(model.RequireCustomTerminalNumber);
            request.RequireCustomMerchantNumber.ShouldBe(model.RequireCustomMerchantNumber);
            request.Name.ShouldBe(model.OperatorName);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorModel_NullModel_ErrorThrown(){
            CreateOperatorModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorResponse_ModelIsConverted(){
            CreateOperatorResponse response = TestData.CreateOperatorResponse;

            CreateOperatorResponseModel model = ModelFactory.ConvertFrom(response);

            model.OperatorId.ShouldBe(response.OperatorId);
            model.EstateId.ShouldBe(response.EstateId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_CreateOperatorResponse_NullResponse_ErrorThrown(){
            CreateOperatorResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_EmptyOperators_ModelIsConverted(){
            EstateResponse response = TestData.EstateResponse;
            response.Operators = new List<EstateOperatorResponse>();

            EstateModel model = ModelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.ShouldBeNull();
            response.SecurityUsers.ForEach(u => {
                                               SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                               securityUserModel.ShouldNotBeNull();
                                               securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                           });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_EmptySecurityUsers_ModelIsConverted(){
            EstateResponse response = TestData.EstateResponse;
            response.SecurityUsers = new List<SecurityUserResponse>();

            EstateModel model = ModelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            model.SecurityUsers.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_ModelIsConverted(){
            EstateResponse response = TestData.EstateResponse;

            EstateModel model = ModelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            response.SecurityUsers.ForEach(u => {
                                               SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                               securityUserModel.ShouldNotBeNull();
                                               securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                           });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullOperators_ModelIsConverted(){
            EstateResponse response = TestData.EstateResponse;
            response.Operators = null;

            EstateModel model = ModelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.ShouldBeNull();
            response.SecurityUsers.ForEach(u => {
                                               SecurityUserModel securityUserModel = model.SecurityUsers.SingleOrDefault(su => su.SecurityUserId == u.SecurityUserId);
                                               securityUserModel.ShouldNotBeNull();
                                               securityUserModel.EmailAddress.ShouldBe(u.EmailAddress);
                                           });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullResponse_ErrorThrown(){
            EstateResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_EstateResponse_NullSecurityUsers_ModelIsConverted(){
            EstateResponse response = TestData.EstateResponse;
            response.SecurityUsers = null;

            EstateModel model = ModelFactory.ConvertFrom(response);

            model.EstateName.ShouldBe(response.EstateName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           EstateOperatorModel operatorModel = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           operatorModel.ShouldNotBeNull();
                                           operatorModel.RequireCustomMerchantNumber.ShouldBe(o.RequireCustomMerchantNumber);
                                           operatorModel.RequireCustomTerminalNumber.ShouldBe(o.RequireCustomTerminalNumber);
                                           operatorModel.Name.ShouldBe(o.Name);
                                       });
            model.SecurityUsers.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_FileDetails_IsConverted(){
            FileDetails response = TestData.FileDetails;

            FileDetailsModel model = ModelFactory.ConvertFrom(response);

            model.ShouldNotBeNull();
            model.UserId.ShouldBe(response.UserId);
            model.EstateId.ShouldBe(response.EstateId);
            model.FileId.ShouldBe(response.FileId);
            model.FileImportLogId.ShouldBe(response.FileImportLogId);
            model.FileLocation.ShouldBe(response.FileLocation);
            model.FileProfileId.ShouldBe(response.FileProfileId);
            model.MerchantId.ShouldBe(response.MerchantId);
            model.ProcessingCompleted.ShouldBe(response.ProcessingCompleted);
            model.ProcessingSummary.ShouldNotBeNull();
            model.ProcessingSummary.IgnoredLines.ShouldBe(response.ProcessingSummary.IgnoredLines);
            model.ProcessingSummary.FailedLines.ShouldBe(response.ProcessingSummary.FailedLines);
            model.ProcessingSummary.NotProcessedLines.ShouldBe(response.ProcessingSummary.NotProcessedLines);
            model.ProcessingSummary.RejectedLines.ShouldBe(response.ProcessingSummary.RejectedLines);
            model.ProcessingSummary.SuccessfullyProcessedLines.ShouldBe(response.ProcessingSummary.SuccessfullyProcessedLines);
            model.ProcessingSummary.TotalLines.ShouldBe(response.ProcessingSummary.TotalLines);
            model.FileLines.ShouldNotBeNull();
            model.FileLines.ShouldNotBeEmpty();

            foreach (FileLine responseFileLine in response.FileLines){
                FileLineModel line = model.FileLines.SingleOrDefault(f => f.LineNumber == responseFileLine.LineNumber);
                line.ShouldNotBeNull();
                line.LineData.ShouldBe(responseFileLine.LineData);
                line.TransactionId.ShouldBe(responseFileLine.TransactionId);
                line.RejectionReason.ShouldBe(responseFileLine.RejectionReason);
                line.ProcessingResult.ToString().ShouldBe(responseFileLine.ProcessingResult.ToString());
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_FileDetails_NullDetails_IsConverted(){
            FileDetails response = null;

            FileDetailsModel model = ModelFactory.ConvertFrom(response);

            model.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_FileImportLogList_IsConverted(){
            FileImportLogList response = TestData.FileImportLogList;

            List<FileImportLogModel> modelList = ModelFactory.ConvertFrom(response);

            modelList.ShouldNotBeNull();
            modelList.ShouldNotBeEmpty();

            foreach (FileImportLog responseFileImportLog in response.FileImportLogs){
                FileImportLogModel fileImportLogModel = modelList.SingleOrDefault(m => m.FileImportLogId == responseFileImportLog.FileImportLogId);

                fileImportLogModel.ShouldNotBeNull();
                fileImportLogModel.ImportLogDateTime.ShouldBe(responseFileImportLog.ImportLogDateTime);
                fileImportLogModel.ImportLogDate.ShouldBe(responseFileImportLog.ImportLogDate);
                fileImportLogModel.ImportLogTime.ShouldBe(responseFileImportLog.ImportLogTime);
                fileImportLogModel.FileCount.ShouldBe(responseFileImportLog.FileCount);
                fileImportLogModel.Files.ShouldNotBeNull();
                fileImportLogModel.Files.ShouldNotBeEmpty();

                foreach (FileImportLogFile responseFileImportLogFile in responseFileImportLog.Files){
                    FileImportLogFileModel fileImportLogFileModel = fileImportLogModel.Files.SingleOrDefault(f => f.FileId == responseFileImportLogFile.FileId);

                    fileImportLogFileModel.ShouldNotBeNull();
                    fileImportLogFileModel.FileId.ShouldBe(responseFileImportLogFile.FileId);
                    fileImportLogFileModel.FileImportLogId.ShouldBe(responseFileImportLogFile.FileImportLogId);
                    fileImportLogFileModel.FilePath.ShouldBe(responseFileImportLogFile.FilePath);
                    fileImportLogFileModel.FileProfileId.ShouldBe(responseFileImportLogFile.FileProfileId);
                    fileImportLogFileModel.FileUploadedDateTime.ShouldBe(responseFileImportLogFile.FileUploadedDateTime);
                    fileImportLogFileModel.MerchantId.ShouldBe(responseFileImportLogFile.MerchantId);
                    fileImportLogFileModel.OriginalFileName.ShouldBe(responseFileImportLogFile.OriginalFileName);
                    fileImportLogFileModel.UserId.ShouldBe(responseFileImportLogFile.UserId);
                }
            }
        }

        [Fact]
        public void ModelFactory_ConvertFrom_FileImportLogList_ResponseIsNull_IsConverted(){
            FileImportLogList response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositModel_ModelIsConverted(){
            MakeMerchantDepositModel model = TestData.MakeMerchantDepositModel;

            MakeMerchantDepositRequest request = ModelFactory.ConvertFrom(model);

            request.Reference.ShouldBe(model.Reference);
            request.DepositDateTime.ShouldBe(model.DepositDateTime);
            request.Amount.ShouldBe(model.Amount);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositModel_NullModel_ErrorThrown(){
            MakeMerchantDepositModel model = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(model); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositResponse_ModelIsConverted(){
            MakeMerchantDepositResponse response = TestData.MakeMerchantDepositResponse;

            MakeMerchantDepositResponseModel model = ModelFactory.ConvertFrom(response);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.EstateId.ShouldBe(response.EstateId);
            model.DepositId.ShouldBe(response.DepositId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MakeMerchantDepositResponse_NullResponse_ErrorThrown(){
            MakeMerchantDepositResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantBalanceHistoryResponseList_EmptyResponse_ErrorThrown(){
            List<MerchantBalanceChangedEntryResponse> response = new List<MerchantBalanceChangedEntryResponse>();

            List<MerchantBalanceHistory> model = ModelFactory.ConvertFrom(response);

            model.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantBalanceHistoryResponseList_ModelIsConverted(){
            List<MerchantBalanceChangedEntryResponse> response = TestData.MerchantBalanceChangedEntryResponseList;

            List<MerchantBalanceHistory> model = ModelFactory.ConvertFrom(response);

            model.ShouldNotBeNull();
            model.ShouldNotBeEmpty();
            model.Count.ShouldBe(response.Count);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantBalanceHistoryResponseList_NullResponse_ErrorThrown(){
            List<MerchantBalanceChangedEntryResponse> response = null;

            List<MerchantBalanceHistory> model = ModelFactory.ConvertFrom(response);

            model.ShouldBeNull();
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantBalanceResponse_IsConverted(){
            MerchantBalanceResponse response = new MerchantBalanceResponse{
                                                                              MerchantId = TestData.MerchantId,
                                                                              AvailableBalance = TestData.AvailableBalance,
                                                                              Balance = TestData.Balance,
                                                                              EstateId = TestData.EstateId
                                                                          };

            MerchantBalanceModel model = ModelFactory.ConvertFrom(response);

            model.ShouldNotBeNull();
            model.MerchantId.ShouldBe(response.MerchantId);
            model.AvailableBalance.ShouldBe(response.AvailableBalance);
            model.Balance.ShouldBe(response.Balance);
            model.EstateId.ShouldBe(response.EstateId);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantBalanceResponse_ResponseIsNull_ErrorThrown(){
            MerchantBalanceResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyAddress_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Addresses = new List<AddressResponse>();

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.ShouldBeNull();
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyContacts_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Contacts = new List<ContactResponse>();

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.ShouldBeNull();

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyDevices_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Devices = new Dictionary<Guid, String>();

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.ShouldBeNull();

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_EmptyOperators_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Operators = new List<MerchantOperatorResponse>();

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.ShouldBeNull();
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Theory]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Immediate, SettlementSchedule.Immediate)]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Weekly, SettlementSchedule.Weekly)]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Monthly, SettlementSchedule.Monthly)]
        public void ModelFactory_ConvertFrom_MerchantResponse_ModelIsConverted(EstateManagement.DataTransferObjects.SettlementSchedule settlementSchedule,
                                                                               SettlementSchedule expectedSettlementSchedule){
            MerchantResponse response = TestData.MerchantResponse(settlementSchedule);
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.SettlementSchedule.ShouldBe(expectedSettlementSchedule);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullAddress_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Addresses = null;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.ShouldBeNull();
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullBalance_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = null;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(0);
            model.AvailableBalance.ShouldBe(0);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullContacts_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Contacts = null;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.ShouldBeNull();

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullDevices_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Devices = null;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.ShouldBeNull();

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullOperators_ModelIsConverted(){
            MerchantResponse response = TestData.MerchantResponse();
            MerchantBalanceResponse merchantBalanceResponse = TestData.MerchantBalanceResponse;
            response.Operators = null;

            MerchantModel model = ModelFactory.ConvertFrom(response, merchantBalanceResponse);

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.ShouldBeNull();
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
            model.Balance.ShouldBe(merchantBalanceResponse.Balance);
            model.AvailableBalance.ShouldBe(merchantBalanceResponse.AvailableBalance);
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponse_NullResponse_ErrorThrown(){
            MerchantResponse response = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(response, null); });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponseList_ModelIsConverted(){
            List<MerchantResponse> responseList = new List<MerchantResponse>{
                                                                                TestData.MerchantResponse()
                                                                            };

            List<MerchantModel> modelList = ModelFactory.ConvertFrom(responseList);

            modelList.ShouldNotBeNull();
            modelList.ShouldNotBeEmpty();
            MerchantModel model = modelList.Single();
            MerchantResponse response = responseList.Single();

            model.MerchantId.ShouldBe(response.MerchantId);
            model.MerchantName.ShouldBe(response.MerchantName);
            model.EstateId.ShouldBe(response.EstateId);
            model.Contacts.Count.ShouldBe(response.Contacts.Count);
            response.Contacts.ForEach(c => {
                                          // Find the contact in the view model
                                          ContactModel modelContact = model.Contacts.SingleOrDefault(mc => mc.ContactId == c.ContactId);
                                          modelContact.ShouldNotBeNull();
                                          modelContact.ContactName.ShouldBe(c.ContactName);
                                          modelContact.ContactPhoneNumber.ShouldBe(c.ContactPhoneNumber);
                                          modelContact.ContactEmailAddress.ShouldBe(c.ContactEmailAddress);
                                      });

            model.Devices.Count.ShouldBe(response.Devices.Count);
            foreach (KeyValuePair<Guid, String> device in response.Devices){
                response.Devices.ContainsKey(device.Key).ShouldBeTrue();
                response.Devices.ContainsValue(device.Value).ShouldBeTrue();
            }

            model.Operators.Count.ShouldBe(response.Operators.Count);
            response.Operators.ForEach(o => {
                                           // Find the operator in the view model
                                           MerchantOperatorModel modelOperator = model.Operators.SingleOrDefault(mo => mo.OperatorId == o.OperatorId);
                                           modelOperator.ShouldNotBeNull();
                                           modelOperator.Name.ShouldBe(o.Name);
                                           modelOperator.TerminalNumber.ShouldBe(o.TerminalNumber);
                                           modelOperator.MerchantNumber.ShouldBe(o.MerchantNumber);
                                       });
            model.Addresses.Count.ShouldBe(response.Addresses.Count);
            response.Addresses.ForEach(a => {
                                           // Find the operator in the view model
                                           AddressModel modelAddress = model.Addresses.SingleOrDefault(ma => ma.AddressId == a.AddressId);
                                           modelAddress.ShouldNotBeNull();
                                           modelAddress.AddressLine1.ShouldBe(a.AddressLine1);
                                           modelAddress.AddressLine2.ShouldBe(a.AddressLine2);
                                           modelAddress.AddressLine3.ShouldBe(a.AddressLine3);
                                           modelAddress.AddressLine4.ShouldBe(a.AddressLine4);
                                           modelAddress.Country.ShouldBe(a.Country);
                                           modelAddress.PostalCode.ShouldBe(a.PostalCode);
                                           modelAddress.Region.ShouldBe(a.Region);
                                           modelAddress.Town.ShouldBe(a.Town);
                                       });
        }

        [Fact]
        public void ModelFactory_ConvertFrom_MerchantResponseList_NullResponse_ErrorThrown(){
            List<MerchantResponse> responseList = null;

            Should.Throw<ArgumentNullException>(() => { ModelFactory.ConvertFrom(responseList); });
        }

        [Theory]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Failed, BusinessLogic.Models.FileLineProcessingResult.Failed)]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Ignored, BusinessLogic.Models.FileLineProcessingResult.Ignored)]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.NotProcessed, BusinessLogic.Models.FileLineProcessingResult.NotProcessed)]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Rejected, BusinessLogic.Models.FileLineProcessingResult.Rejected)]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Successful, BusinessLogic.Models.FileLineProcessingResult.Successful)]
        [InlineData(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult.Unknown, BusinessLogic.Models.FileLineProcessingResult.Unknown)]
        [InlineData((FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult)99, BusinessLogic.Models.FileLineProcessingResult.Unknown)]

        public void ViewModelFactory_ConvertFrom_FileLineProcessingResult_ResultConverted(FileProcessor.DataTransferObjects.Responses.FileLineProcessingResult processingResult,
                                                                                          BusinessLogic.Models.FileLineProcessingResult expectedResult)
        {
            FileLineProcessingResult result = ModelFactory.ConvertFrom(processingResult);
            result.ShouldBe(expectedResult);
        }

        [Theory]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Immediate, Models.SettlementSchedule.Immediate)]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Monthly, Models.SettlementSchedule.Monthly)]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.NotSet, Models.SettlementSchedule.Immediate)]
        [InlineData(EstateManagement.DataTransferObjects.SettlementSchedule.Weekly, Models.SettlementSchedule.Weekly)]
        public void ModelFactory_ConvertFrom_SettlementSchedule_IsConverted(EstateManagement.DataTransferObjects.SettlementSchedule settlementSchedule, Models.SettlementSchedule expectedResult){
            SettlementSchedule result = ModelFactory.ConvertFrom(settlementSchedule);
            result.ShouldBe(expectedResult);
        }

        #endregion
    }
}