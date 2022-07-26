using EnsekTechnicalTest.Context;
using EnsekTechnicalTest.DTO;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;

namespace EnsekTechnicalTest
{
    public class ApiHelper
    {
        readonly ScenarioContext _scenarioContext;
        string Token;
        const string BaseUrl = "https://ensekapicandidatetest.azurewebsites.net";
        public ApiHelper(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        public RestClient GetClient => new RestClient(BaseUrl);

        public RestRequest CreatePostRequest(string resource, string body)
        {
            var request = new RestRequest(resource, Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            return request;
        }

        public RestRequest CreatePutRequest(string resource)
        {

            var request = new RestRequest(resource, Method.Put);

            request.AddHeader("Authorization", $"Bearer {Token}");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        public RestResponse GetPostReponse(RestRequest request)
        {
            return GetClient.Execute(request);
        }

        public RestResponse GetPutReponse(RestRequest request)
        {
            return GetClient.Execute(request);
        }

        public void CreateToken(string username, string password)
        {
            var body = @"{
                            ""username"": ""test"",
                            ""password"": ""testing""
                        }";

            var request = CreatePostRequest("login", body);
            var response = GetPostReponse(request);
            var count = 0;

            //Need to add this check because unable to get the accesstoken in the first attempt 
            while (!response.IsSuccessful && count < 5)
            {
                response = GetPostReponse(request);
            }

            var context = new ContextProperties(_scenarioContext);
            Token = context.Token = JsonConvert.DeserializeObject<AccessTokenDTO>(response.Content).AccessToken;
        }

        public T GetResponseContent<T>(string resource)
        {
            var request = new RestRequest(resource, Method.Get);

            request.AddHeader("Authorization", $"Bearer {Token}");

            request.AddHeader("Accept", "application/json");

            var response = GetClient.Get(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
