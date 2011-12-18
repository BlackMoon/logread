namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Барс.БраузерОтчетныхФорм.Properties;

    public class РедакторМаски : XtraForm
    {
        private ComboBoxEdit comboBoxEdit_ФильтрСтолбец;
        private IContainer components = null;
        private EmptySpaceItem emptySpaceItem1;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlItem layoutControlItem6;
        private LayoutControlItem layoutControlItem7;
        private RadioGroup radioGroup_УстановкаФильтра;
        private SimpleButton simpleButton_ОК;
        private SimpleButton simpleButton_Отмена;
        private SimpleButton simpleButton1;
        private TextEdit textEdit_СтрокаЦеликом;
        private TextEdit textEdit_ФильтрЗначение;
        private string началоВыражения = "";
        private string столбецОтбора = "";
        private string столбцыОтбора = "";
        private РедакторФормул текущийРедакторФормул = null;
        private string фильтр = "";

        public РедакторМаски(List<string> списокСтолбцов, РедакторФормул редакторФормул, string таблица, string столбец)
        {
            this.текущийРедакторФормул = редакторФормул;
            this.началоВыражения = "СуммаПоСубтаблице(" + таблица + "," + столбец;
            this.InitializeComponent();
            this.СформироватьСтроку();
            this.radioGroup_УстановкаФильтра.SelectedIndex = 0;
            this.comboBoxEdit_ФильтрСтолбец.Enabled = false;
            this.textEdit_ФильтрЗначение.Enabled = false;
            foreach (string str in списокСтолбцов)
            {
                this.comboBoxEdit_ФильтрСтолбец.Properties.Items.Add(str);
            }
        }

        private void comboBoxEdit_ФильтрСтолбец_SelectedValueChanged(object sender, EventArgs e)
        {
            this.столбецОтбора = this.comboBoxEdit_ФильтрСтолбец.SelectedItem.ToString();
            this.СформироватьСтроку();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(РедакторМаски));
            this.textEdit_СтрокаЦеликом = new TextEdit();
            this.layoutControl1 = new LayoutControl();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton_Отмена = new SimpleButton();
            this.radioGroup_УстановкаФильтра = new RadioGroup();
            this.simpleButton_ОК = new SimpleButton();
            this.textEdit_ФильтрЗначение = new TextEdit();
            this.comboBoxEdit_ФильтрСтолбец = new ComboBoxEdit();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.layoutControlItem5 = new LayoutControlItem();
            this.layoutControlItem6 = new LayoutControlItem();
            this.layoutControlGroup2 = new LayoutControlGroup();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.layoutControlItem7 = new LayoutControlItem();
            this.textEdit_СтрокаЦеликом.Properties.BeginInit();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.radioGroup_УстановкаФильтра.Properties.BeginInit();
            this.textEdit_ФильтрЗначение.Properties.BeginInit();
            this.comboBoxEdit_ФильтрСтолбец.Properties.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.layoutControlItem2.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.layoutControlItem5.BeginInit();
            this.layoutControlItem6.BeginInit();
            this.layoutControlGroup2.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.layoutControlItem4.BeginInit();
            this.layoutControlItem7.BeginInit();
            base.SuspendLayout();
            this.textEdit_СтрокаЦеликом.Location = new Point(7, 7);
            this.textEdit_СтрокаЦеликом.Name = "textEdit_СтрокаЦеликом";
            this.textEdit_СтрокаЦеликом.Properties.ReadOnly = true;
            this.textEdit_СтрокаЦеликом.Size = new Size(0x19d, 20);
            this.textEdit_СтрокаЦеликом.StyleController = this.layoutControl1;
            this.textEdit_СтрокаЦеликом.TabIndex = 0;
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.textEdit_СтрокаЦеликом);
            this.layoutControl1.Controls.Add(this.simpleButton_Отмена);
            this.layoutControl1.Controls.Add(this.radioGroup_УстановкаФильтра);
            this.layoutControl1.Controls.Add(this.simpleButton_ОК);
            this.layoutControl1.Controls.Add(this.textEdit_ФильтрЗначение);
            this.layoutControl1.Controls.Add(this.comboBoxEdit_ФильтрСтолбец);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x1aa, 0xb3);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            this.simpleButton1.Image = Resources.plus4;
            this.simpleButton1.Location = new Point(10, 0x71);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x1d, 0x18);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton_Отмена.DialogResult = DialogResult.Cancel;
            this.simpleButton_Отмена.Location = new Point(0x12e, 0x97);
            this.simpleButton_Отмена.Name = "simpleButton_Отмена";
            this.simpleButton_Отмена.Size = new Size(0x76, 0x16);
            this.simpleButton_Отмена.StyleController = this.layoutControl1;
            this.simpleButton_Отмена.TabIndex = 8;
            this.simpleButton_Отмена.Text = "Отмена";
            this.radioGroup_УстановкаФильтра.Location = new Point(7, 0x26);
            this.radioGroup_УстановкаФильтра.Name = "radioGroup_УстановкаФильтра";
            this.radioGroup_УстановкаФильтра.Properties.Columns = 2;
            this.radioGroup_УстановкаФильтра.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "Без отбора"), new RadioGroupItem(null, "Наложить фильтр") });
            this.radioGroup_УстановкаФильтра.Size = new Size(0x19d, 0x2b);
            this.radioGroup_УстановкаФильтра.StyleController = this.layoutControl1;
            this.radioGroup_УстановкаФильтра.TabIndex = 2;
            this.radioGroup_УстановкаФильтра.SelectedIndexChanged += new EventHandler(this.radioGroup_УстановкаФильтра_SelectedIndexChanged);
            this.simpleButton_ОК.Location = new Point(0xae, 0x97);
            this.simpleButton_ОК.Name = "simpleButton_ОК";
            this.simpleButton_ОК.Size = new Size(0x75, 0x16);
            this.simpleButton_ОК.StyleController = this.layoutControl1;
            this.simpleButton_ОК.TabIndex = 7;
            this.simpleButton_ОК.Text = "ОК";
            this.simpleButton_ОК.Click += new EventHandler(this.simpleButton_ОК_Click);
            this.textEdit_ФильтрЗначение.Location = new Point(0x123, 0x71);
            this.textEdit_ФильтрЗначение.Name = "textEdit_ФильтрЗначение";
            this.textEdit_ФильтрЗначение.Size = new Size(0x7e, 20);
            this.textEdit_ФильтрЗначение.StyleController = this.layoutControl1;
            this.textEdit_ФильтрЗначение.TabIndex = 1;
            this.textEdit_ФильтрЗначение.EditValueChanged += new EventHandler(this.textEdit_ФильтрЗначение_EditValueChanged);
            this.comboBoxEdit_ФильтрСтолбец.Location = new Point(0x67, 0x71);
            this.comboBoxEdit_ФильтрСтолбец.Name = "comboBoxEdit_ФильтрСтолбец";
            this.comboBoxEdit_ФильтрСтолбец.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit_ФильтрСтолбец.Size = new Size(0x7c, 20);
            this.comboBoxEdit_ФильтрСтолбец.StyleController = this.layoutControl1;
            this.comboBoxEdit_ФильтрСтолбец.TabIndex = 4;
            this.comboBoxEdit_ФильтрСтолбец.SelectedValueChanged += new EventHandler(this.comboBoxEdit_ФильтрСтолбец_SelectedValueChanged);
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem1, this.layoutControlItem2, this.emptySpaceItem1, this.layoutControlItem5, this.layoutControlItem6, this.layoutControlGroup2 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(0x1aa, 0xb3);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem1.Control = this.textEdit_СтрокаЦеликом;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(0x1a8, 0x1f);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = Locations.Left;
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem2.Control = this.radioGroup_УстановкаФильтра;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(0, 0x1f);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x1a8, 0x36);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = Locations.Left;
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(0, 0x90);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(0xa7, 0x21);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.layoutControlItem5.Control = this.simpleButton_ОК;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new Point(0xa7, 0x90);
            this.layoutControlItem5.MaxSize = new Size(0x80, 0x21);
            this.layoutControlItem5.MinSize = new Size(0x80, 0x21);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new Size(0x80, 0x21);
            this.layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextLocation = Locations.Left;
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.layoutControlItem6.Control = this.simpleButton_Отмена;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new Point(0x127, 0x90);
            this.layoutControlItem6.MaxSize = new Size(0x81, 0x21);
            this.layoutControlItem6.MinSize = new Size(0x81, 0x21);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new Size(0x81, 0x21);
            this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextLocation = Locations.Left;
            this.layoutControlItem6.TextSize = new Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            this.layoutControlGroup2.CustomizationFormText = "Фильтр";
            this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem7 });
            this.layoutControlGroup2.Location = new Point(0, 0x55);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new Size(0x1a8, 0x3b);
            this.layoutControlGroup2.Text = "Фильтр";
            this.layoutControlItem3.Control = this.comboBoxEdit_ФильтрСтолбец;
            this.layoutControlItem3.CustomizationFormText = "Столбец";
            this.layoutControlItem3.Location = new Point(40, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(0xbc, 0x23);
            this.layoutControlItem3.Text = "Столбец";
            this.layoutControlItem3.TextLocation = Locations.Left;
            this.layoutControlItem3.TextSize = new Size(0x30, 20);
            this.layoutControlItem4.Control = this.textEdit_ФильтрЗначение;
            this.layoutControlItem4.CustomizationFormText = "Значение";
            this.layoutControlItem4.Location = new Point(0xe4, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(190, 0x23);
            this.layoutControlItem4.Text = "Значение";
            this.layoutControlItem4.TextLocation = Locations.Left;
            this.layoutControlItem4.TextSize = new Size(0x30, 20);
            this.layoutControlItem7.Control = this.simpleButton1;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new Size(40, 0x23);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextLocation = Locations.Left;
            this.layoutControlItem7.TextSize = new Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1aa, 0xb3);
            base.Controls.Add(this.layoutControl1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "РедакторМаски";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Редактор маски";
            this.textEdit_СтрокаЦеликом.Properties.EndInit();
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.radioGroup_УстановкаФильтра.Properties.EndInit();
            this.textEdit_ФильтрЗначение.Properties.EndInit();
            this.comboBoxEdit_ФильтрСтолбец.Properties.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem1.EndInit();
            this.layoutControlItem2.EndInit();
            this.emptySpaceItem1.EndInit();
            this.layoutControlItem5.EndInit();
            this.layoutControlItem6.EndInit();
            this.layoutControlGroup2.EndInit();
            this.layoutControlItem3.EndInit();
            this.layoutControlItem4.EndInit();
            this.layoutControlItem7.EndInit();
            base.ResumeLayout(false);
        }

        private void radioGroup_УстановкаФильтра_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup_УстановкаФильтра.SelectedIndex == 0)
            {
                this.comboBoxEdit_ФильтрСтолбец.Enabled = false;
                this.textEdit_ФильтрЗначение.Enabled = false;
                this.simpleButton1.Enabled = false;
                this.фильтр = "";
                this.столбецОтбора = "";
                this.столбцыОтбора = "";
                this.СформироватьСтроку();
            }
            else
            {
                this.comboBoxEdit_ФильтрСтолбец.Enabled = true;
                this.textEdit_ФильтрЗначение.Enabled = true;
                this.simpleButton1.Enabled = true;
            }
        }

        private void simpleButton_ОК_Click(object sender, EventArgs e)
        {
            this.текущийРедакторФормул.ДобавитьПеременнуюКФормуле(this.textEdit_СтрокаЦеликом.Text);
            base.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.столбцыОтбора = this.столбцыОтбора + this.столбецОтбора + "&";
            this.столбецОтбора = this.comboBoxEdit_ФильтрСтолбец.SelectedItem.ToString();
            this.СформироватьСтроку();
        }

        private void textEdit_ФильтрЗначение_EditValueChanged(object sender, EventArgs e)
        {
            this.столбецОтбора = this.comboBoxEdit_ФильтрСтолбец.SelectedItem.ToString();
            this.фильтр = this.textEdit_ФильтрЗначение.Text;
            this.СформироватьСтроку();
        }

        private void СформироватьСтроку()
        {
            if (string.IsNullOrEmpty(this.фильтр) && string.IsNullOrEmpty(this.столбецОтбора))
            {
                this.textEdit_СтрокаЦеликом.Text = this.началоВыражения + ")";
            }
            else
            {
                this.textEdit_СтрокаЦеликом.Text = this.началоВыражения + "," + this.столбцыОтбора + this.столбецОтбора + "," + this.фильтр + ")";
            }
        }
    }
}

