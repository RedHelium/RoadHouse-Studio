

namespace RoadHouse_Studio.Networking
{
    //TODO: Storage client keys and save/load them into a file. Crypt the keys
    public sealed class KeysStorage
    {
        public string userID { get; private set; }
        public string clientID { get; private set; }
        public string userToken { get; private set; }

        public void SetID(string id) => userID = id;
    }
}
