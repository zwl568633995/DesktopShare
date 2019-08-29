using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mobwiz.DesktopShare
{
    public class HostCaster
    {
        public event EventHandler Stopped;
        public event EventHandler Started;

        private UdpClient _udpClient;
        private CancellationTokenSource _tokenSource;
        private bool _isCasting = false;
        private string _connectionString = "Stopped";

        public void Start()
        {
            var broadCastEp = new IPEndPoint(IPAddress.Broadcast, 29999);
            _udpClient = new UdpClient();
            _udpClient.EnableBroadcast = true;

            _tokenSource = new CancellationTokenSource();

            var task = new Task(() =>
            {
                while (!_tokenSource.IsCancellationRequested)
                {
                    var dataStr = $"{_isCasting}|{_connectionString}";
                    var buf = Encoding.UTF8.GetBytes(dataStr);

                    _udpClient.Send(buf, buf.Length, broadCastEp);
                    Thread.Sleep(3000);
                }

            }, _tokenSource.Token);

            task.ContinueWith(t =>
            {
                _udpClient.Close();
                _udpClient = null;
                OnStopped();
            });

            task.Start();
            OnStarted();
        }

        public void StartCast(string connStr)
        {
            _isCasting = true;
            _connectionString = connStr;
        }

        public void StopCast()
        {
            //_tokenSource.Cancel();
            _isCasting = false;
            _connectionString = "Stopped";
        }

        protected virtual void OnStopped()
        {
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }
    }
}
