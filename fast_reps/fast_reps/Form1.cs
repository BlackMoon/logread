using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace fast_reps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.__2010__' table. You can move, or remove it, as needed.
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._2010__TableAdapter.Fill(this.dataSet1.@__2010__);
            
            
            if (report1.Prepare()) report1.ShowPrepared();
        }
    }
}