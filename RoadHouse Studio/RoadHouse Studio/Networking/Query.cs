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
        public void Init(KeyValuePair<string, string> value)
        => query = string.Concat(value.Key, '=', value.Value);

        public void Append(char symbol, KeyValuePair<string, string> value)
        => query += string.Concat(symbol, value.Key, '=', value.Value);

        public override string ToString() => query;

    }
}
