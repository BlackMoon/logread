using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.DBF;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBFReader dbf = new DBFReader("d:\\skl\\3\\TOVAR.DBF");
            dbf.Open();

            DBFColumn s = null;
            List<string> keys = new List<string>();
            int i;
            for (i = 0; i < 2; i++)
            {
                 s = dbf.GetColumn(i);
                 keys.Add(s.ColumnName);
            }

            IDataRecord irow;
            string key;           

            Dictionary<string, IDataRecord> data = new Dictionary<string, IDataRecord>();
            for (i = 0; i < dbf.Count; i++)
            {                
                irow = (IDataRecord)dbf[i];
                key = ((DBFRow)irow).GetKey(keys.ToArray());
                data.Add(key, irow);
            }

            dbf.Close();
            
        }
    }
}