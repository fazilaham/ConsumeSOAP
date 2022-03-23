using ConsumeSOAP.Model;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace ConsumeSOAP.Service
{
    public class CountryService : ICountryService
    {
        private HttpClient _httpClient;
        private static string soapStr = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                            <soap12:Envelope xmlns:soap12 =""http://www.w3.org/2003/05/soap-envelope"">
                                                <soap12:Body>
                                                <CapitalCity xmlns =""http://www.oorsprong.org/websamples.countryinfo"">
                                                    <sCountryISOCode>{0}</sCountryISOCode>
                                                </CapitalCity>
                                            </soap12:Body>
                                        </soap12:Envelope>";
        public CountryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string GetCountryCapitalXml(string countryIsoCode)
        {
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string baseUrl = "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso?op=CapitalCity";
                    using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
                    {
                        httpRequestMessage.Method = HttpMethod.Post;
                        httpRequestMessage.RequestUri = new Uri(baseUrl);
                        string content = String.Format(soapStr, countryIsoCode);
                        httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, "text/xml");

                        httpRequestMessage.Headers.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                        httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

                        HttpResponseMessage response = httpClient.SendAsync(httpRequestMessage).Result;
                        Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                        Stream stream = streamTask.Result;
                        var sr = new StreamReader(stream);
                        var soapResponse = XDocument.Load(sr);
                        return soapResponse.ToString();
                    }
                }
            }        
        }
        public Country GetCapital(string countryIsoCode)
        {
            string soapResult = GetCountryCapitalXml(countryIsoCode);
            if (soapResult.Contains("CapitalCityResult"))
            {
                int start = soapResult.IndexOf("CapitalCityResult>") + 18;
                int end = soapResult.IndexOf('<', start);
                string capital = soapResult.Substring(start, end - start);
                return new Country(capital);
            }
            else
            {
                return new Country("not found");
            }
        }
    }
}
