using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace zserv
{
    class Program
    {
        const int port = 10000;
        static void Main()
        {
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
                Byte[] bytes = new Byte[256];
                String data = null;
                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");                    
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;                    
                    NetworkStream stream = client.GetStream();

                    int i;                    
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        
                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }
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
        }
    }
}
