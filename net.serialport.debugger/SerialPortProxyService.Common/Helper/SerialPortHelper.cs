using System;
using System.IO.Ports;
using System.Text;

namespace SerialPortProxyService.Common.Helper
{
    public class SerialPortHelper : IDisposable
    {
        private bool disposedValue;

        private SerialPort SerialPort { get; set; }

        public delegate void DataReceivedHandle(byte[] byteData);

        public event DataReceivedHandle DataReceivedEvent;


        public bool ISOpen
        {
            get
            {
                return SerialPort?.IsOpen ?? false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public SerialPortHelper(Encoding encode, string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            SerialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            //SerialPort.Encoding = Encoding.UTF8;
            SerialPort.Encoding = encode;
            SerialPort.DataReceived += SerialPort_DataReceived;
            SerialPort.ErrorReceived += SerialPort_ErrorReceived;
            SerialPort.PinChanged += SerialPort_PinChanged;
        }


        /// <summary>
        /// 串口号发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            //TODO
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            //TODO
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[SerialPort.ReadBufferSize];
            SerialPort.Read(readBuffer, 0, readBuffer.Length);
            //string data = Encoding.Default.GetString(readBuffer);

            DataReceivedEvent.Invoke(readBuffer);
        }


        public void Send(byte[] buffer, int offset, int count)
        {
            SerialPort.Write(buffer, offset, count);
        }


        public void Open()
        {
            SerialPort.Open();
        }

        public void Close()
        {
            SerialPort.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }

            SerialPort.Dispose();
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~SerialPortHelper()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
