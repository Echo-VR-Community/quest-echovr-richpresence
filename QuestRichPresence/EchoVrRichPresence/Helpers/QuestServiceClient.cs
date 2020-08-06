using System;
using System.Configuration;
using System.Net.Http;
using EchoVrRichPresence.Models;
using Newtonsoft.Json;

namespace EchoVrRichPresence.Helpers
{
    public class QuestServiceClient
    {
        private string _echoVrAppName;

        private HttpClient _client;

        public bool IsPlayingEchoVr()
        {
            if (_client == null)
            {
                InitializeHttpClient();
            }

            if (string.IsNullOrEmpty(_echoVrAppName))
            {
                _echoVrAppName = ConfigurationManager.AppSettings["EchoVrPartialAppName"];
            }

            try
            {
                var questDataString = _client.GetStringAsync("/").Result;
                if (string.IsNullOrEmpty(questDataString))
                    return false;

                var questData = JsonConvert.DeserializeObject<QuestServiceData>(questDataString);
                if (questData.AppName.StartsWith(_echoVrAppName))
                    return true;
            }
            catch (Exception)
            {
                //Do nothing
            }

            return false;
        }

        private void InitializeHttpClient()
        {
            var questIp = ConfigurationManager.AppSettings["QuestIpAddress"];
            _client = new HttpClient(new HttpClientHandler());
            _client.BaseAddress = new Uri($"http://{questIp}:8080"); //Quest service listening port
            _client.Timeout = new TimeSpan(0, 0, 0, 0, 2000); //2s timeout in case of network/quest lag
        }
    }
}