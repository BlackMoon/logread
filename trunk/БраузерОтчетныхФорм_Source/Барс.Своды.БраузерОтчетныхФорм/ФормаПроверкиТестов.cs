namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using Барс;

    public class ФормаПроверкиТестов : XtraForm
    {
        private IContainer components = null;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControl layoutControl_main;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private MemoEdit memoEdit_сообщения;
        private ProgressBarControl progressBarControl_общее;
        private ProgressBarControl progressBarControl_проверка;
        private SimpleButton simpleButton_ок;

        public ФормаПроверкиТестов()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ФормаПроверкиТестов));
            this.layoutControl_main = new LayoutControl();
            this.progressBarControl_проверка = new ProgressBarControl();
            this.progressBarControl_общее = new ProgressBarControl();
            this.simpleButton_ок = new SimpleButton();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.memoEdit_сообщения = new MemoEdit();
            this.layoutControlItem2 = new LayoutControlItem();
            this.layoutControl_main.BeginInit();
            this.layoutControl_main.SuspendLayout();
            this.progressBarControl_проверка.Properties.BeginInit();
            this.progressBarControl_общее.Properties.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.layoutControlItem4.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.memoEdit_сообщения.Properties.BeginInit();
            this.layoutControlItem2.BeginInit();
            base.SuspendLayout();
            this.layoutControl_main.Controls.Add(this.memoEdit_сообщения);
            this.layoutControl_main.Controls.Add(this.progressBarControl_проверка);
            this.layoutControl_main.Controls.Add(this.progressBarControl_общее);
            this.layoutControl_main.Controls.Add(this.simpleButton_ок);
            this.layoutControl_main.Dock = DockStyle.Fill;
            this.layoutControl_main.Location = new Point(0, 0);
            this.layoutControl_main.Name = "layoutControl_main";
            this.layoutControl_main.Root = this.layoutControlGroup1;
            this.layoutControl_main.Size = new Size(0x25d, 410);
            this.layoutControl_main.TabIndex = 0;
            this.layoutControl_main.Text = "layoutControl1";
            this.progressBarControl_проверка.Location = new Point(0x5d, 0x2f);
            this.progressBarControl_проверка.Name = "progressBarControl_проверка";
            this.progressBarControl_проверка.Properties.Step = 1;
            this.progressBarControl_проверка.Size = new Size(0x1fa, 0x1d);
            this.progressBarControl_проверка.StyleController = this.layoutControl_main;
            this.progressBarControl_проверка.TabIndex = 6;
            this.progressBarControl_общее.Location = new Point(0x5d, 7);
            this.progressBarControl_общее.Name = "progressBarControl_общее";
            this.progressBarControl_общее.Properties.Step = 1;
            this.progressBarControl_общее.Size = new Size(0x1fa, 0x1d);
            this.progressBarControl_общее.StyleController = this.layoutControl_main;
            this.progressBarControl_общее.TabIndex = 4;
            this.simpleButton_ок.Location = new Point(0x1f5, 0x17e);
            this.simpleButton_ок.Name = "simpleButton_ок";
            this.simpleButton_ок.Size = new Size(0x62, 0x16);
            this.simpleButton_ок.StyleController = this.layoutControl_main;
            this.simpleButton_ок.TabIndex = 7;
            this.simpleButton_ок.Text = "OK";
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem1, this.layoutControlItem3, this.layoutControlItem4, this.emptySpaceItem1, this.layoutControlItem2 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(0x25d, 410);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem1.Control = this.progressBarControl_общее;
            this.layoutControlItem1.CustomizationFormText = "Общий процесс";
            this.layoutControlItem1.Location = new Point(0, 0);
            this.layoutControlItem1.MaxSize = new Size(0, 40);
            this.layoutControlItem1.MinSize = new Size(0x93, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(0x25b, 40);
            this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Общий процесс";
            this.layoutControlItem1.TextLocation = Locations.Left;
            this.layoutControlItem1.TextSize = new Size(0x51, 20);
            this.layoutControlItem3.Control = this.progressBarControl_проверка;
            this.layoutControlItem3.CustomizationFormText = "Проверка теста";
            this.layoutControlItem3.FillControlToClientArea = false;
            this.layoutControlItem3.Location = new Point(0, 40);
            this.layoutControlItem3.MaxSize = new Size(0, 40);
            this.layoutControlItem3.MinSize = new Size(0x93, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(0x25b, 40);
            this.layoutControlItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "Проверка теста";
            this.layoutControlItem3.TextLocation = Locations.Left;
            this.layoutControlItem3.TextSize = new Size(0x51, 20);
            this.layoutControlItem4.Control = this.simpleButton_ок;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new Point(0x1ee, 0x177);
            this.layoutControlItem4.MaxSize = new Size(0x6d, 0);
            this.layoutControlItem4.MinSize = new Size(0x6d, 0x21);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(0x6d, 0x21);
            this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextLocation = Locations.Left;
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(0, 0x177);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(0x1ee, 0x21);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.memoEdit_сообщения.Location = new Point(7, 0x57);
            this.memoEdit_сообщения.Name = "memoEdit_сообщения";
            this.memoEdit_сообщения.Properties.Appearance.BackColor = SystemColors.ActiveCaptionText;
            this.memoEdit_сообщения.Properties.Appearance.Options.UseBackColor = true;
            this.memoEdit_сообщения.Properties.HideSelection = false;
            this.memoEdit_сообщения.Properties.ReadOnly = true;
            this.memoEdit_сообщения.Properties.WordWrap = false;
            this.memoEdit_сообщения.Size = new Size(0x250, 0x11c);
            this.memoEdit_сообщения.StyleController = this.layoutControl_main;
            this.memoEdit_сообщения.TabIndex = 8;
            this.layoutControlItem2.Control = this.memoEdit_сообщения;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(0, 80);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x25b, 0x127);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = Locations.Left;
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x25d, 410);
            base.Controls.Add(this.layoutControl_main);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ФормаПроверкиТестов";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Проверка тестов";
            this.layoutControl_main.EndInit();
            this.layoutControl_main.ResumeLayout(false);
            this.progressBarControl_проверка.Properties.EndInit();
            this.progressBarControl_общее.Properties.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem1.EndInit();
            this.layoutControlItem3.EndInit();
            this.layoutControlItem4.EndInit();
            this.emptySpaceItem1.EndInit();
            this.memoEdit_сообщения.Properties.EndInit();
            this.layoutControlItem2.EndInit();
            base.ResumeLayout(false);
        }

        private void ДобавитьСообщение(string Сообщение)
        {
            if (this.memoEdit_сообщения.InvokeRequired)
            {
                ОбновлениеОтладочнойИнформации method = new ОбновлениеОтладочнойИнформации(this.ДобавитьСообщение);
                base.Invoke(method, new object[] { Сообщение });
            }
            else
            {
                this.memoEdit_сообщения.Text = this.memoEdit_сообщения.Text + Сообщение + "\r\n";
                Application.DoEvents();
            }
        }

        public void НачатьВыводТеста(ПроверкаТестаФормы ПроверкаТеста)
        {
            this.progressBarControl_общее.PerformStep();
            this.progressBarControl_проверка.Properties.Maximum = ПроверкаТеста.КоличествоКомпонентовТеста;
            this.progressBarControl_проверка.Position = 0;
            ПроверкаТеста.ПриНачалеПроверкиКомпонентаТеста += new ОбрабтчикНачалаПроверкиКомпонентаТеста(this.ПроверкаТеста_ПриНачалеПроверкиКомпонентаТеста);
            ПроверкаТеста.ПриОкончанииПроверкиКомпонентаТеста += new ОбрабтчикОкончанияПроверкиКомпонентаТеста(this.ПроверкаТеста_ПриОкончанииПроверкиКомпонентаТеста);
        }

        public void ПоказатьФорму(int КоличествоТестов)
        {
            base.Show();
            this.progressBarControl_общее.Properties.Maximum = КоличествоТестов;
            this.progressBarControl_общее.Position = 0;
            this.progressBarControl_проверка.Position = 0;
            this.simpleButton_ок.Enabled = false;
        }

        private void ПроверкаТеста_ПриНачалеПроверкиКомпонентаТеста(ОписаниеТеста.КомпонентТестаФормы КомпонентТеста)
        {
            this.ДобавитьСообщение("----Тест компонента " + КомпонентТеста.Наименование + "----");
            this.progressBarControl_проверка.PerformStep();
        }

        private void ПроверкаТеста_ПриОкончанииПроверкиКомпонентаТеста(ОписаниеТеста.КомпонентТестаФормы КомпонентТеста, string Сообщение)
        {
            this.ДобавитьСообщение("----------");
            this.ДобавитьСообщение("");
        }

        private delegate void ОбновлениеОтладочнойИнформации(string text);
    }
}

