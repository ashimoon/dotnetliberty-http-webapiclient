using System.IO;
using System.Net.Http;
using System.Text;

namespace DotNetLiberty.Http
{
    public class JsonContent : StringContent
    {
        private const string ApplicationJson = "application/json";

        public JsonContent(MemoryStream stream)
            : this(stream.ToArray())
        { }

        public JsonContent(byte[] bytes)
            : base(ToUtf8(bytes), Encoding.UTF8, ApplicationJson)
        { }

        private static string ToUtf8(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}