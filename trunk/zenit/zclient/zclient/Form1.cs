using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text; 
using System.Windows.Forms;
using System.Web;
using System.Xml;

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
            NetworkStream stream;
            
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1251);
            string data = null;
            
            try
            {
                if (txtClient.Text.Length == 0) 
                    throw new System.ArgumentException("Не заполнено поле ФИО клиента", "original");

                tcpClient = new TcpClient("localhost", 10000);
                stream = tcpClient.GetStream();
                
                data = txtClient.Text + '\\' + dtIntervalFrom.Value.ToShortDateString() + '\\' + dtIntervalTo.Value.ToShortDateString();
                Byte[] bytes = System.Text.Encoding.GetEncoding(1251).GetBytes(data);
                stream.Write(bytes, 0, bytes.Length);

                int i;
                data = null;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data += System.Text.Encoding.GetEncoding(1251).GetString(bytes, 0, i);
                    if (!stream.DataAvailable) break;
                }                
                // Приняли сообщение от сервера
                StringBuilder output = new StringBuilder();
                using (XmlReader reader = XmlReader.Create(new StringReader(data)))
                {
                    XmlWriterSettings ws = new XmlWriterSettings();
                    ws.Indent = true;
                    using (XmlWriter writer = XmlWriter.Create(output, ws))
                    {
                        // Parse the file and display each of the nodes.
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case XmlNodeType.Text:
                                    writer.WriteString(reader.Value);
                                    break;
                                case XmlNodeType.XmlDeclaration:
                                case XmlNodeType.ProcessingInstruction:
                                    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                    break;
                                case XmlNodeType.Comment:
                                    writer.WriteComment(reader.Value);
                                    break;
                                case XmlNodeType.EndElement:
                                    writer.WriteFullEndElement();
                                    break;
                            }
                        }

                    }                  
                }

                rtxtResult.Text = output.ToString(); 
                tcpClient.Close();
            }
            catch (Exception exc)
            {
                rtxtResult.Text = "Ошибка : " + exc.Message;
            }            
        }
               
    }
}
