namespace LS.Client
{
    partial class frmClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabConnectToLS = new System.Windows.Forms.TabPage();
            this.lblConnecionStatus = new System.Windows.Forms.Label();
            this.btnConnectToLs = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtboxPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtboxUserName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtboxAdapterSetName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLsPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtboxLsServer = new System.Windows.Forms.TextBox();
            this.tabLsSubscriptions = new System.Windows.Forms.TabPage();
            this.comboBoxSubscriptionMode = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBoxSubsciptionItems = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtboxFeedAdatperName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxSchema = new System.Windows.Forms.TextBox();
            this.tabNotifications = new System.Windows.Forms.TabPage();
            this.dgNotifications = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabConnectToLS.SuspendLayout();
            this.tabLsSubscriptions.SuspendLayout();
            this.tabNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifications)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabConnectToLS);
            this.tabControl1.Controls.Add(this.tabLsSubscriptions);
            this.tabControl1.Controls.Add(this.tabNotifications);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1589, 709);
            this.tabControl1.TabIndex = 0;
            // 
            // tabConnectToLS
            // 
            this.tabConnectToLS.Controls.Add(this.lblConnecionStatus);
            this.tabConnectToLS.Controls.Add(this.btnConnectToLs);
            this.tabConnectToLS.Controls.Add(this.label12);
            this.tabConnectToLS.Controls.Add(this.txtboxPassword);
            this.tabConnectToLS.Controls.Add(this.label11);
            this.tabConnectToLS.Controls.Add(this.txtboxUserName);
            this.tabConnectToLS.Controls.Add(this.label9);
            this.tabConnectToLS.Controls.Add(this.txtboxAdapterSetName);
            this.tabConnectToLS.Controls.Add(this.label6);
            this.tabConnectToLS.Controls.Add(this.txtLsPort);
            this.tabConnectToLS.Controls.Add(this.label5);
            this.tabConnectToLS.Controls.Add(this.txtboxLsServer);
            this.tabConnectToLS.Location = new System.Drawing.Point(4, 25);
            this.tabConnectToLS.Name = "tabConnectToLS";
            this.tabConnectToLS.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnectToLS.Size = new System.Drawing.Size(1581, 680);
            this.tabConnectToLS.TabIndex = 1;
            this.tabConnectToLS.Text = "Connect to LS";
            this.tabConnectToLS.UseVisualStyleBackColor = true;
            // 
            // lblConnecionStatus
            // 
            this.lblConnecionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnecionStatus.ForeColor = System.Drawing.Color.Green;
            this.lblConnecionStatus.Location = new System.Drawing.Point(573, 35);
            this.lblConnecionStatus.Name = "lblConnecionStatus";
            this.lblConnecionStatus.Size = new System.Drawing.Size(339, 39);
            this.lblConnecionStatus.TabIndex = 23;
            // 
            // btnConnectToLs
            // 
            this.btnConnectToLs.Location = new System.Drawing.Point(184, 311);
            this.btnConnectToLs.Name = "btnConnectToLs";
            this.btnConnectToLs.Size = new System.Drawing.Size(246, 53);
            this.btnConnectToLs.TabIndex = 22;
            this.btnConnectToLs.Text = "Connect to LS Server";
            this.btnConnectToLs.UseVisualStyleBackColor = true;
            this.btnConnectToLs.Click += new System.EventHandler(this.btnConnectToLs_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 209);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 17);
            this.label12.TabIndex = 21;
            this.label12.Text = "Password";
            // 
            // txtboxPassword
            // 
            this.txtboxPassword.Location = new System.Drawing.Point(214, 209);
            this.txtboxPassword.Name = "txtboxPassword";
            this.txtboxPassword.Size = new System.Drawing.Size(188, 22);
            this.txtboxPassword.TabIndex = 20;
            this.txtboxPassword.Text = "123456";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(49, 166);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 17);
            this.label11.TabIndex = 19;
            this.label11.Text = "UserName";
            // 
            // txtboxUserName
            // 
            this.txtboxUserName.Location = new System.Drawing.Point(214, 166);
            this.txtboxUserName.Name = "txtboxUserName";
            this.txtboxUserName.Size = new System.Drawing.Size(188, 22);
            this.txtboxUserName.TabIndex = 18;
            this.txtboxUserName.Text = "EfgClient";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Adapter Set Name";
            // 
            // txtboxAdapterSetName
            // 
            this.txtboxAdapterSetName.Location = new System.Drawing.Point(214, 123);
            this.txtboxAdapterSetName.Name = "txtboxAdapterSetName";
            this.txtboxAdapterSetName.Size = new System.Drawing.Size(188, 22);
            this.txtboxAdapterSetName.TabIndex = 14;
            this.txtboxAdapterSetName.Text = "WELCOME";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "LS Port";
            // 
            // txtLsPort
            // 
            this.txtLsPort.Location = new System.Drawing.Point(214, 83);
            this.txtLsPort.Name = "txtLsPort";
            this.txtLsPort.Size = new System.Drawing.Size(188, 22);
            this.txtLsPort.TabIndex = 8;
            this.txtLsPort.Text = "8080";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "LS Server";
            // 
            // txtboxLsServer
            // 
            this.txtboxLsServer.Location = new System.Drawing.Point(214, 39);
            this.txtboxLsServer.Name = "txtboxLsServer";
            this.txtboxLsServer.Size = new System.Drawing.Size(188, 22);
            this.txtboxLsServer.TabIndex = 6;
            this.txtboxLsServer.Text = "localhost";
            // 
            // tabLsSubscriptions
            // 
            this.tabLsSubscriptions.Controls.Add(this.comboBoxSubscriptionMode);
            this.tabLsSubscriptions.Controls.Add(this.button2);
            this.tabLsSubscriptions.Controls.Add(this.txtBoxSubsciptionItems);
            this.tabLsSubscriptions.Controls.Add(this.label8);
            this.tabLsSubscriptions.Controls.Add(this.label7);
            this.tabLsSubscriptions.Controls.Add(this.txtboxFeedAdatperName);
            this.tabLsSubscriptions.Controls.Add(this.label3);
            this.tabLsSubscriptions.Controls.Add(this.label2);
            this.tabLsSubscriptions.Controls.Add(this.textboxSchema);
            this.tabLsSubscriptions.Location = new System.Drawing.Point(4, 25);
            this.tabLsSubscriptions.Name = "tabLsSubscriptions";
            this.tabLsSubscriptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabLsSubscriptions.Size = new System.Drawing.Size(1581, 680);
            this.tabLsSubscriptions.TabIndex = 0;
            this.tabLsSubscriptions.Text = "LS Subscriptions";
            this.tabLsSubscriptions.UseVisualStyleBackColor = true;
            // 
            // comboBoxSubscriptionMode
            // 
            this.comboBoxSubscriptionMode.AutoCompleteCustomSource.AddRange(new string[] {
            "DISTINCT",
            "MERGE",
            "COMMAND",
            "RAW"});
            this.comboBoxSubscriptionMode.FormattingEnabled = true;
            this.comboBoxSubscriptionMode.Items.AddRange(new object[] {
            "DISTINCT",
            "MERGE",
            "COMMAND",
            "RAW"});
            this.comboBoxSubscriptionMode.Location = new System.Drawing.Point(177, 175);
            this.comboBoxSubscriptionMode.Name = "comboBoxSubscriptionMode";
            this.comboBoxSubscriptionMode.Size = new System.Drawing.Size(187, 24);
            this.comboBoxSubscriptionMode.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button2.Location = new System.Drawing.Point(569, 496);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(307, 53);
            this.button2.TabIndex = 15;
            this.button2.Text = "Subscirbe To LightStreamer";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtBoxSubsciptionItems
            // 
            this.txtBoxSubsciptionItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSubsciptionItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtBoxSubsciptionItems.Location = new System.Drawing.Point(168, 116);
            this.txtBoxSubsciptionItems.Multiline = true;
            this.txtBoxSubsciptionItems.Name = "txtBoxSubsciptionItems";
            this.txtBoxSubsciptionItems.Size = new System.Drawing.Size(1195, 34);
            this.txtBoxSubsciptionItems.TabIndex = 13;
            this.txtBoxSubsciptionItems.Text = "Notify_User#1248";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label8.Location = new System.Drawing.Point(26, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Subsciption Items";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "AdapterName";
            // 
            // txtboxFeedAdatperName
            // 
            this.txtboxFeedAdatperName.Location = new System.Drawing.Point(168, 75);
            this.txtboxFeedAdatperName.Name = "txtboxFeedAdatperName";
            this.txtboxFeedAdatperName.Size = new System.Drawing.Size(188, 22);
            this.txtboxFeedAdatperName.TabIndex = 10;
            this.txtboxFeedAdatperName.Text = "MY_REMOTE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(26, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Schema Fields";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(26, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Subscription Mode";
            // 
            // textboxSchema
            // 
            this.textboxSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxSchema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textboxSchema.Location = new System.Drawing.Point(168, 216);
            this.textboxSchema.Multiline = true;
            this.textboxSchema.Name = "textboxSchema";
            this.textboxSchema.Size = new System.Drawing.Size(1070, 219);
            this.textboxSchema.TabIndex = 3;
            this.textboxSchema.Text = resources.GetString("textboxSchema.Text");
            this.textboxSchema.TextChanged += new System.EventHandler(this.textboxSchema_TextChanged);
            // 
            // tabNotifications
            // 
            this.tabNotifications.Controls.Add(this.dgNotifications);
            this.tabNotifications.Location = new System.Drawing.Point(4, 25);
            this.tabNotifications.Name = "tabNotifications";
            this.tabNotifications.Size = new System.Drawing.Size(1581, 680);
            this.tabNotifications.TabIndex = 2;
            this.tabNotifications.Text = "Notificaionts";
            this.tabNotifications.UseVisualStyleBackColor = true;
            // 
            // dgNotifications
            // 
            this.dgNotifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNotifications.Location = new System.Drawing.Point(20, 20);
            this.dgNotifications.Name = "dgNotifications";
            this.dgNotifications.RowTemplate.Height = 24;
            this.dgNotifications.Size = new System.Drawing.Size(1285, 619);
            this.dgNotifications.TabIndex = 0;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1613, 746);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmClient";
            this.Text = "LS Client";
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabConnectToLS.ResumeLayout(false);
            this.tabConnectToLS.PerformLayout();
            this.tabLsSubscriptions.ResumeLayout(false);
            this.tabLsSubscriptions.PerformLayout();
            this.tabNotifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNotifications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLsSubscriptions;
        private System.Windows.Forms.TabPage tabConnectToLS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxSchema;
        private System.Windows.Forms.Button btnConnectToLs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtboxPassword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtboxUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtboxAdapterSetName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLsPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtboxLsServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtboxFeedAdatperName;
        private System.Windows.Forms.Label lblConnecionStatus;
        private System.Windows.Forms.TextBox txtBoxSubsciptionItems;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabNotifications;
        private System.Windows.Forms.DataGridView dgNotifications;
        private System.Windows.Forms.ComboBox comboBoxSubscriptionMode;
    }
}

