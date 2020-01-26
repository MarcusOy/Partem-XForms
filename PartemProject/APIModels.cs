using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Xamarin.Forms;
using static PartemProject.APIModels;

namespace PartemProject
{
    public class APIModels
    {
        public class ServiceRequest
        {
            public string Url { get; set; }
        }

        public class ServiceResponse
        {
            public double LeftPercentage { get; set; }
            public double CenterPercentage { get; set; }
            public double RightPercentage { get; set; }

            public string Headline { get; set; }
            public string Source { get; set; }
            public string Image { get; set; }

            public bool Success { get; set; }
            public string Error { get; set; }
        }
    }

    public class ApiAdapter
    {
        RestClient client;

        public ApiAdapter()
        {
            //this.client = new RestClient("http://partem.tech/");
            //this.client = new RestClient("https://localhost:5001/");
            this.client = new RestClient("https://partem-1579924523879.appspot.com/");
        }

        public async Task<ServiceResponse> MakeRequest(ServiceRequest request)
        {
            try
            {
                var response = JsonConvert.DeserializeObject
                    <ServiceResponse>(MakeWebRequestAsync(request, "api/Query").Result);
                if (!response.Success)
                {
                    MessagingCenter.Send<object, string>(this, "APIError", response.Error);
                }
                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Error = "Failed to connect to the server. Please try again."
                };
            }
        }

        public async Task<ServiceResponse> MakeSampleRequest(ServiceRequest request)
        {
            var response = new ServiceResponse
            {
                LeftPercentage = 79.2342 / 100,
                CenterPercentage = 12.2133 / 100,
                RightPercentage = 89.3424 / 100,

                Headline = "Florida man dies after sticking a stick of dynamite into his mother's left ear canal.",
                Source = "FoxNews.com",
                Image = "https://a57.foxnews.com/static.foxnews.com/foxnews.com/content/uploads/2019/10/931/524/AP19296533476603.jpg",

                Success = true,
                
            };
            return response;
        }
        public async Task<string> MakeWebRequestAsync(Object request, String uri)
        {
            var json = JsonConvert.SerializeObject(request);
            var payload = new RestRequest(uri, Method.POST);
            // TODO: Implement simple authentication using client secret
            //payload.AddHeader("Accept", "*/*");

            // TODO: Add version number
            payload.AddHeader("User-Agent", "Partem" + Device.RuntimePlatform == Device.iOS ? "iOS" : "Android");
            payload.AddHeader("Content-Type", "application/json");
            payload.AddParameter("undefined", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(payload);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessagingCenter.Send<ApiAdapter, string>(this, "HttpError", response.Content);
            }

            return response.Content;

        }
    }

}
