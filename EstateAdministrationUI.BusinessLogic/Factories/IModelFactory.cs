using System;
using System.Collections.Generic;
using System.Text;

namespace EstateAdministrationUI.BusinessLogic.Factories
{
    using System.Threading.Tasks;
    using EstateManagement.DataTransferObjects.Responses;
    using Models;

    public interface IModelFactory
    {
        EstateModel ConvertFrom(EstateResponse source);
    }

    public class ModelFactory : IModelFactory
    {
        public EstateModel ConvertFrom(EstateResponse estateResponse)
        {
            if (estateResponse == null)
            {
                throw new ArgumentNullException(nameof(estateResponse));
            }

            EstateModel model = new EstateModel
                                {
                                    EstateId = estateResponse.EstateId,
                                    EstateName = estateResponse.EstateName,
                                    Operators = this.ConvertOperators(estateResponse.Operators),
                                    SecurityUsers = this.ConvertSecurityUsers(estateResponse.SecurityUsers)
                                };
            return model;
        }

        private List<SecurityUserModel> ConvertSecurityUsers(List<SecurityUserResponse> estateResponseSecurityUsers)
        {
            if (estateResponseSecurityUsers == null)
            {
                throw new ArgumentNullException(nameof(estateResponseSecurityUsers));
            }

            List<SecurityUserModel> models = new List<SecurityUserModel>();
            foreach (SecurityUserResponse estateResponseSecurityUser in estateResponseSecurityUsers)
            {
                models.Add(new SecurityUserModel
                           {
                               EmailAddress = estateResponseSecurityUser.EmailAddress,
                               SecurityUserId = estateResponseSecurityUser.SecurityUserId
                           });
            }

            return models;
        }

        private List<EstateOperatorModel> ConvertOperators(List<EstateOperatorResponse> estateResponseOperators)
        {
            if (estateResponseOperators == null)
            {
                throw new ArgumentNullException(nameof(estateResponseOperators));
            }

            List<EstateOperatorModel> models = new List<EstateOperatorModel>();
            foreach (EstateOperatorResponse estateOperatorResponse in estateResponseOperators)
            {
                models.Add(new EstateOperatorModel
                           {
                               Name = estateOperatorResponse.Name,
                               OperatorId = estateOperatorResponse.OperatorId,
                               RequireCustomMerchantNumber = estateOperatorResponse.RequireCustomMerchantNumber,
                               RequireCustomTerminalNumber = estateOperatorResponse.RequireCustomTerminalNumber
                           });
            }

            return models;
        }
    }
}
