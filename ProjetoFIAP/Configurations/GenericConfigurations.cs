using System.ComponentModel;
using System.Globalization;

namespace ProjetoFIAP.Api.Configurations
{
    public static class GenericConfigurations
    {
        public static void ConfigureCultureInfo()
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }
    }
}
