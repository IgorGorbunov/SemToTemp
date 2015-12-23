namespace SemToTemp
{
    partial class fMain
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpAddGroup = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLoad = new System.Windows.Forms.ProgressBar();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.tbDoc = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.bttnRecord = new System.Windows.Forms.Button();
            this.tp2 = new System.Windows.Forms.TabPage();
            this.rbMat = new System.Windows.Forms.RadioButton();
            this.rbInstr = new System.Windows.Forms.RadioButton();
            this.lblNfiles = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tpAddGroup.SuspendLayout();
            this.tp2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpAddGroup);
            this.tabControl.Controls.Add(this.tp2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(351, 219);
            this.tabControl.TabIndex = 0;
            // 
            // tpAddGroup
            // 
            this.tpAddGroup.Controls.Add(this.lblStatus);
            this.tpAddGroup.Controls.Add(this.label6);
            this.tpAddGroup.Controls.Add(this.lblNfiles);
            this.tpAddGroup.Controls.Add(this.label4);
            this.tpAddGroup.Controls.Add(this.label3);
            this.tpAddGroup.Controls.Add(this.label2);
            this.tpAddGroup.Controls.Add(this.label1);
            this.tpAddGroup.Controls.Add(this.pbLoad);
            this.tpAddGroup.Controls.Add(this.tbYear);
            this.tpAddGroup.Controls.Add(this.tbDoc);
            this.tpAddGroup.Controls.Add(this.tbTitle);
            this.tpAddGroup.Controls.Add(this.tbName);
            this.tpAddGroup.Controls.Add(this.bttnRecord);
            this.tpAddGroup.Location = new System.Drawing.Point(4, 22);
            this.tpAddGroup.Name = "tpAddGroup";
            this.tpAddGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddGroup.Size = new System.Drawing.Size(343, 193);
            this.tpAddGroup.TabIndex = 0;
            this.tpAddGroup.Text = "Добавить группу";
            this.tpAddGroup.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Намиенование столбца с годом документа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Наименование столбца с документом";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Наименование столбца с обозначением";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Наименование столбца с наименованием";
            // 
            // pbLoad
            // 
            this.pbLoad.Location = new System.Drawing.Point(6, 165);
            this.pbLoad.Name = "pbLoad";
            this.pbLoad.Size = new System.Drawing.Size(330, 10);
            this.pbLoad.TabIndex = 18;
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(274, 96);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(31, 20);
            this.tbYear.TabIndex = 17;
            this.tbYear.Text = "L";
            // 
            // tbDoc
            // 
            this.tbDoc.Location = new System.Drawing.Point(274, 70);
            this.tbDoc.Name = "tbDoc";
            this.tbDoc.Size = new System.Drawing.Size(31, 20);
            this.tbDoc.TabIndex = 16;
            this.tbDoc.Text = "L";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(274, 44);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(31, 20);
            this.tbTitle.TabIndex = 15;
            this.tbTitle.Text = "A";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(274, 18);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(31, 20);
            this.tbName.TabIndex = 14;
            this.tbName.Text = "B";
            // 
            // bttnRecord
            // 
            this.bttnRecord.Location = new System.Drawing.Point(122, 127);
            this.bttnRecord.Name = "bttnRecord";
            this.bttnRecord.Size = new System.Drawing.Size(75, 23);
            this.bttnRecord.TabIndex = 0;
            this.bttnRecord.Text = "Записать";
            this.bttnRecord.UseVisualStyleBackColor = true;
            this.bttnRecord.Click += new System.EventHandler(this.bttnRecord_Click);
            // 
            // tp2
            // 
            this.tp2.Controls.Add(this.rbMat);
            this.tp2.Controls.Add(this.rbInstr);
            this.tp2.Location = new System.Drawing.Point(4, 22);
            this.tp2.Name = "tp2";
            this.tp2.Padding = new System.Windows.Forms.Padding(3);
            this.tp2.Size = new System.Drawing.Size(343, 193);
            this.tp2.TabIndex = 1;
            this.tp2.Text = "Настройки";
            this.tp2.UseVisualStyleBackColor = true;
            // 
            // rbMat
            // 
            this.rbMat.AutoSize = true;
            this.rbMat.Checked = true;
            this.rbMat.Location = new System.Drawing.Point(205, 44);
            this.rbMat.Name = "rbMat";
            this.rbMat.Size = new System.Drawing.Size(83, 17);
            this.rbMat.TabIndex = 1;
            this.rbMat.TabStop = true;
            this.rbMat.Text = "Материалы";
            this.rbMat.UseVisualStyleBackColor = true;
            // 
            // rbInstr
            // 
            this.rbInstr.AutoSize = true;
            this.rbInstr.Location = new System.Drawing.Point(205, 21);
            this.rbInstr.Name = "rbInstr";
            this.rbInstr.Size = new System.Drawing.Size(85, 17);
            this.rbInstr.TabIndex = 0;
            this.rbInstr.Text = "radioButton1";
            this.rbInstr.UseVisualStyleBackColor = true;
            // 
            // lblNfiles
            // 
            this.lblNfiles.AutoSize = true;
            this.lblNfiles.Location = new System.Drawing.Point(29, 137);
            this.lblNfiles.Name = "lblNfiles";
            this.lblNfiles.Size = new System.Drawing.Size(0, 13);
            this.lblNfiles.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Записано файлов";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(224, 132);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 25;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 219);
            this.Controls.Add(this.tabControl);
            this.Name = "fMain";
            this.Text = "Внесение данных в ТеМП2";
            this.tabControl.ResumeLayout(false);
            this.tpAddGroup.ResumeLayout(false);
            this.tpAddGroup.PerformLayout();
            this.tp2.ResumeLayout(false);
            this.tp2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpAddGroup;
        private System.Windows.Forms.Button bttnRecord;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.TextBox tbDoc;
        private System.Windows.Forms.ProgressBar pbLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tp2;
        private System.Windows.Forms.RadioButton rbMat;
        private System.Windows.Forms.RadioButton rbInstr;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblNfiles;
    }
}