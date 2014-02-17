using System.Collections.Generic;

namespace Griffin.Net.Protocols.Http
{
    public interface IHeaderCollection : IEnumerable<KeyValuePair<string, string>>
    {
        int Count { get; }
        string this[string name] { get; set; }
        bool Contains(string name);
    }
}