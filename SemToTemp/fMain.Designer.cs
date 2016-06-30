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
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblNfiles = new System.Windows.Forms.Label();
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
            this.rbOpers = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rbMachine = new System.Windows.Forms.RadioButton();
            this.rbMeasure = new System.Windows.Forms.RadioButton();
            this.rbAddition = new System.Windows.Forms.RadioButton();
            this.rbMat = new System.Windows.Forms.RadioButton();
            this.rbInstr = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bExcelExportTo = new System.Windows.Forms.Button();
            this.tbWorkshop = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tpAddGroup.SuspendLayout();
            this.tp2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpAddGroup);
            this.tabControl.Controls.Add(this.tp2);
            this.tabControl.Controls.Add(this.tabPage1);
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
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(224, 132);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 25;
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
            // lblNfiles
            // 
            this.lblNfiles.AutoSize = true;
            this.lblNfiles.Location = new System.Drawing.Point(29, 137);
            this.lblNfiles.Name = "lblNfiles";
            this.lblNfiles.Size = new System.Drawing.Size(0, 13);
            this.lblNfiles.TabIndex = 23;
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
            this.tbYear.Text = "E";
            // 
            // tbDoc
            // 
            this.tbDoc.Location = new System.Drawing.Point(274, 70);
            this.tbDoc.Name = "tbDoc";
            this.tbDoc.Size = new System.Drawing.Size(31, 20);
            this.tbDoc.TabIndex = 16;
            this.tbDoc.Text = "E";
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
            this.tp2.Controls.Add(this.rbOpers);
            this.tp2.Controls.Add(this.radioButton1);
            this.tp2.Controls.Add(this.rbMachine);
            this.tp2.Controls.Add(this.rbMeasure);
            this.tp2.Controls.Add(this.rbAddition);
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
            // rbOpers
            // 
            this.rbOpers.AutoSize = true;
            this.rbOpers.Location = new System.Drawing.Point(27, 168);
            this.rbOpers.Name = "rbOpers";
            this.rbOpers.Size = new System.Drawing.Size(75, 17);
            this.rbOpers.TabIndex = 6;
            this.rbOpers.TabStop = true;
            this.rbOpers.Text = "Операции";
            this.rbOpers.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(27, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(163, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "Вспомогательный процесс";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rbMachine
            // 
            this.rbMachine.AutoSize = true;
            this.rbMachine.Location = new System.Drawing.Point(27, 133);
            this.rbMachine.Name = "rbMachine";
            this.rbMachine.Size = new System.Drawing.Size(98, 17);
            this.rbMachine.TabIndex = 4;
            this.rbMachine.Text = "Оборудование";
            this.rbMachine.UseVisualStyleBackColor = true;
            // 
            // rbMeasure
            // 
            this.rbMeasure.AutoSize = true;
            this.rbMeasure.Location = new System.Drawing.Point(27, 87);
            this.rbMeasure.Name = "rbMeasure";
            this.rbMeasure.Size = new System.Drawing.Size(132, 17);
            this.rbMeasure.TabIndex = 3;
            this.rbMeasure.Text = "Средства измерения";
            this.rbMeasure.UseVisualStyleBackColor = true;
            // 
            // rbAddition
            // 
            this.rbAddition.AutoSize = true;
            this.rbAddition.Location = new System.Drawing.Point(27, 64);
            this.rbAddition.Name = "rbAddition";
            this.rbAddition.Size = new System.Drawing.Size(198, 17);
            this.rbAddition.TabIndex = 2;
            this.rbAddition.Text = "Слесарно-монтажный инструмент";
            this.rbAddition.UseVisualStyleBackColor = true;
            // 
            // rbMat
            // 
            this.rbMat.AutoSize = true;
            this.rbMat.Location = new System.Drawing.Point(27, 110);
            this.rbMat.Name = "rbMat";
            this.rbMat.Size = new System.Drawing.Size(83, 17);
            this.rbMat.TabIndex = 1;
            this.rbMat.Text = "Материалы";
            this.rbMat.UseVisualStyleBackColor = true;
            // 
            // rbInstr
            // 
            this.rbInstr.AutoSize = true;
            this.rbInstr.Checked = true;
            this.rbInstr.Location = new System.Drawing.Point(27, 19);
            this.rbInstr.Name = "rbInstr";
            this.rbInstr.Size = new System.Drawing.Size(134, 17);
            this.rbInstr.TabIndex = 0;
            this.rbInstr.TabStop = true;
            this.rbInstr.Text = "Режущий инструмент";
            this.rbInstr.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.bExcelExportTo);
            this.tabPage1.Controls.Add(this.tbWorkshop);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(343, 193);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Выгрузить ТО для цеха";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bExcelExportTo
            // 
            this.bExcelExportTo.Location = new System.Drawing.Point(29, 42);
            this.bExcelExportTo.Name = "bExcelExportTo";
            this.bExcelExportTo.Size = new System.Drawing.Size(129, 23);
            this.bExcelExportTo.TabIndex = 2;
            this.bExcelExportTo.Text = "Выгрузить в Excel";
            this.bExcelExportTo.UseVisualStyleBackColor = true;
            this.bExcelExportTo.Click += new System.EventHandler(this.bExcelExportTo_Click);
            // 
            // tbWorkshop
            // 
            this.tbWorkshop.Location = new System.Drawing.Point(58, 16);
            this.tbWorkshop.Name = "tbWorkshop";
            this.tbWorkshop.Size = new System.Drawing.Size(100, 20);
            this.tbWorkshop.TabIndex = 1;
            this.tbWorkshop.Text = "303";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Цех";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "ТП:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(180, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Операциии:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Переход:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(252, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(252, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(252, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "label12";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(289, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "из";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(289, 69);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "из";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(289, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "из";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(306, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "label16";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(306, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "label17";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(306, 69);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "label18";
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.RadioButton rbMachine;
        private System.Windows.Forms.RadioButton rbMeasure;
        private System.Windows.Forms.RadioButton rbAddition;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rbOpers;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button bExcelExportTo;
        private System.Windows.Forms.TextBox tbWorkshop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}