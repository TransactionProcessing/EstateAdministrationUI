using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateAdministrationUI.Services
{
    using System.ComponentModel;
    using System.Security.Claims;
    using System.Threading;
    using BusinessLogic.Factories;
    using BusinessLogic.Models;
    using EstateManagement.Client;
    using EstateManagement.DataTransferObjects.Responses;
    using Shared.Logger;

    public interface IApiClient
    {
        Task<EstateModel> GetEstate(String accessToken, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken);
    }

    public class ApiClient : IApiClient
    {
        private readonly IEstateClient EstateClient;

        private readonly IModelFactory ModelFactory;

        public ApiClient(IEstateClient estateClient, IModelFactory modelFactory)
        {
            this.EstateClient = estateClient;
            this.ModelFactory = modelFactory;
        }

        public async Task<EstateModel> GetEstate(String accessToken, ClaimsIdentity claimsIdentity, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Access Token is [{accessToken}]");

            Guid estateId = ApiClient.GetClaimValue<Guid>(claimsIdentity, "EstateId");

            EstateResponse estate = await this.EstateClient.GetEstate(accessToken, estateId, cancellationToken);

            return this.ModelFactory.ConvertFrom(estate);
        }

        /// <summary>
        /// Gets the claim value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <param name="claimType">Type of the claim.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">User {claimsIdentity.Name} does not have Claim [{claimType}]</exception>
        private static T GetClaimValue<T>(ClaimsIdentity claimsIdentity,
                                          String claimType)
        {
            if (!claimsIdentity.HasClaim(x => x.Type == claimType))
            {
                throw new InvalidOperationException($"User {claimsIdentity.Name} does not have Claim [{claimType}]");
            }

            Claim claim = claimsIdentity.Claims.Single(x => x.Type == claimType);
            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
        }
    }
}
