using System.Configuration;
using System.Windows.Forms;
using DiscordRPC;

namespace EchoVrRichPresence.Helpers
{
    public class RichPresenceHandler
    {
        private string _detailsText;
        private string _stateText;
        private string _imageHoverText;
        private bool _showTrayPopup;

        private DiscordRpcClient _client;

        public RichPresenceHandler()
        {
            _detailsText = ConfigurationManager.AppSettings["DetailsText"];
            _stateText = ConfigurationManager.AppSettings["StateText"];
            _imageHoverText = ConfigurationManager.AppSettings["ImageHoverText"];
            _showTrayPopup = ConfigurationManager.AppSettings["ShowTrayPopup"].Equals("true");
        }

        public void Start(NotifyIcon trayIcon)
        {
            if (_client != null)
                return;

            _client = new DiscordRpcClient("740659413312077834");
            
            _client.Initialize();
            
            _client.SetPresence(new RichPresence
            {
                Details = _detailsText,
                State = _stateText,
                Assets = new Assets
                {
                    LargeImageKey = "icon1_big",
                    LargeImageText = _imageHoverText
                }
            });

            if (_showTrayPopup)
            {
                trayIcon.ShowBalloonTip(2000, @"EchoVR started!", @"Updating Discord Rich Presence", ToolTipIcon.None);
            }
        }

        public void Stop(NotifyIcon trayIcon)
        {
            if (_client == null)
                return;

            _client.Dispose();
            _client = null;

            if (_showTrayPopup)
            {
                trayIcon.ShowBalloonTip(2000, @"EchoVR stopped!", @"Updating Discord Rich Presence", ToolTipIcon.None);
            }
        }
    }
}