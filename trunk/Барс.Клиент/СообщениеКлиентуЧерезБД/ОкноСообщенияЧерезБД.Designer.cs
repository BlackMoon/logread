namespace Барс.Клиент.СообщениеКлиенту
{
    partial class ОкноСообщенияЧерезБД
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ОкноСообщенияЧерезБД));
            this.кнопкаЗакрыть = new DevExpress.XtraEditors.SimpleButton();
            this.текстСообщения = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.текстСообщения.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // кнопкаЗакрыть
            // 
            this.кнопкаЗакрыть.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.кнопкаЗакрыть.Location = new System.Drawing.Point(493, 201);
            this.кнопкаЗакрыть.Name = "кнопкаЗакрыть";
            this.кнопкаЗакрыть.Size = new System.Drawing.Size(75, 23);
            this.кнопкаЗакрыть.TabIndex = 0;
            this.кнопкаЗакрыть.Text = "Закрыть";
            // 
            // текстСообщения
            // 
            this.текстСообщения.Location = new System.Drawing.Point(12, 31);
            this.текстСообщения.Name = "текстСообщения";
            this.текстСообщения.Properties.ReadOnly = true;
            this.текстСообщения.Size = new System.Drawing.Size(556, 164);
            this.текстСообщения.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(248, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Администратор посылает Вам сообщения:";
            // 
            // ОкноСообщенияЧерезБД
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.кнопкаЗакрыть;
            this.ClientSize = new System.Drawing.Size(580, 236);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.текстСообщения);
            this.Controls.Add(this.кнопкаЗакрыть);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ОкноСообщенияЧерезБД";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сообщение";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.текстСообщения.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton кнопкаЗакрыть;
        private DevExpress.XtraEditors.MemoEdit текстСообщения;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}