using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace zclient
{
    public partial class Form1 : Form
    {
        const int port = 10000;

        public Form1()
        {
            InitializeComponent();
            dtIntervalFrom.Value = DateTime.Now.AddMonths(-1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient;
            NetworkStream networkStream;
            StreamReader streamReader;
            StreamWriter streamWriter;

            try
            {
                tcpClient = new TcpClient("localhost", 10000);

                networkStream = tcpClient.GetStream();

                streamReader = new StreamReader(networkStream);
                streamWriter = new StreamWriter(networkStream);

                // Передали серверу сообщение
                streamWriter.WriteLine("Client: Ready");
                streamWriter.Flush();
                // Приняли сообщение от сервера
                rtxtResult.Text = streamReader.ReadLine();
                tcpClient.Close();
            }
            catch (SocketException exc)
            {
                rtxtResult.Text += "Ошибка: " + exc;
            }
        }
               
    }
}
