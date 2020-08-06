using System;
using System.Timers;
using System.Windows.Forms;
using EchoVrRichPresence.Helpers;
using Timer = System.Timers.Timer;

namespace EchoVrRichPresence
{
    public class AppContext : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private Timer _timer;
        private RichPresenceHandler _richPresenceHandler;
        private QuestServiceClient _questServiceClient;

        public AppContext()
        {
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            _trayIcon = new NotifyIcon();
            _trayIcon.Icon = Properties.Resources.echo_icon;
            _trayIcon.ContextMenu = new ContextMenu(new MenuItem[]
                { exitMenuItem });
            _trayIcon.Visible = true;
            _trayIcon.ShowBalloonTip(2000, @"Starting client", @"Listening for Quest service data...", ToolTipIcon.None);

            StartDiscordClient();
        }

        private void StartDiscordClient()
        {
            _richPresenceHandler = new RichPresenceHandler();
            _questServiceClient = new QuestServiceClient();

            _timer = new Timer();
            _timer.Interval = 30000; // 30 seconds - no point in updating too frequently
            _timer.Elapsed += TryUpdateRichPresence;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private void TryUpdateRichPresence(object sender, ElapsedEventArgs args)
        {
            if (_questServiceClient.IsPlayingEchoVr())
            {
                _richPresenceHandler.Start(_trayIcon);
            }
            else
            {
                _richPresenceHandler.Stop(_trayIcon);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            _timer.Stop();
            _timer.Close();
            _timer.Dispose();
            
            Application.Exit();
        }
    }
}