using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Application.Providers
{
    public class RetailerProvider: IRetailerProvider
    {
        public async Task<string> ConfirmOrderAsync(Guid cartId)
        {
            var client = new RestClient("http://localhost:3000/api/order");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(new { cartId = cartId.ToString() }, "application/json");

            var response = await client.ExecuteAsync(request);
            var json = JObject.Parse(response.Content);
            var orderNumber = json.Value<string>("orderNumber");

            return orderNumber;
        }
    }
}
