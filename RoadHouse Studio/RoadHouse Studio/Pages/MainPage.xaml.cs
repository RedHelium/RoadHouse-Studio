using RoadHouse_Studio.JSON_Objects;
using RoadHouse_Studio.Networking;
using RoadHouse_Studio.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
        private RequestsSender requestsSender;
        private KeysStorage keysStorage; //TODO: Init this class in MainWindow class

        public MainPage()
        {
            InitializeComponent();

            requestsSender = new RequestsSender(keysStorage);
        }


        private void GenerateLink_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Process.Start(GenerateAccessTokenURI());

            //TODO: Make a new thread for this mehtod
            //InitServer(); 

        }

        private string GenerateAccessTokenURI()
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

            return linkBuilder.ToString();
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
            requestsSender.Validation();
        }

        private void CreateReward_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(Strings.Twitch_API_Channel_Points + userID);
            Content c = new Content();
            c.Start();
            c.Append(new KeyValuePair<string, object>("title", "TEST"));
            c.Append(new KeyValuePair<string, object>("cost", 50000), string.Empty);
            c.End();

            requestsSender.CreateRewardRequest(uri, Strings.Test_Client_ID, Strings.Test_Client_Secret, c, "application/json");
        }

        private void DeleteReward_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(string.Concat(Strings.Twitch_API_Channel_Points, userID, "&id=", reward.data[0].id));
            requestsSender.DeleteRewardRequest(uri, Strings.Test_Client_ID, Strings.Test_Client_Secret);
        }
    }
}
