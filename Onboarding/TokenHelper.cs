using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Onboarding
{
    public class TokenHelper
    {
        public TokenHelper ()
        {

        }

        public string Generate<T>(T obj)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            byte[] bytes = Encoding.ASCII.GetBytes(json);
            string token = Convert.ToBase64String(bytes.ToArray());

            return token;
        }

        public T GetObject<T> (string token)
        {
            byte[] data = Convert.FromBase64String(token);
            string enrollmentTokenJson = Encoding.ASCII.GetString(data);
            return JsonConvert.DeserializeObject<T>(enrollmentTokenJson);
        }
    }
}
