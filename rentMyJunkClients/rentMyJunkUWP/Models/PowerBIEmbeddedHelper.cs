using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentMyJunkUWP.Models
{
    public class PowerBIEmbeddedHelper
    {
        public string WorkspaceCollectionName { get; set; }
        public string WorkspaceId { get; set; }
        public string AccessKey { get; set; }
        public string ReportId { get; set; }

        public string token1
        {
            get
            {
                return "{" +
                    "\"typ\":\"JWT\"," +
                    "\"alg\":\"HS256\"," +
                    "}";
            }
        }

        public string GetToken1()
        {
            return token1;
        }

        public string GetToken2(string WorkspaceId)
        {
            DateTime epoch = new DateTime(1970, 1, 1);
            DateTime now = DateTime.UtcNow;
            DateTime oneHour = now.AddHours(1);
            TimeSpan tnow = now - epoch;
            TimeSpan thour = oneHour - epoch;
            int startTime  = (int)tnow.TotalSeconds;
            int endTime = (int)thour.TotalSeconds;

            string token2Format = "{" +
                          "\"wid\":\"{0}\"," + // workspace id
                          "\"rid\":\"{1}\"," + // report id
                          "\"wcn\":\"{2}\"," + // workspace collection name
                          "\"iss\":\"PowerBISDK\"," +
                          "\"ver\":\"0.2.0\"," +
                          "\"aud\":\"https://analysis.windows.net/powerbi/api\"," +
                          "\"nbf\":{3}," + //Start Time
                          "\"exp\":{4}" +  //End Time
                      "}";

            string token2 = string.Format(token2Format, WorkspaceId, ReportId, WorkspaceCollectionName);

            return token2;

        }

        static string Base64Encode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Regular base64 encoder
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding
            return s;
        }

        static byte[] Base64Decode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        //private static byte[] HashHMAC(byte[] key, byte[] message)
        //{
        //    var hash = new HMACSHA256(key);
        //    return hash.ComputeHash(message);
        //}

        //public string GetAppToken()
        //{
        //    /*

        //    $inputval = rfc4648_base64_encode($token1) .
        //      "." .
        //      rfc4648_base64_encode($token2);

        //    // 3. get encoded signature
        //    $hash = hash_hmac("sha256",
        //        $inputval,
        //        $accesskey,
        //        true);
        //    $sig = rfc4648_base64_encode($hash);

        //    // 4. show result (which is the apptoken)
        //    $apptoken = $inputval . "." . $sig;
        //    echo($apptoken);

        //      */
        //}
    }
}
