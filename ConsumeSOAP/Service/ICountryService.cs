using ConsumeSOAP.Model;

namespace ConsumeSOAP.Service
{
    public interface ICountryService
    {
        string GetCountryCapitalXml(string countryIsoCode);
        public Country GetCapital(string countryIsoCode);
    }
}
