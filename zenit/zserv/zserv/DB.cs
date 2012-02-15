using System;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Xml;

namespace zserv
{
    class DB
    {
        const string conn = "Database=zenitdb;Data Source=178.170.147.8;User Id=zenit;Password=zenit";
        
        public DB()
        {               
            
        }

        public bool open()
        {            
            return true;
        }

        public XmlDocument Query(string FIO, DateTime dateFrom, DateTime dateTo)
        {
            string[] fio = FIO.Split(' ');
            string CommandText = "getOrders";
            string val;

            MySqlDataReader MyDataReader;
            MySqlConnection _myConnection = new MySqlConnection(conn);
            MySqlCommand myCommand = new MySqlCommand(CommandText, _myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;
            
            myCommand.Parameters.Add("surname", MySqlDbType.VarChar, 50);
            myCommand.Parameters["surname"].Direction = ParameterDirection.Input;
            myCommand.Parameters["surname"].Value = fio[0];
                        
            myCommand.Parameters.Add("name", MySqlDbType.VarChar, 50);
            myCommand.Parameters["name"].Direction = ParameterDirection.Input;

            if (fio.Length > 1) val = fio[1];
            else val = "%";
            myCommand.Parameters["name"].Value = val;          

            myCommand.Parameters.Add("patronymic", MySqlDbType.VarChar, 50);
            myCommand.Parameters["patronymic"].Direction = ParameterDirection.Input;
            
            if (fio.Length > 2) val = fio[2];
            else val = "%";            
            myCommand.Parameters["patronymic"].Value = val;

            myCommand.Parameters.Add("dateFrom", MySqlDbType.Date);
            myCommand.Parameters["dateFrom"].Direction = ParameterDirection.Input;
            myCommand.Parameters["dateFrom"].Value = dateFrom;

            myCommand.Parameters.Add("dateTo", MySqlDbType.Date);
            myCommand.Parameters["dateTo"].Direction = ParameterDirection.Input;
            myCommand.Parameters["dateTo"].Value = dateTo;

            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldec = xmldoc.CreateXmlDeclaration("1.0", null, null);
            xmldoc.AppendChild(xmldec);
            XmlElement orders = xmldoc.CreateElement("orders");
           
            try
            {
                _myConnection.Open();  
                MyDataReader = myCommand.ExecuteReader();
                myCommand.Dispose();

                if (MyDataReader.HasRows)
                {
                    XmlElement order, elem;
                    int i = 0;                    
                    DateTime dt;
                    float price;

                    while (MyDataReader.Read())
                    {
                        i++;                        
                        order = xmldoc.CreateElement("order");
                        order.InnerText = i.ToString();

                        elem = xmldoc.CreateElement("surname");
                        elem.InnerText = MyDataReader.GetString("surname");
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("name");
                        elem.InnerText = MyDataReader.GetString("name");
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("patronymic");
                        elem.InnerText = MyDataReader.GetString("patronymic");
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("price");
                        price = MyDataReader.GetFloat("price");                        
                        elem.InnerText = string.Format("{0:f2}", price);
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("orderdate");
                        dt = MyDataReader.GetDateTime("orderdate");                        
                        elem.InnerText = dt.ToShortDateString();
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("cashiersurname");
                        elem.InnerText = MyDataReader.GetString("cashiersurname");
                        order.AppendChild(elem);

                        elem = xmldoc.CreateElement("cashiername");
                        elem.InnerText = MyDataReader.GetString("cashiername");
                        order.AppendChild(elem);                        

                        orders.AppendChild(order);
                    }
                }                
            }            
            catch (Exception exc)
            {                
            }
            _myConnection.Close();           
           
            xmldoc.AppendChild(orders);            
            return xmldoc;
        }        
    }
}
