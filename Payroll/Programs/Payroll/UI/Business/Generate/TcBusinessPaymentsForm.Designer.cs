namespace Payroll.UI.Business.Generate
{
    partial class TcBusinessPaymentsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.branchCodeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.accountNumberTextBox = new System.Windows.Forms.TextBox();
            this.accountNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.referenceTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.valueDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.generateButton = new System.Windows.Forms.Button();
            this.generateSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.satisfiedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportButton = new System.Windows.Forms.Button();
            this.zipSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.openFolderbutton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.salarySlipsButton = new System.Windows.Forms.Button();
            this.slarySlipSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.bankTextBox = new System.Windows.Forms.TextBox();
            this.bankLabel = new System.Windows.Forms.Label();
            this.bankCodeTextBox = new System.Windows.Forms.TextBox();
            this.bankCodeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 521);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Branch Code:";
            // 
            // branchCodeTextBox
            // 
            this.branchCodeTextBox.Enabled = false;
            this.branchCodeTextBox.Location = new System.Drawing.Point(434, 537);
            this.branchCodeTextBox.MaxLength = 3;
            this.branchCodeTextBox.Name = "branchCodeTextBox";
            this.branchCodeTextBox.Size = new System.Drawing.Size(109, 20);
            this.branchCodeTextBox.TabIndex = 1;
            this.branchCodeTextBox.Text = "000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 521);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Account Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 521);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Account Name:";
            // 
            // accountNumberTextBox
            // 
            this.accountNumberTextBox.Enabled = false;
            this.accountNumberTextBox.Location = new System.Drawing.Point(559, 537);
            this.accountNumberTextBox.MaxLength = 12;
            this.accountNumberTextBox.Name = "accountNumberTextBox";
            this.accountNumberTextBox.Size = new System.Drawing.Size(109, 20);
            this.accountNumberTextBox.TabIndex = 4;
            this.accountNumberTextBox.Text = "000000000000";
            // 
            // accountNameTextBox
            // 
            this.accountNameTextBox.Enabled = false;
            this.accountNameTextBox.Location = new System.Drawing.Point(15, 537);
            this.accountNameTextBox.MaxLength = 20;
            this.accountNameTextBox.Name = "accountNameTextBox";
            this.accountNameTextBox.Size = new System.Drawing.Size(150, 20);
            this.accountNameTextBox.TabIndex = 5;
            this.accountNameTextBox.Text = "ABC (PVT) LTD";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 564);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Reference:";
            // 
            // referenceTextBox
            // 
            this.referenceTextBox.Location = new System.Drawing.Point(181, 580);
            this.referenceTextBox.MaxLength = 15;
            this.referenceTextBox.Name = "referenceTextBox";
            this.referenceTextBox.Size = new System.Drawing.Size(150, 20);
            this.referenceTextBox.TabIndex = 7;
            this.referenceTextBox.Text = "SALARY MMM \'YY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 564);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Value Date:";
            // 
            // valueDateDateTimePicker
            // 
            this.valueDateDateTimePicker.CustomFormat = "yyyy - MM - dd";
            this.valueDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.valueDateDateTimePicker.Location = new System.Drawing.Point(15, 580);
            this.valueDateDateTimePicker.Name = "valueDateDateTimePicker";
            this.valueDateDateTimePicker.Size = new System.Drawing.Size(109, 20);
            this.valueDateDateTimePicker.TabIndex = 9;
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.Location = new System.Drawing.Point(688, 651);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 10;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // generateSaveFileDialog
            // 
            this.generateSaveFileDialog.DefaultExt = "dat";
            this.generateSaveFileDialog.FileName = "Temp";
            this.generateSaveFileDialog.Filter = "PayMaster files|*.dat|All files|*.*";
            this.generateSaveFileDialog.Title = "Select Path to Save PayMaster File";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageColumn,
            this.descriptionColumn,
            this.emptyColumn,
            this.satisfiedColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(751, 506);
            this.dataGridView.TabIndex = 11;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // imageColumn
            // 
            this.imageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageColumn.DataPropertyName = "Image";
            this.imageColumn.HeaderText = "Image";
            this.imageColumn.MinimumWidth = 25;
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            this.imageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imageColumn.Width = 25;
            // 
            // descriptionColumn
            // 
            this.descriptionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.descriptionColumn.DataPropertyName = "Description";
            this.descriptionColumn.HeaderText = "Description";
            this.descriptionColumn.MinimumWidth = 100;
            this.descriptionColumn.Name = "descriptionColumn";
            this.descriptionColumn.ReadOnly = true;
            // 
            // emptyColumn
            // 
            this.emptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.emptyColumn.HeaderText = "";
            this.emptyColumn.Name = "emptyColumn";
            this.emptyColumn.ReadOnly = true;
            // 
            // satisfiedColumn
            // 
            this.satisfiedColumn.DataPropertyName = "Satisfied";
            this.satisfiedColumn.HeaderText = "Satisfied";
            this.satisfiedColumn.Name = "satisfiedColumn";
            this.satisfiedColumn.ReadOnly = true;
            this.satisfiedColumn.Visible = false;
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(607, 651);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // zipSaveFileDialog
            // 
            this.zipSaveFileDialog.DefaultExt = "zip";
            this.zipSaveFileDialog.Filter = "zip files|*.zip|All files|*.*";
            this.zipSaveFileDialog.RestoreDirectory = true;
            this.zipSaveFileDialog.Title = "Select Path to Save Source Directory as a ZIP";
            // 
            // summaryLabel
            // 
            this.summaryLabel.AutoSize = true;
            this.summaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.summaryLabel.Location = new System.Drawing.Point(12, 619);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(15, 13);
            this.summaryLabel.TabIndex = 14;
            this.summaryLabel.Text = "--";
            this.summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFolderbutton
            // 
            this.openFolderbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openFolderbutton.Location = new System.Drawing.Point(445, 651);
            this.openFolderbutton.Name = "openFolderbutton";
            this.openFolderbutton.Size = new System.Drawing.Size(75, 23);
            this.openFolderbutton.TabIndex = 17;
            this.openFolderbutton.Text = "Open Folder";
            this.openFolderbutton.UseVisualStyleBackColor = true;
            this.openFolderbutton.Click += new System.EventHandler(this.openFolderbutton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 656);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 12;
            this.statusLabel.Text = "--";
            // 
            // salarySlipsButton
            // 
            this.salarySlipsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.salarySlipsButton.Location = new System.Drawing.Point(526, 651);
            this.salarySlipsButton.Name = "salarySlipsButton";
            this.salarySlipsButton.Size = new System.Drawing.Size(75, 23);
            this.salarySlipsButton.TabIndex = 18;
            this.salarySlipsButton.Text = "Salary Slips";
            this.salarySlipsButton.UseVisualStyleBackColor = true;
            this.salarySlipsButton.Click += new System.EventHandler(this.salarySlipsButton_Click);
            // 
            // slarySlipSaveFileDialog
            // 
            this.slarySlipSaveFileDialog.DefaultExt = "zip";
            this.slarySlipSaveFileDialog.FileName = "CustomerCareSalrySlips.pdf";
            this.slarySlipSaveFileDialog.Filter = "pdf files|*.pdf|All files|*.*";
            this.slarySlipSaveFileDialog.RestoreDirectory = true;
            this.slarySlipSaveFileDialog.Title = "Select Path to Save Salry Slip";
            // 
            // bankTextBox
            // 
            this.bankTextBox.Enabled = false;
            this.bankTextBox.Location = new System.Drawing.Point(181, 537);
            this.bankTextBox.MaxLength = 3;
            this.bankTextBox.Name = "bankTextBox";
            this.bankTextBox.Size = new System.Drawing.Size(109, 20);
            this.bankTextBox.TabIndex = 20;
            this.bankTextBox.Text = "N/A";
            // 
            // bankLabel
            // 
            this.bankLabel.AutoSize = true;
            this.bankLabel.Location = new System.Drawing.Point(178, 521);
            this.bankLabel.Name = "bankLabel";
            this.bankLabel.Size = new System.Drawing.Size(35, 13);
            this.bankLabel.TabIndex = 19;
            this.bankLabel.Text = "Bank:";
            // 
            // bankCodeTextBox
            // 
            this.bankCodeTextBox.Enabled = false;
            this.bankCodeTextBox.Location = new System.Drawing.Point(308, 537);
            this.bankCodeTextBox.MaxLength = 3;
            this.bankCodeTextBox.Name = "bankCodeTextBox";
            this.bankCodeTextBox.Size = new System.Drawing.Size(109, 20);
            this.bankCodeTextBox.TabIndex = 22;
            this.bankCodeTextBox.Text = "0000";
            // 
            // bankCodeLabel
            // 
            this.bankCodeLabel.AutoSize = true;
            this.bankCodeLabel.Location = new System.Drawing.Point(305, 521);
            this.bankCodeLabel.Name = "bankCodeLabel";
            this.bankCodeLabel.Size = new System.Drawing.Size(63, 13);
            this.bankCodeLabel.TabIndex = 21;
            this.bankCodeLabel.Text = "Bank Code:";
            // 
            // TcBusinessPaymentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.bankCodeTextBox);
            this.Controls.Add(this.bankCodeLabel);
            this.Controls.Add(this.bankTextBox);
            this.Controls.Add(this.bankLabel);
            this.Controls.Add(this.salarySlipsButton);
            this.Controls.Add(this.openFolderbutton);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.valueDateDateTimePicker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.referenceTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.accountNameTextBox);
            this.Controls.Add(this.accountNumberTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.branchCodeTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcBusinessPaymentsForm";
            this.Text = "TcPayMasterGenerate";
            this.Load += new System.EventHandler(this.TcBusinessPayMasterGenerateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox branchCodeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox accountNumberTextBox;
        private System.Windows.Forms.TextBox accountNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox referenceTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker valueDateDateTimePicker;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.SaveFileDialog generateSaveFileDialog;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.SaveFileDialog zipSaveFileDialog;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emptyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn satisfiedColumn;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.Button openFolderbutton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button salarySlipsButton;
        private System.Windows.Forms.SaveFileDialog slarySlipSaveFileDialog;
        private System.Windows.Forms.TextBox bankTextBox;
        private System.Windows.Forms.Label bankLabel;
        private System.Windows.Forms.TextBox bankCodeTextBox;
        private System.Windows.Forms.Label bankCodeLabel;
    }
}