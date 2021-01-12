using System.Collections.Generic;

namespace RoadHouse_Studio.Networking
{
    /// <summary>
    /// Query string for Uri
    /// </summary>
    public struct Query
    {
        private string query;

        /// <summary>
        /// Initialize first parameter
        /// </summary>
        /// <param name="value"></param>
        public void Init(string key, string value)
        => query = string.Concat(key, '=', value);

        public void Append(char symbol, string key, string value)
        => query += string.Concat(symbol, key, '=', value);

        public override string ToString() => query;

    }
}
