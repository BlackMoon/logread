using System;
using System.Collections.Generic;
using System.Data;
using System.Data.DBF;
using System.IO;
using System.Text;

namespace skl_upd
{
    class Program
    {

        private Dictionary<string, IDataRecord> ReadData(DBFReader dbfr, List<string> keys)
        {
            string key;
            IDataRecord irow;
            Dictionary<string, IDataRecord> data = new Dictionary<string, IDataRecord>();            
            
            for (int i = 0; i < dbfr.Count; i++)
            {
                irow = (IDataRecord)dbfr[i];
                key = ((DBFRow)irow).GetKey(keys.ToArray());
                data.Add(key, irow);
            }
            return data;
        }
        
        static void Main(string[] args)
        {
            
            
            // считывание настроек
            //for (i = 0; i < n; i++)

            string path1 = "D:\\skl\\3",
                   path2 = "D:\\skl\\1";
            
            string file1 = Path.Combine(path1, "TOVAR.DBF"),
                   file2 = Path.Combine(path2, "TOVAR.DBF");

            DBFReader dbfr1 = new DBFReader(file1), 
                      dbfr2 = new DBFReader(file2); 

            if (dbfr1.Open())
            {
                int i;
                #region ключевые поля
                DBFColumn col;
                List<string> keys = new List<string>();
               
                for (i = 0; i < 2; i++)
                {
                    col = dbfr1.GetColumn(i);
                    keys.Add(col.ColumnName);
                }
                #endregion

                Dictionary<string, byte[]> data1 = dbfr1.ReadData(keys), data2 = null;                
                Directory.CreateDirectory(path2); 
                // файла нет
                if (!File.Exists(file2)) File.Copy(file1, file2);
                else
                {
                    if (dbfr2.Open()) data2 = dbfr2.ReadData(keys);
                }
               
                #region синхронизация
               
                string key;
                byte[] buf1, buf2;
                object obj1, obj2;
                
                foreach (KeyValuePair<string, byte[]> kvp in data1)
                {
                    key = kvp.Key;
                    buf1 = kvp.Value;

                    char[]  c = dbfr1.Encoding.GetChars(buf1);

                    if (data2.ContainsKey(key))
                    {
                        buf2 = data2[key];

                        for (i = 2; i < dbfr2.ColumnCount; i++)
                        {
                            col = dbfr2.GetColumn(i);

                            obj1 = buf1[col.Size];
                            obj2 = buf2[col.Size];
                            //if (T == typeof(int))  = "System.Convert.ToInt32(row1[i])";
                        
                        }
     
                    }
                    else data2.Add(key, buf1);
                }
                #endregion

                data1.Clear();
                data2.Clear();
                keys.Clear();
                
            }
            dbfr1.Close();
            dbfr2.Close();

            dbfr1 = null;
            dbfr2 = null;
        }
    }
}
