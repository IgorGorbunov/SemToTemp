namespace SemToTemp.DialogForms
{
    partial class AnotherGroupDialog
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
            this.lMessage = new System.Windows.Forms.Label();
            this.bAllNo = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bYes = new System.Windows.Forms.Button();
            this.bNo = new System.Windows.Forms.Button();
            this.bAllYes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lMessage
            // 
            this.lMessage.Location = new System.Drawing.Point(12, 19);
            this.lMessage.Name = "lMessage";
            this.lMessage.Size = new System.Drawing.Size(326, 70);
            this.lMessage.TabIndex = 0;
            this.lMessage.Text = "label1";
            // 
            // bAllNo
            // 
            this.bAllNo.Location = new System.Drawing.Point(160, 127);
            this.bAllNo.Name = "bAllNo";
            this.bAllNo.Size = new System.Drawing.Size(86, 23);
            this.bAllNo.TabIndex = 1;
            this.bAllNo.Text = "Нет, для всех";
            this.bAllNo.UseVisualStyleBackColor = true;
            this.bAllNo.Click += new System.EventHandler(this.bAllNo_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(252, 98);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(86, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bYes
            // 
            this.bYes.Location = new System.Drawing.Point(68, 98);
            this.bYes.Name = "bYes";
            this.bYes.Size = new System.Drawing.Size(86, 23);
            this.bYes.TabIndex = 3;
            this.bYes.Text = "Да";
            this.bYes.UseVisualStyleBackColor = true;
            this.bYes.Click += new System.EventHandler(this.bYes_Click);
            // 
            // bNo
            // 
            this.bNo.Location = new System.Drawing.Point(160, 98);
            this.bNo.Name = "bNo";
            this.bNo.Size = new System.Drawing.Size(86, 23);
            this.bNo.TabIndex = 4;
            this.bNo.Text = "Нет";
            this.bNo.UseVisualStyleBackColor = true;
            this.bNo.Click += new System.EventHandler(this.bNo_Click);
            // 
            // bAllYes
            // 
            this.bAllYes.Location = new System.Drawing.Point(68, 127);
            this.bAllYes.Name = "bAllYes";
            this.bAllYes.Size = new System.Drawing.Size(86, 23);
            this.bAllYes.TabIndex = 5;
            this.bAllYes.Text = "Да, для всех";
            this.bAllYes.UseVisualStyleBackColor = true;
            this.bAllYes.Click += new System.EventHandler(this.bAllYes_Click);
            // 
            // AnotherGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(350, 162);
            this.Controls.Add(this.bAllYes);
            this.Controls.Add(this.bNo);
            this.Controls.Add(this.bYes);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bAllNo);
            this.Controls.Add(this.lMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnotherGroupDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Внимание!";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnotherGroupDialog_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lMessage;
        private System.Windows.Forms.Button bAllNo;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bYes;
        private System.Windows.Forms.Button bNo;
        private System.Windows.Forms.Button bAllYes;
    }
}