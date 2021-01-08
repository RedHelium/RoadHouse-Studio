using Newtonsoft.Json;
using RoadHouse_Studio.JSON_Objects;
using RoadHouse_Studio.Networking;
using RoadHouse_Studio.Networking.Requests;
using RoadHouse_Studio.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RoadHouse_Studio.Pages
{


    public partial class MainPage : Page
    {
        private string userID; //TODO: Replace in keys storage
        private Reward reward;

        public MainPage()
        {
            InitializeComponent();
        }


        private void GenerateLink_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UriResponseBuilder linkBuilder = new UriResponseBuilder(Strings.Twitch_OAuth_Protocol, Strings.Twitch_OAuth_Host, Strings.Twitch_OAuth_Path);
            Query linkData = new Query();
            Scopes linkScopes = new Scopes(); //TODO: Get from user selection

            linkScopes.Append("channel", "read", "redemptions");
            linkScopes.Append("channel", "manage", "redemptions");
            linkScopes.Append("channel", "read", "subscriptions", string.Empty);

            linkData.Init(new KeyValuePair<string, string>("response_type", "token"));
            linkData.Append('&', new KeyValuePair<string, string>("client_id", ClientID.Password));
            linkData.Append('&', new KeyValuePair<string, string>("redirect_uri", Strings.Redirect_URL));
            linkData.Append('&', new KeyValuePair<string, string>("scope", linkScopes.ToString()));

            linkBuilder.BuildUri(linkData);

            Process.Start(linkBuilder.ToString());

            //TODO: Make a new thread for this mehtod
            //InitServer(); 

        }

        private void InitServer()
        {
            TcpListener server = null;
            int port = 80;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                string data = string.Empty;
                // запуск слушателя
                server.Start();

                while (string.IsNullOrEmpty(data))
                {
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();


                    while (client.Available < 3) ; // match against "get"

                    byte[] bytes = new byte[client.Available];
                    stream.Read(bytes, 0, client.Available);
                    data = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(data);

                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }

        //TODO: Automate. Call validation method after traffic listen
        private void Validate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Validation();
        }

        private async void Validation()
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
                        JSON_Objects.ValidationResult validationResult = JsonConvert.DeserializeObject<JSON_Objects.ValidationResult>(result);
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

        private void CreateReward_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(Strings.Twitch_API_Channel_Points + userID);
            Content c = new Content();
            c.Start();
            c.Append(new KeyValuePair<string, object>("title", "TEST"));
            c.Append(new KeyValuePair<string, object>("cost", 50000), string.Empty);
            c.End();

            CreateRewardRequest(uri, Strings.Test_Client_ID, Strings.Test_Client_Secret, c, "application/json");
        }

        //TODO: Replace into another class
        private async void CreateRewardRequest(Uri uri, string clientID, string userToken, Content content, string type)
        {
            using (HttpClient client = new HttpClient())
            {
                using (CreateRewardRequest createRewardRequest = new CreateRewardRequest(uri, clientID, userToken, content, type))
                {
                    var response = await client.SendAsync(createRewardRequest.request);
                    string result = response.Content.ReadAsStringAsync().Result;

                    //TODO: Finish it
                    Reward rewardObj = JsonConvert.DeserializeObject<Reward>(result);
                    reward = rewardObj;

                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(result);
                    Console.WriteLine(reward.data.Count + " " + reward.data[0].id);
                }
            }
        }

        //TODO: Replace into another class
        private async void DeleteRewardRequest(Uri uri, string clientID, string userToken)
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

        private void DeleteReward_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(string.Concat(Strings.Twitch_API_Channel_Points, userID, "&id=", reward.data[0].id));
            DeleteRewardRequest(uri, Strings.Test_Client_ID, Strings.Test_Client_Secret);
        }

        //TODO: Replace into another class
        private async void GetRewardsRequest(Uri uri, string clientID, string userToken)
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
