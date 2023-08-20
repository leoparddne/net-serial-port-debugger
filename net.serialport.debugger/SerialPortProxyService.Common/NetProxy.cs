﻿using SerialPortProxyService.Common.Constant;
using SerialPortProxyService.Common.Helper;
using SerialPortProxyService.Common.Model;
using System.Net.Sockets;
using System.Text;

namespace SerialPortProxyService.Common
{
    public class NetProxy : IProxyBase
    {
        private NetProxyConfig netProxyConfig;
        private SocketHelper acceptSocketHelper;
        private Task task;
        private CancellationTokenSource cancellationToken = new();
        private SocketHelper clientSocketHelper = null;

        public RunningModeEnum RunningMode { get; set; }

        public Action<byte[]> Receive { get; set; }

        public NetProxy(Action<byte[]> receive)
        {
            Receive = receive;
        }

        public void Build(IProxyConfig config)
        {
            if (config is not NetProxyConfig netConfig)
            {
                throw new Exception("net error config");
            }

            netProxyConfig = netConfig;
        }

        public void Start()
        {
            if (netProxyConfig == null)
            {
                throw new Exception("net config is null");
            }
            acceptSocketHelper = new SocketHelper(netProxyConfig.Encode);
            acceptSocketHelper.ReceiveCallback = Receive;

            switch (RunningMode)
            {
                case RunningModeEnum.SerialPort:
                    task = Task.Run(() =>
                   {
                       StartClient(netProxyConfig.IP, netProxyConfig.Port);
                   }, cancellationToken.Token);
                    break;
                case RunningModeEnum.Net:
                    task = Task.Run(() =>
                   {
                       StartServer("0.0.0.0", netProxyConfig.Port);
                   }, cancellationToken.Token);
                    break;
                default:
                    throw new Exception("error running mode");
                    break;
            }

        }

        public void StartServer(string ip, int port)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var encode = System.Text.Encoding.GetEncoding("GB2312");

            acceptSocketHelper = new SocketHelper(encode);
            acceptSocketHelper.Bind(ip, port);

            acceptSocketHelper.Listen();

            while (true)
            {
                var clientSocket = acceptSocketHelper.Accept();
                clientSocketHelper = new SocketHelper(encode, clientSocket);

                Task.Run(() =>
                {
                    ReceiveSocketData(encode, clientSocketHelper);
                });
            }
        }

        private void ReceiveSocketData(Encoding encode, SocketHelper acceptSocketHelper)
        {
            while (acceptSocketHelper.ISConnect)
            {
                var buffer = new byte[4096];
                var size = acceptSocketHelper.Receive(buffer);
                if (size <= 0)
                {
                    continue;
                }

                var finalBuffer = new byte[size];

                buffer.ToList().CopyTo(0, finalBuffer, 0, size);

                Receive(finalBuffer);
            }
        }


        private void StartClient(string ip, int port)
        {
            SocketHelper socketHelper = new SocketHelper(netProxyConfig.Encode);
            socketHelper.Connect(ip, port);
            while (true)
            {
                var sendStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            cancellationToken.Cancel();
            acceptSocketHelper.Close();
            acceptSocketHelper.Dispose();

            clientSocketHelper?.Close();
            clientSocketHelper?.Dispose();
        }

        public void Send(byte[] data)
        {
            clientSocketHelper.Send(data);
        }
    }
}
