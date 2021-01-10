using Newtonsoft.Json;
using RoadHouse_Studio.JSON_Objects;
using RoadHouse_Studio.Networking.Requests;
using RoadHouse_Studio.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace RoadHouse_Studio.Networking
{
    public sealed class RequestsSender
    {

        public async void Validation()
        {
            Uri validationURI = new Uri(Strings.Twitch_OAuth_URI);

            using (HttpClient client = new HttpClient())
            {
                using (ValidationRequest validationRequest = new ValidationRequest(validationURI, Strings.Test_Client_Secret))
                {
                    var response = await client.SendAsync(validationRequest.request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ValidationResult validationResult = JsonConvert.DeserializeObject<ValidationResult>(result);
                        userID = validationResult.user_id;
                        Console.WriteLine(result);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка валидации, статус ошибки: \n" + response.Content.ReadAsStringAsync().Result);
                        userID = string.Empty;
                    }
                }
            }
        }
        public async void CreateRewardRequest(Uri uri, string clientID, string userToken, Content content, string type)
        {
            using (HttpClient client = new HttpClient())
            {
                using (CreateRewardRequest createRewardRequest = new CreateRewardRequest(uri, clientID, userToken, content, type))
                {
                    var response = await client.SendAsync(createRewardRequest.request);
                    string result = response.Content.ReadAsStringAsync().Result;

                    //TODO: Added reward in list for save/load into a file
                    Reward reward = JsonConvert.DeserializeObject<Reward>(result);

                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(result);
                }
            }
        }

        public async void DeleteRewardRequest(Uri uri, string clientID, string userToken)
        {
            using (HttpClient client = new HttpClient())
            {
                using (DeleteRewardRequest deleteRewardRequest = new DeleteRewardRequest(uri, clientID, userToken))
                {
                    var response = await client.SendAsync(deleteRewardRequest.request);
                    Console.WriteLine(response.StatusCode);
                }
            }
        }

        public async void GetRewardsRequest(Uri uri, string clientID, string userToken)
        {
            using (HttpClient client = new HttpClient())
            {
                using (GetRewardsRequest getRewardsRequest = new GetRewardsRequest(uri, clientID, userToken))
                {
                    var response = await client.SendAsync(getRewardsRequest.request);
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(response.StatusCode);
                }
            }
        }
    }
}
