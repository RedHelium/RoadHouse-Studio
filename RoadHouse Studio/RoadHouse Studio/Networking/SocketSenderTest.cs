
using Newtonsoft.Json;
using RoadHouse_Studio.Resources;
using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace RoadHouse_Studio.Networking
{
    public sealed class SocketSenderTest
    {
        public void TestConnection()
        {
            JSONLogin jsonLogin = new JSONLogin();
            JSONTest jt = new JSONTest();

            jt.type = "PING";

            jsonLogin.type = "LISTEN";
            jsonLogin.nonce = "44h1k13746815ab1r2";
            jsonLogin.data = new JSONLoginData();
            jsonLogin.data.auth_token = "cfabdegwdoklmawdzdo98xt2fo512y";
            jsonLogin.data.topics.Add("channel-bits-events-v1.148844345");

            string json = JsonConvert.SerializeObject(jsonLogin, Formatting.Indented);
            string t = JsonConvert.SerializeObject(jt, Formatting.Indented);

            WebSocket socket = new WebSocket(Strings.Test_Server_URL);
      
            socket.OnMessage += Socket_OnMessage;
            socket.OnOpen += Socket_OnOpen;
            socket.Connect();
            socket.Send(t);
            
        }

        private void Socket_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("Connection is Open");
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }

    [Serializable]
    public sealed class JSONTest
    {
        [JsonProperty("type")]
        public string type { get; set; }
    }

    [Serializable]
    public sealed class JSONLoginData
    {
        [JsonProperty("topics")]
        public List<string> topics { get; set; } = new List<string>();
        [JsonProperty("auth_token")]
        public string auth_token { get; set; }
    }

    [Serializable]
    public sealed class JSONLogin
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("nonce")]
        public string nonce { get; set; }
        [JsonProperty("data")]
        public JSONLoginData data { get; set; }
    }


}
