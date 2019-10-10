using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace System
{
    public class FcmMessage
    {
        public string to { get; set; }
        public Notification notification { get; set; }
        public Dictionary<string, string> data { get; set; } = new Dictionary<string, string>();
    }
    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
        public string sound = "default";
    }

    public class Fcm
    {
        private string ServerKey { get { return ""; } }

        public async Task<HttpResponseMessage> Send(Notification notification, Dictionary<string, string> Data)
        {
            Dictionary<string, string> CustomDataPassedToFCM = new Dictionary<string, string>();
            CustomDataPassedToFCM.Add("title", notification.title);
            CustomDataPassedToFCM.Add("body", notification.body);
            CustomDataPassedToFCM.Add("click_action", "FLUTTER_NOTIFICATION_CLICK");
            foreach (var data in Data)
            {
                CustomDataPassedToFCM.Add(data.Key,data.Value);
            }

            var messageInformation = new FcmMessage()
            {
                to = "/topics/all",
                notification = notification,
                data = CustomDataPassedToFCM
            };
            string jsonMessage = JsonConvert.SerializeObject(messageInformation);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            using (var client = new HttpClient()) { return await client.SendAsync(request); }


        }
    }
}