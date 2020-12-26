
namespace RoadHouse_Studio.Networking
{
    /// <summary>
    /// Scopes for using Twitch API
    /// </summary>
    public struct Scopes
    {
        private string scopes;

        public void Append(string type, string func, string property, string delimiter = " ")
        => scopes += string.Concat(type, ':', func, ':', property, delimiter);

        public override string ToString() => scopes;
    }
}
