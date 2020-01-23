using RestSharp;

namespace TeleBotService.Model
{
    public class RestCall
    {
        public string RestClientComm(string uri, string endPoint, string methodType)
        {

            var _client = new RestClient(uri);
            IRestResponse result = null;
            //_client.Authenticator = new HttpBasicAuthenticator(username: username, password: password);
            var request = new RestRequest(resource: endPoint, DataFormat.Json);


            switch (methodType)
            {
                default: //GET
                    result = _client.Get(request);
                    break;
            }

            return result.Content;

        }
    }
}
