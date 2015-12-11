namespace SemToTemp
{
    partial class fConnect
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tBsid = new System.Windows.Forms.TextBox();
            this.tBport = new System.Windows.Forms.TextBox();
            this.tBhostname = new System.Windows.Forms.TextBox();
            this.tBpassword = new System.Windows.Forms.TextBox();
            this.tBlogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnConnect = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.tBsid);
            this.groupBox1.Controls.Add(this.tBport);
            this.groupBox1.Controls.Add(this.tBhostname);
            this.groupBox1.Controls.Add(this.tBpassword);
            this.groupBox1.Controls.Add(this.tBlogin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 189);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB Connection";
            // 
            // tBsid
            // 
            this.tBsid.Location = new System.Drawing.Point(87, 149);
            this.tBsid.Name = "tBsid";
            this.tBsid.Size = new System.Drawing.Size(100, 20);
            this.tBsid.TabIndex = 8;
            this.tBsid.Text = "temp";
            // 
            // tBport
            // 
            this.tBport.Location = new System.Drawing.Point(87, 124);
            this.tBport.Name = "tBport";
            this.tBport.Size = new System.Drawing.Size(100, 20);
            this.tBport.TabIndex = 7;
            this.tBport.Text = "1521";
            // 
            // tBhostname
            // 
            this.tBhostname.Location = new System.Drawing.Point(87, 101);
            this.tBhostname.Name = "tBhostname";
            this.tBhostname.Size = new System.Drawing.Size(100, 20);
            this.tBhostname.TabIndex = 6;
            this.tBhostname.Text = "temp-server";
            // 
            // tBpassword
            // 
            this.tBpassword.Location = new System.Drawing.Point(87, 77);
            this.tBpassword.Name = "tBpassword";
            this.tBpassword.PasswordChar = 'ï';
            this.tBpassword.Size = new System.Drawing.Size(100, 20);
            this.tBpassword.TabIndex = 5;
            this.tBpassword.Text = "avia_design";
            // 
            // tBlogin
            // 
            this.tBlogin.Location = new System.Drawing.Point(87, 52);
            this.tBlogin.Name = "tBlogin";
            this.tBlogin.Size = new System.Drawing.Size(100, 20);
            this.tBlogin.TabIndex = 1;
            this.tBlogin.Text = "avia_design";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "SID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hostname";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // bttnConnect
            // 
            this.bttnConnect.Location = new System.Drawing.Point(84, 207);
            this.bttnConnect.Name = "bttnConnect";
            this.bttnConnect.Size = new System.Drawing.Size(75, 23);
            this.bttnConnect.TabIndex = 1;
            this.bttnConnect.Text = "Connect";
            this.bttnConnect.UseVisualStyleBackColor = true;
            this.bttnConnect.Click += new System.EventHandler(this.bttnConnect_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "”Î√” - Debug",
            "¿‚Ë‡ÒÚ‡ - Debug",
            "¿‚Ë‡ÒÚ‡ - Release"});
            this.comboBox1.Location = new System.Drawing.Point(45, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(113, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "”Î√” - Debug";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // fConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 242);
            this.Controls.Add(this.bttnConnect);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fConnect";
            this.Text = "DB connection";
            this.Load += new System.EventHandler(this.fConnect_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fConnect_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBsid;
        private System.Windows.Forms.TextBox tBport;
        private System.Windows.Forms.TextBox tBhostname;
        private System.Windows.Forms.TextBox tBpassword;
        private System.Windows.Forms.TextBox tBlogin;
        private System.Windows.Forms.Button bttnConnect;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

