using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bars_license
{
    public partial class Form1 : Form
    {
        //string key1 = "Ключ: Основной\r\nПродукт: ПК БАРС.Web-Своды (Мониторинг образования)\r\nУчреждение: Партнерский Ключ (не для коммерческого использования)\r\nИНН: 9999999999\r\nКПП: 555555555\r\nДата окончания: 31.12.2010\r\nКомплектация: ПРОФ\r\nБэк-Офис: 10\r\nФронт-Офис: 70\r\nКоличество учреждений:90\r\nКод учреждения: 6fee2b53d7ab";
        string key1 = "Ключ: Основной\r\nПродукт: БАРС.ЭПК\r\nУчреждение: Администрация Ближнеосиновского сельского поселения Суровикинского района\r\nИНН: 3430008173\r\nКПП: 343001001\r\nДата окончания: 27.08.2010\r\nБэк-Офис: 0\r\nФронт-Офис: 1\r\nКоличество учреждений:1\r\nКод учреждения: ea8048208d4a";
        string key2 = "dpX6r+5yHULhxadE+mzaQ0B62HzYaLfh912nwCtF4PVwEKFWfmQQMXuFlRHJT4Yi1dcDRIFJlEGsGgELHw3t2lK0iXZ6qI+QkJDa6dNQBKampgOhOKSuS7wM+pB5a77XzClYclhlwtxzZMUdxa4nuQ+tSX88aie5IVIpTjx3IziBMe61X17aKRU7GFZ+fvrOiVBLjcs1eHRNIx0R2CBxVUC0gUsq8fPKwzCbUfjdpmyagftpUd3NckRahImPoFkZIns0F9jTxH30yzCiHFZG0eHO7E/U3Q9Peax2DLm0WpofAMAM9C7HRI4+";
        //string key3 = "БАРС.Web-Своды";
        string key3 = "БАРС.ЭПК";

        //[DllImport(@"d:\\dev\bars_license\rkey32.dll", EntryPoint = "a", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
        //public static extern string a(string a_0, string a_1);


        //[DllImport(@"d:\\dev\bars_license\rkey32.dll", EntryPoint = "x", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
       // public static extern int x();

        [DllImport(@"d:\\dev\rkey32\debug\rkey32.dll", EntryPoint = "a1", CharSet = CharSet.Auto)]
        public static extern string a1(string a_0, string a_1);

        public Form1()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = a1(key1, key3);
           
            //string s = a(key1, key3);
            bool b = a.a_(key1, s, key3); 
            //MessageBox.Show(s);
            if (b) MessageBox.Show("OK");
        }  
    }   
}