using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.EasyDelivery.Geolocalizacao.Models;
using Domain.EasyDelivery.Geolocalizacao.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infra.CrossCutting.Geolocalizacao.Services
{
    public class CoordenadasService : ICoordenadasService
    {

        private readonly IConfiguration _configuration;
        const string placesUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json";

        public CoordenadasService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CoordenadasVO> ObterCoordenadasEnderecoAsync(string uf, string cidade, string rua, string numero, string cep)
        {
            try
            {

                var apiKey = _configuration["GoogleApis:PlacesKey"];

                var endereco = $"{uf}+{cidade}+{rua}+{numero}+{cep}".Replace(" ", "+");

                var url = placesUrl + $"?query=Brasil+{endereco}&key={apiKey}";

                using (var http = new HttpClient())
                {
                    var response = await http.GetStringAsync(url);
                    var json = JsonConvert.DeserializeObject<GoogleMapsResponse>(response);

                    if (!json.results.Any()) return new CoordenadasVO(0, 0);

                    var location = json.results.First().geometry.location;
                    return new CoordenadasVO(Convert.ToDecimal(location.lat), Convert.ToDecimal(location.lng));
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}