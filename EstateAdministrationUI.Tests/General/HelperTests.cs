using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstateAdministrationUI.Bootstrapper;
using Shouldly;
using Xunit;

namespace EstateAdministrationUI.Tests.General
{
    public class HelperTests
    {
        [Theory]
        [InlineData("https://localhost:5001/", null, null, "https://localhost:5001", "https://localhost:5001")]
        [InlineData("https://localhost:5001/", "", "", "https://localhost:5001", "https://localhost:5001")]
        [InlineData("https://localhost", null, null, "https://localhost:5001", "https://localhost:5001")]
        [InlineData("https://localhost", "", "", "https://localhost:5001", "https://localhost:5001")]
        [InlineData("https://localhost", "1234", null, "https://localhost:1234", "https://localhost:5001")]
        [InlineData("https://localhost", "1234", "", "https://localhost:1234", "https://localhost:5001")]
        [InlineData("https://localhost:5001", "1234", null, "https://localhost:1234", "https://localhost:5001")]
        [InlineData("https://localhost:5001", "1234", "", "https://localhost:1234", "https://localhost:5001")]
        [InlineData("https://localhost", null,"1234", "https://localhost:5001", "https://localhost:1234")]
        [InlineData("https://localhost", "","1234",  "https://localhost:5001", "https://localhost:1234")]
        [InlineData("https://localhost:5001",null, "1234",  "https://localhost:5001", "https://localhost:1234")]
        [InlineData("https://localhost:5001", "", "1234", "https://localhost:5001", "https://localhost:1234")]
        [InlineData("https://localhost", "1234", "5678", "https://localhost:1234", "https://localhost:5678")]
        [InlineData("https://localhost:5001", "1234", "5678", "https://localhost:1234", "https://localhost:5678")]
        public void Helpers_GetSecurityServiceAddresses_CorrectAddressesReturned(String authority, String securityServiceLocalPort, String securityServicePort, String expectedAuthorityAddress, String expectedIssuerAddress)
        {
            var result = Helpers.GetSecurityServiceAddresses(authority, securityServiceLocalPort, securityServicePort);
            result.authorityAddress.ShouldBe(expectedAuthorityAddress);
            result.issuerAddress.ShouldBe(expectedIssuerAddress);
        }

    }
}
