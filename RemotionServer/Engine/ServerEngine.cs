using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RemotionServer.Engine;
using RemotionServer.Properties;

namespace Engine
{
    /// <summary>
    /// Udp-сервер
    /// </summary>
    internal sealed class ServerEngine : IDisposable
    {
        #region Static variables
        const char MOUSEMOVECOMMAND = 'r';
        public static event EventHandler serverOpenedSuccessfullyEvent;
        public static bool errorOccured;
        //Singleton
        private static ServerEngine serverEngine;
        public static double touchpadSens;
        /// <summary>
        /// Лист доступных IP адресов компьютера
        /// </summary>
        public static List<IPAddress> adressList
        {
            get
            {
                IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                return
                    ipHostEntry.AddressList.Where(adress => adress.AddressFamily == AddressFamily.InterNetwork)
                        .ToList();
            }
        }
        #endregion

        #region P/Invoke
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        #endregion

        #region Variables
        public int runCounter = 0;
        IPAddress ipAddress;
        UdpClient udpClient;
        Invoker invoker;
        public bool _IsRunning;
        /// <summary>
        /// Запущен ли сервер
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _IsRunning & udpClient != null & udpClient.Client != null & udpClient.Client.Connected;
            }
        }
        #endregion

        private ServerEngine()
        {
            invoker = new Invoker();
        }

        public static ServerEngine CreateServer()
        {
            if (serverEngine == null)
            {
                serverEngine = new ServerEngine() { _IsRunning = false };
            }
            return serverEngine;
        }

        public Task OpenConnection(IPAddress ipAdress)
        {
            errorOccured = false;
            ipAddress = ipAdress;
            runCounter++;

            return Task.Factory.StartNew(() =>
            {
                string data;
                byte[] bytes;
                IPEndPoint sender = null;

                try
                {
                    _IsRunning = true;
                    udpClient = new UdpClient(new IPEndPoint(ipAddress, 48856));
                }
                catch (SocketException)
                {
                    CloseConnection();
                    throw;
                }

                try
                {
                    serverOpenedSuccessfullyEvent?.Invoke(this, EventArgs.Empty);

                    while (true)
                    {
                        data = null;
                        bytes = udpClient.Receive(ref sender);
                        data += Encoding.UTF8.GetString(bytes);
                        char command = data[0];
                        data = data.Remove(0, 1);

                        if (command == MOUSEMOVECOMMAND)
                        {
                            string[] axis = data.Split(':');
                            int dx = Convert.ToInt32(axis[0]);
                            int dy = Convert.ToInt32(axis[1]);
                            mouse_event(0x0001, (int)(dx * touchpadSens), (int)(dy * touchpadSens), 0, 0);
                        }
                        else
                            invoker.Invoke(data, command);
                    }
                }
                catch (SocketException)
                {
                    //It's ok, using for immediate closing
                }
                finally
                {
                    Dispose();
                }
            });
        }

        public static Task<bool> PingServer()
        {
            return Task.Factory.StartNew<bool>(() =>
            {
                string[] serverList = { "google.com"};
                bool haveAnInternetConnection = false;
                Ping ping = new Ping();
                for (int i = 0; i < serverList.Length; i++)
                {
                    PingReply pingReply = ping.Send(serverList[i]);
                    haveAnInternetConnection = (pingReply.Status == IPStatus.Success);
                    if (haveAnInternetConnection)
                        break;
                }

                return haveAnInternetConnection;
            });
        }

        public void CloseConnection()
        {
            Dispose();
            if (errorOccured)
            {
                OpenConnection(ipAddress);
            }
        }

        public void Dispose()
        {
            if (udpClient != null)
                udpClient.Client.Close();
            _IsRunning = false;
        }
    }
}
