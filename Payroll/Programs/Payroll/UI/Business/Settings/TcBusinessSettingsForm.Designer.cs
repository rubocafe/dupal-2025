namespace Payroll.UI.Business.Settings
{
    partial class TcBusinessSettingsForm
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
            this.statusLabel = new System.Windows.Forms.Label();
            this.loadDataButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.salaryMonthDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.payrollDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.dataDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.companyTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(9, 656);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "--";
            // 
            // loadDataButton
            // 
            this.loadDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadDataButton.Location = new System.Drawing.Point(688, 651);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(75, 23);
            this.loadDataButton.TabIndex = 7;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadDataButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Salary Month:";
            // 
            // salaryMonthDateTimePicker
            // 
            this.salaryMonthDateTimePicker.CustomFormat = "yyyy - MM";
            this.salaryMonthDateTimePicker.Enabled = false;
            this.salaryMonthDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.salaryMonthDateTimePicker.Location = new System.Drawing.Point(15, 154);
            this.salaryMonthDateTimePicker.Name = "salaryMonthDateTimePicker";
            this.salaryMonthDateTimePicker.ShowUpDown = true;
            this.salaryMonthDateTimePicker.Size = new System.Drawing.Size(72, 20);
            this.salaryMonthDateTimePicker.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Payroll Directory:";
            // 
            // payrollDirectoryTextBox
            // 
            this.payrollDirectoryTextBox.Location = new System.Drawing.Point(15, 25);
            this.payrollDirectoryTextBox.Name = "payrollDirectoryTextBox";
            this.payrollDirectoryTextBox.ReadOnly = true;
            this.payrollDirectoryTextBox.Size = new System.Drawing.Size(667, 20);
            this.payrollDirectoryTextBox.TabIndex = 1;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openFolderButton.Location = new System.Drawing.Point(607, 651);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(75, 23);
            this.openFolderButton.TabIndex = 6;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // dataDirectoryTextBox
            // 
            this.dataDirectoryTextBox.Location = new System.Drawing.Point(15, 68);
            this.dataDirectoryTextBox.Name = "dataDirectoryTextBox";
            this.dataDirectoryTextBox.ReadOnly = true;
            this.dataDirectoryTextBox.Size = new System.Drawing.Size(667, 20);
            this.dataDirectoryTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data Directory:";
            // 
            // companyTextBox
            // 
            this.companyTextBox.Location = new System.Drawing.Point(15, 111);
            this.companyTextBox.Name = "companyTextBox";
            this.companyTextBox.ReadOnly = true;
            this.companyTextBox.Size = new System.Drawing.Size(667, 20);
            this.companyTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Company:";
            // 
            // TcBusinessSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.companyTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataDirectoryTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.payrollDirectoryTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.salaryMonthDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.loadDataButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcBusinessSettingsForm";
            this.Text = "TcMasterDataForm";
            this.Load += new System.EventHandler(this.TcSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker salaryMonthDateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox payrollDirectoryTextBox;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.TextBox dataDirectoryTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox companyTextBox;
        private System.Windows.Forms.Label label4;
    }
}