namespace Барс.Интерфейс
{
    partial class ДиалогВводаСтроки
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
            this.ПриглашениеКВводу = new DevExpress.XtraEditors.LabelControl();
            this.Редактор = new DevExpress.XtraEditors.TextEdit();
            this.кнопкаОтмена = new DevExpress.XtraEditors.SimpleButton();
            this.кнопкаОК = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Редактор.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ПриглашениеКВводу
            // 
            this.ПриглашениеКВводу.Location = new System.Drawing.Point(12, 12);
            this.ПриглашениеКВводу.Name = "ПриглашениеКВводу";
            this.ПриглашениеКВводу.Size = new System.Drawing.Size(82, 13);
            this.ПриглашениеКВводу.TabIndex = 0;
            this.ПриглашениеКВводу.Text = "Введите текст :";
            // 
            // Редактор
            // 
            this.Редактор.Location = new System.Drawing.Point(12, 31);
            this.Редактор.Name = "Редактор";
            this.Редактор.Size = new System.Drawing.Size(384, 19);
            this.Редактор.TabIndex = 1;
            // 
            // кнопкаОтмена
            // 
            this.кнопкаОтмена.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.кнопкаОтмена.Location = new System.Drawing.Point(321, 62);
            this.кнопкаОтмена.Name = "кнопкаОтмена";
            this.кнопкаОтмена.Size = new System.Drawing.Size(75, 23);
            this.кнопкаОтмена.TabIndex = 2;
            this.кнопкаОтмена.Text = "Отмена";
            // 
            // кнопкаОК
            // 
            this.кнопкаОК.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.кнопкаОК.Location = new System.Drawing.Point(240, 62);
            this.кнопкаОК.Name = "кнопкаОК";
            this.кнопкаОК.Size = new System.Drawing.Size(75, 23);
            this.кнопкаОК.TabIndex = 3;
            this.кнопкаОК.Text = "ОК";
            // 
            // ДиалогВводаСтроки
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.кнопкаОтмена;
            this.ClientSize = new System.Drawing.Size(409, 99);
            this.Controls.Add(this.кнопкаОК);
            this.Controls.Add(this.кнопкаОтмена);
            this.Controls.Add(this.Редактор);
            this.Controls.Add(this.ПриглашениеКВводу);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 135);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 135);
            this.Name = "ДиалогВводаСтроки";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.Редактор.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl ПриглашениеКВводу;
        private DevExpress.XtraEditors.TextEdit Редактор;
        private DevExpress.XtraEditors.SimpleButton кнопкаОтмена;
        private DevExpress.XtraEditors.SimpleButton кнопкаОК;
    }
}