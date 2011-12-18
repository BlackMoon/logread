using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Барс.Клиент.СообщениеКлиенту
{
    public partial class ОкноСообщенияЧерезБД : DevExpress.XtraEditors.XtraForm
    {
        public ОкноСообщенияЧерезБД()
            :base()
        {
            InitializeComponent();
        }

        public ОкноСообщенияЧерезБД(string текстСообщения)
            :this()
        {
            this.текстСообщения.Text = текстСообщения;
        }

        public string ТекстСообщения
        {
            get 
            {
                return this.текстСообщения.Text;
            }
            set 
            {
                this.текстСообщения.Text = value;
            }
        }

        public string[] МассивСтрок
        {
            get 
            {
                return this.текстСообщения.Lines;
            }
            set
            {
                this.текстСообщения.Lines = value;
            }
        }
    }
}