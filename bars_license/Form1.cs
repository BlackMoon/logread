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
        //string key1 = "����: ��������\r\n�������: �� ����.Web-����� (���������� �����������)\r\n����������: ����������� ���� (�� ��� ������������� �������������)\r\n���: 9999999999\r\n���: 555555555\r\n���� ���������: 31.12.2010\r\n������������: ����\r\n���-����: 10\r\n�����-����: 70\r\n���������� ����������:90\r\n��� ����������: 6fee2b53d7ab";
        string key1 = "����: ��������\r\n�������: ����.���\r\n����������: ������������� ����������������� ��������� ��������� �������������� ������\r\n���: 3430008173\r\n���: 343001001\r\n���� ���������: 27.08.2010\r\n���-����: 0\r\n�����-����: 1\r\n���������� ����������:1\r\n��� ����������: ea8048208d4a";
        string key2 = "dpX6r+5yHULhxadE+mzaQ0B62HzYaLfh912nwCtF4PVwEKFWfmQQMXuFlRHJT4Yi1dcDRIFJlEGsGgELHw3t2lK0iXZ6qI+QkJDa6dNQBKampgOhOKSuS7wM+pB5a77XzClYclhlwtxzZMUdxa4nuQ+tSX88aie5IVIpTjx3IziBMe61X17aKRU7GFZ+fvrOiVBLjcs1eHRNIx0R2CBxVUC0gUsq8fPKwzCbUfjdpmyagftpUd3NckRahImPoFkZIns0F9jTxH30yzCiHFZG0eHO7E/U3Q9Peax2DLm0WpofAMAM9C7HRI4+";
        //string key3 = "����.Web-�����";
        string key3 = "����.���";

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