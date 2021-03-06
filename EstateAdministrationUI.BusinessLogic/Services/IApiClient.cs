﻿namespace EstateAdministrationUI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic.Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IApiClient
    {
        #region Methods

        /// <summary>
        /// Adds the product to contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="addProductToContractModel">The add product to contract model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AddProductToContractResponseModel> AddProductToContract(String accessToken,
                                                                     ClaimsIdentity claimsIdentity,
                                                                     Guid contractId,
                                                                     AddProductToContractModel addProductToContractModel,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Adds the transaction fee to contract product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="addTransactionFeeToContractProductModel">The add transaction fee to contract product model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<AddTransactionFeeToContractProductResponseModel> AddTransactionFeeToContractProduct(String accessToken,
                                                                                                 ClaimsIdentity claimsIdentity,
                                                                                                 Guid contractId,
                                                                                                 Guid contractProductId,
                                                                                                 AddTransactionFeeToContractProductModel
                                                                                                     addTransactionFeeToContractProductModel,
                                                                                                 CancellationToken cancellationToken);

        /// <summary>
        /// Creates the contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createContractModel">The create contract model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CreateContractResponseModel> CreateContract(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateContractModel createContractModel,
                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Creates the merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createMerchantModel">The create merchant model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CreateMerchantResponseModel> CreateMerchant(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateMerchantModel createMerchantModel,
                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Creates the operator.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="createOperatorModel">The create operator model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<CreateOperatorResponseModel> CreateOperator(String accessToken,
                                                         ClaimsIdentity claimsIdentity,
                                                         CreateOperatorModel createOperatorModel,
                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Gets the contract.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ContractModel> GetContract(String accessToken,
                                        ClaimsIdentity claimsIdentity,
                                        Guid contractId,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Gets the contract product.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="contractProductId">The contract product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ContractProductModel> GetContractProduct(String accessToken,
                                                      ClaimsIdentity claimsIdentity,
                                                      Guid contractId,
                                                      Guid contractProductId,
                                                      CancellationToken cancellationToken);

        /// <summary>
        /// Gets the contracts.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<List<ContractModel>> GetContracts(String accessToken,
                                               ClaimsIdentity claimsIdentity,
                                               CancellationToken cancellationToken);

        /// <summary>
        /// Gets the estate.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<EstateModel> GetEstate(String accessToken,
                                    ClaimsIdentity claimsIdentity,
                                    CancellationToken cancellationToken);

        /// <summary>
        /// Gets the merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<MerchantModel> GetMerchant(String accessToken,
                                        ClaimsIdentity claimsIdentity,
                                        Guid merchantId,
                                        CancellationToken cancellationToken);

        /// <summary>
        /// Gets the merchant balance history.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<List<MerchantBalanceHistory>> GetMerchantBalanceHistory(String accessToken,
                                                                     ClaimsIdentity claimsIdentity,
                                                                     Guid merchantId,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Gets the merchants.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<List<MerchantModel>> GetMerchants(String accessToken,
                                               ClaimsIdentity claimsIdentity,
                                               CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDateModel> GetTransactionsByDate(String accessToken,
                                                            ClaimsIdentity claimsIdentity,
                                                            DateTime startDate,
                                                            DateTime endDate,
                                                            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMerchantModel> GetTransactionsByMerchant(String accessToken,
                                                                    ClaimsIdentity claimsIdentity,
                                                                    DateTime startDate,
                                                                    DateTime endDate,
                                                                    Int32 recordCount,
                                                                    SortDirection sortDirection,
                                                                    SortField sortField,
                                                                    CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMonthModel> GetTransactionsByMonth(String accessToken,
                                                              ClaimsIdentity claimsIdentity,
                                                              DateTime startDate,
                                                              DateTime endDate,
                                                              CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by operator.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByOperatorModel> GetTransactionsByOperator(String accessToken,
                                                                    ClaimsIdentity claimsIdentity,
                                                                    DateTime startDate,
                                                                    DateTime endDate,
                                                                    Int32 recordCount,
                                                                    SortDirection sortDirection,
                                                                    SortField sortField,
                                                                    CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByWeekModel> GetTransactionsByWeek(String accessToken,
                                                            ClaimsIdentity claimsIdentity,
                                                            DateTime startDate,
                                                            DateTime endDate,
                                                            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for date period.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="datePeriod">The date period.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionForPeriodModel> GetTransactionsForDatePeriod(String accessToken,
                                                                     ClaimsIdentity claimsIdentity,
                                                                     DatePeriod datePeriod,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Makes the merchant deposit.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="makeMerchantDepositModel">The make merchant deposit model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<MakeMerchantDepositResponseModel> MakeMerchantDeposit(String accessToken,
                                                                   ClaimsIdentity claimsIdentity,
                                                                   Guid merchantId,
                                                                   MakeMerchantDepositModel makeMerchantDepositModel,
                                                                   CancellationToken cancellationToken);

        #endregion
    }
}