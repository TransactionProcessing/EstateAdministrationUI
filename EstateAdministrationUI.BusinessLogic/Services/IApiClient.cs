namespace EstateAdministrationUI.Services
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