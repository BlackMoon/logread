using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace zserv
{
    class Program
    {
        const int port = 10000;

        static void Main()
        {
            DB _db = new DB();

            #region TcpListener
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                server.Start();
                Console.WriteLine("Server started");
                Console.WriteLine("IP-address: 127.0.0.1");
                Console.WriteLine("Port: {0}", port);
                Console.WriteLine();
                // Buffer for reading data
                Byte[] bytes = new Byte[1024];
                String data = null;
                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    string[] pars;
                    int i;
                    NetworkStream stream = client.GetStream();
                    DateTime dt1 = DateTime.Now, 
                             dt2 = DateTime.Now;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data += System.Text.Encoding.GetEncoding(1251).GetString(bytes, 0, i);
                        if (!stream.DataAvailable) break;
                    }
                    pars = data.Split('\\');
                        
                    if (pars.Length > 1) DateTime.TryParse(pars[1], out dt1);
                    if (pars.Length > 2) DateTime.TryParse(pars[2], out dt2);
                        
                    XmlDocument xmldoc = _db.Query(pars[0], dt1, dt2);

                    StringWriter sw = new StringWriter();
                    XmlTextWriter tx = new XmlTextWriter(sw);
                    xmldoc.WriteTo(tx);

                        
                    data = sw.ToString();
                    bytes = System.Text.Encoding.GetEncoding(1251).GetBytes(data);
                    stream.Write(bytes, 0, bytes.Length);                        
                    
                    // Shutdown and end connection
                       
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
            #endregion
        }
    }
}
