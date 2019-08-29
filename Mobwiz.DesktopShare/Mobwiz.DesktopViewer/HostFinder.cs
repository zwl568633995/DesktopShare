using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Mobwiz.DesktopViewer
{
    public class HostFoundEventArgs : EventArgs
    {
        public string ConnectionString { get; set; }
        public bool IsCasting { get; set; }

        public HostFoundEventArgs(bool isCasting, string connectionString)
        {
            IsCasting = isCasting;
            this.ConnectionString = connectionString;
        }
    }

    public class HostFinder
    {
        public event EventHandler<HostFoundEventArgs> HostFound;

        private UdpClient _udpClient = new UdpClient();
        private IPEndPoint _remotEndPoint = new IPEndPoint(IPAddress.Broadcast, 29999);
        private bool _found = false;
        private bool _isCasting = false;
        private CancellationTokenSource _tokenSource;

        public void StartFind()
        {
            _udpClient = new UdpClient(29999);
            _tokenSource = new CancellationTokenSource();

            var task = new Task(() =>
            {
                while (!_tokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var data = _udpClient.Receive(ref _remotEndPoint);
                        var str = Encoding.UTF8.GetString(data);

                        var vals = str.Split('|');
                        if (vals.Length != 2)
                            continue;

                        bool isCasting = false;
                        if (bool.TryParse(vals[0], out isCasting))
                        {
                            OnHostFound(new HostFoundEventArgs(isCasting, vals[1]));
                        }
                    }
                    catch (SocketException ex) // retry！
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

                _udpClient.Close();
                _udpClient = null;

            }, _tokenSource.Token);

            task.Start();
        }


        protected virtual void OnHostFound(HostFoundEventArgs e)
        {
            HostFound?.Invoke(this, e);
        }
    }
}
