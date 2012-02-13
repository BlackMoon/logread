namespace zclient
{
    partial class Form1
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtIntervalFrom = new System.Windows.Forms.DateTimePicker();
            this.dtIntervalTo = new System.Windows.Forms.DateTimePicker();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lbClient = new System.Windows.Forms.Label();
            this.lbInterval = new System.Windows.Forms.Label();
            this.lbTo = new System.Windows.Forms.Label();
            this.lbFrom = new System.Windows.Forms.Label();
            this.lbResult = new System.Windows.Forms.Label();
            this.rtxtResult = new System.Windows.Forms.RichTextBox();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lbPort = new System.Windows.Forms.Label();
            this.lbIPAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(16, 314);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(158, 23);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "Запрос";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtIntervalFrom
            // 
            this.dtIntervalFrom.Location = new System.Drawing.Point(192, 77);
            this.dtIntervalFrom.Name = "dtIntervalFrom";
            this.dtIntervalFrom.Size = new System.Drawing.Size(140, 20);
            this.dtIntervalFrom.TabIndex = 2;
            // 
            // dtIntervalTo
            // 
            this.dtIntervalTo.Location = new System.Drawing.Point(440, 77);
            this.dtIntervalTo.Name = "dtIntervalTo";
            this.dtIntervalTo.Size = new System.Drawing.Size(140, 20);
            this.dtIntervalTo.TabIndex = 3;
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(192, 38);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(388, 20);
            this.txtClient.TabIndex = 4;
            // 
            // lbClient
            // 
            this.lbClient.AutoSize = true;
            this.lbClient.Location = new System.Drawing.Point(16, 38);
            this.lbClient.Name = "lbClient";
            this.lbClient.Size = new System.Drawing.Size(78, 13);
            this.lbClient.TabIndex = 5;
            this.lbClient.Text = "ФИО клиента";
            // 
            // lbInterval
            // 
            this.lbInterval.AutoSize = true;
            this.lbInterval.Location = new System.Drawing.Point(16, 84);
            this.lbInterval.Name = "lbInterval";
            this.lbInterval.Size = new System.Drawing.Size(56, 13);
            this.lbInterval.TabIndex = 6;
            this.lbInterval.Text = "Интервал";
            // 
            // lbTo
            // 
            this.lbTo.AutoSize = true;
            this.lbTo.Location = new System.Drawing.Point(415, 77);
            this.lbTo.Name = "lbTo";
            this.lbTo.Size = new System.Drawing.Size(19, 13);
            this.lbTo.TabIndex = 7;
            this.lbTo.Text = "до";
            // 
            // lbFrom
            // 
            this.lbFrom.AutoSize = true;
            this.lbFrom.Location = new System.Drawing.Point(156, 77);
            this.lbFrom.Name = "lbFrom";
            this.lbFrom.Size = new System.Drawing.Size(18, 13);
            this.lbFrom.TabIndex = 8;
            this.lbFrom.Text = "от";
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(16, 113);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(59, 13);
            this.lbResult.TabIndex = 9;
            this.lbResult.Text = "Результат";
            // 
            // rtxtResult
            // 
            this.rtxtResult.Location = new System.Drawing.Point(192, 113);
            this.rtxtResult.Name = "rtxtResult";
            this.rtxtResult.Size = new System.Drawing.Size(388, 248);
            this.rtxtResult.TabIndex = 10;
            this.rtxtResult.Text = "";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Enabled = false;
            this.txtIPAddress.Location = new System.Drawing.Point(192, 13);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(140, 20);
            this.txtIPAddress.TabIndex = 11;
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // txtPort
            // 
            this.txtPort.Enabled = false;
            this.txtPort.Location = new System.Drawing.Point(440, 13);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(140, 20);
            this.txtPort.TabIndex = 12;
            this.txtPort.Text = "10000";
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Location = new System.Drawing.Point(359, 19);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(32, 13);
            this.lbPort.TabIndex = 13;
            this.lbPort.Text = "Порт";
            // 
            // lbIPAddress
            // 
            this.lbIPAddress.AutoSize = true;
            this.lbIPAddress.Location = new System.Drawing.Point(19, 13);
            this.lbIPAddress.Name = "lbIPAddress";
            this.lbIPAddress.Size = new System.Drawing.Size(50, 13);
            this.lbIPAddress.TabIndex = 14;
            this.lbIPAddress.Text = "IP адрес";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 373);
            this.Controls.Add(this.lbIPAddress);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.rtxtResult);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.lbFrom);
            this.Controls.Add(this.lbTo);
            this.Controls.Add(this.lbInterval);
            this.Controls.Add(this.lbClient);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.dtIntervalTo);
            this.Controls.Add(this.dtIntervalFrom);
            this.Controls.Add(this.btnQuery);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";            
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker dtIntervalFrom;
        private System.Windows.Forms.DateTimePicker dtIntervalTo;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label lbClient;
        private System.Windows.Forms.Label lbInterval;
        private System.Windows.Forms.Label lbTo;
        private System.Windows.Forms.Label lbFrom;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.RichTextBox rtxtResult;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Label lbIPAddress;
    }
}

