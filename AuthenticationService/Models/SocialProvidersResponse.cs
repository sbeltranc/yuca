using System.Collections.Generic;

using System.Collections.Generic;

namespace AuthenticationService.Models
{
    public class SocialProvidersResponse
    {
        public List<ProviderInfoModel> Providers { get; set; } = new List<ProviderInfoModel>();
    }
}
