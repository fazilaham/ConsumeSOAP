using ConsumeSOAP.Service;
using CountryInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeSOAP.Controllers
{
    [ApiController]
    public class SoapController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public SoapController()
        {
            _countryService = new CountryService(new HttpClient());
        }

        [HttpGet]
        [Route("api/capital/{IsoCode}")]
        public IActionResult GetCountryFromSoapApi(string IsoCode)
        {
            var country = _countryService.GetCapital(IsoCode);
            if (country == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);
            }
        }

        [HttpGet]
        [Route("api/capitalXml/{IsoCode}")]
        public IActionResult ReturnXmlFromSoapApi(string IsoCode)
        {
            var country = _countryService.GetCountryCapitalXml(IsoCode);
            if (country == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(country);
            }
        }

        [HttpGet]
        [Route("api/capital/test")]
        public IActionResult Test()
        {
            return Ok("works");
        }

        [HttpGet]
        [Route("api/capitalWebRef/test")]
        public IActionResult WebRef()
        {
            CapitalCityRequestBody capitalCityRequest = new CapitalCityRequestBody("IND");
            CapitalCityRequest capitalCityRequest1 = new CapitalCityRequest(capitalCityRequest);
            


            CapitalCityResponse capitalCityResponse = new CapitalCityResponse();
            CapitalCityResponseBody capitalCityResponseBody = new CapitalCityResponseBody();





            return Ok();
        }
    }
}
