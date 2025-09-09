namespace Payroll.UI.Etf.Etf
{
    partial class TcEtfForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.generateButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.etfDataGridView = new System.Windows.Forms.DataGridView();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employerNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberAcNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fromColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anEmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.reasonsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.identifierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invalidRowsTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.validRowsTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.etfDataGridView)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageColumn,
            this.identifierColumn,
            this.modifiedDateColumn,
            this.invalidRowsTotalColumn,
            this.validRowsTotalColumn,
            this.totalColumn,
            this.emptyColumn});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(751, 215);
            this.dataGridView.TabIndex = 12;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // generateButton
            // 
            this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateButton.Location = new System.Drawing.Point(688, 651);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 13;
            this.generateButton.Text = "Generate";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Items.AddRange(new object[] {
            "All",
            "Invalid"});
            this.filterComboBox.Location = new System.Drawing.Point(6, 24);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(259, 21);
            this.filterComboBox.TabIndex = 14;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // etfDataGridView
            // 
            this.etfDataGridView.AllowUserToAddRows = false;
            this.etfDataGridView.AllowUserToDeleteRows = false;
            this.etfDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.etfDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.etfDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNumberColumn,
            this.identificationColumn,
            this.employerNumberColumn,
            this.memberAcNumberColumn,
            this.initialsColumn,
            this.lastNameColumn,
            this.nicNumberColumn,
            this.fromColumn,
            this.toColumn,
            this.totalContributionColumn,
            this.anEmptyColumn});
            this.etfDataGridView.Location = new System.Drawing.Point(6, 53);
            this.etfDataGridView.MultiSelect = false;
            this.etfDataGridView.Name = "etfDataGridView";
            this.etfDataGridView.ReadOnly = true;
            this.etfDataGridView.RowHeadersVisible = false;
            this.etfDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.etfDataGridView.Size = new System.Drawing.Size(739, 204);
            this.etfDataGridView.TabIndex = 16;
            this.etfDataGridView.SelectionChanged += new System.EventHandler(this.etfDataGridView_SelectionChanged);
            // 
            // lineNumberColumn
            // 
            this.lineNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lineNumberColumn.DataPropertyName = "LineNumber";
            this.lineNumberColumn.HeaderText = "Line";
            this.lineNumberColumn.MinimumWidth = 100;
            this.lineNumberColumn.Name = "lineNumberColumn";
            this.lineNumberColumn.ReadOnly = true;
            // 
            // identificationColumn
            // 
            this.identificationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.identificationColumn.DataPropertyName = "Identification";
            this.identificationColumn.HeaderText = "Identification";
            this.identificationColumn.MinimumWidth = 100;
            this.identificationColumn.Name = "identificationColumn";
            this.identificationColumn.ReadOnly = true;
            // 
            // employerNumberColumn
            // 
            this.employerNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.employerNumberColumn.DataPropertyName = "EmployerNumber";
            this.employerNumberColumn.HeaderText = "Employer Number";
            this.employerNumberColumn.MinimumWidth = 120;
            this.employerNumberColumn.Name = "employerNumberColumn";
            this.employerNumberColumn.ReadOnly = true;
            this.employerNumberColumn.Width = 120;
            // 
            // memberAcNumberColumn
            // 
            this.memberAcNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.memberAcNumberColumn.DataPropertyName = "MemberNumber";
            this.memberAcNumberColumn.HeaderText = "Member Number";
            this.memberAcNumberColumn.MinimumWidth = 110;
            this.memberAcNumberColumn.Name = "memberAcNumberColumn";
            this.memberAcNumberColumn.ReadOnly = true;
            this.memberAcNumberColumn.Width = 110;
            // 
            // initialsColumn
            // 
            this.initialsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.initialsColumn.DataPropertyName = "Initials";
            this.initialsColumn.HeaderText = "Initials";
            this.initialsColumn.MinimumWidth = 70;
            this.initialsColumn.Name = "initialsColumn";
            this.initialsColumn.ReadOnly = true;
            this.initialsColumn.Width = 70;
            // 
            // lastNameColumn
            // 
            this.lastNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lastNameColumn.DataPropertyName = "Surname";
            this.lastNameColumn.HeaderText = "Surname";
            this.lastNameColumn.MinimumWidth = 120;
            this.lastNameColumn.Name = "lastNameColumn";
            this.lastNameColumn.ReadOnly = true;
            this.lastNameColumn.Width = 120;
            // 
            // nicNumberColumn
            // 
            this.nicNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nicNumberColumn.DataPropertyName = "NICNumber";
            this.nicNumberColumn.HeaderText = "NIC Number";
            this.nicNumberColumn.MinimumWidth = 100;
            this.nicNumberColumn.Name = "nicNumberColumn";
            this.nicNumberColumn.ReadOnly = true;
            // 
            // fromColumn
            // 
            this.fromColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.fromColumn.DataPropertyName = "From";
            this.fromColumn.HeaderText = "From";
            this.fromColumn.MinimumWidth = 100;
            this.fromColumn.Name = "fromColumn";
            this.fromColumn.ReadOnly = true;
            // 
            // toColumn
            // 
            this.toColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.toColumn.DataPropertyName = "To";
            this.toColumn.HeaderText = "To";
            this.toColumn.MinimumWidth = 100;
            this.toColumn.Name = "toColumn";
            this.toColumn.ReadOnly = true;
            // 
            // totalContributionColumn
            // 
            this.totalContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.totalContributionColumn.DataPropertyName = "TotalContribution";
            this.totalContributionColumn.HeaderText = "Total Contribution";
            this.totalContributionColumn.MinimumWidth = 140;
            this.totalContributionColumn.Name = "totalContributionColumn";
            this.totalContributionColumn.ReadOnly = true;
            this.totalContributionColumn.Width = 140;
            // 
            // anEmptyColumn
            // 
            this.anEmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.anEmptyColumn.HeaderText = "";
            this.anEmptyColumn.Name = "anEmptyColumn";
            this.anEmptyColumn.ReadOnly = true;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(6, 376);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 17;
            this.statusLabel.Text = "--";
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.reasonsRichTextBox);
            this.groupBox.Controls.Add(this.etfDataGridView);
            this.groupBox.Controls.Add(this.statusLabel);
            this.groupBox.Controls.Add(this.filterComboBox);
            this.groupBox.Location = new System.Drawing.Point(12, 246);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(751, 399);
            this.groupBox.TabIndex = 18;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "--";
            // 
            // reasonsRichTextBox
            // 
            this.reasonsRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reasonsRichTextBox.Location = new System.Drawing.Point(6, 263);
            this.reasonsRichTextBox.Name = "reasonsRichTextBox";
            this.reasonsRichTextBox.ReadOnly = true;
            this.reasonsRichTextBox.Size = new System.Drawing.Size(739, 110);
            this.reasonsRichTextBox.TabIndex = 18;
            this.reasonsRichTextBox.Text = "";
            // 
            // openFolderButton
            // 
            this.openFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openFolderButton.Location = new System.Drawing.Point(607, 651);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(75, 23);
            this.openFolderButton.TabIndex = 20;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // imageColumn
            // 
            this.imageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageColumn.DataPropertyName = "Image";
            this.imageColumn.HeaderText = "";
            this.imageColumn.MinimumWidth = 25;
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            this.imageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.imageColumn.Width = 25;
            // 
            // identifierColumn
            // 
            this.identifierColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.identifierColumn.DataPropertyName = "Identifier";
            this.identifierColumn.HeaderText = "Business";
            this.identifierColumn.MinimumWidth = 100;
            this.identifierColumn.Name = "identifierColumn";
            this.identifierColumn.ReadOnly = true;
            // 
            // modifiedDateColumn
            // 
            this.modifiedDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.modifiedDateColumn.DataPropertyName = "ModifiedDate";
            this.modifiedDateColumn.HeaderText = "File Date";
            this.modifiedDateColumn.MinimumWidth = 120;
            this.modifiedDateColumn.Name = "modifiedDateColumn";
            this.modifiedDateColumn.ReadOnly = true;
            this.modifiedDateColumn.Width = 120;
            // 
            // invalidRowsTotalColumn
            // 
            this.invalidRowsTotalColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.invalidRowsTotalColumn.DataPropertyName = "InvalidRowsTotal";
            this.invalidRowsTotalColumn.HeaderText = "Invalid Rows Total";
            this.invalidRowsTotalColumn.MinimumWidth = 120;
            this.invalidRowsTotalColumn.Name = "invalidRowsTotalColumn";
            this.invalidRowsTotalColumn.ReadOnly = true;
            this.invalidRowsTotalColumn.Width = 120;
            // 
            // validRowsTotalColumn
            // 
            this.validRowsTotalColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.validRowsTotalColumn.DataPropertyName = "ValidRowsTotal";
            this.validRowsTotalColumn.HeaderText = "Valid Rows Total";
            this.validRowsTotalColumn.MinimumWidth = 120;
            this.validRowsTotalColumn.Name = "validRowsTotalColumn";
            this.validRowsTotalColumn.ReadOnly = true;
            this.validRowsTotalColumn.Width = 120;
            // 
            // totalColumn
            // 
            this.totalColumn.DataPropertyName = "Total";
            this.totalColumn.HeaderText = "Total";
            this.totalColumn.MinimumWidth = 100;
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            // 
            // emptyColumn
            // 
            this.emptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.emptyColumn.HeaderText = "";
            this.emptyColumn.Name = "emptyColumn";
            this.emptyColumn.ReadOnly = true;
            // 
            // TcEtfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcEtfForm";
            this.Text = "TcEPFForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.etfDataGridView)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.DataGridView etfDataGridView;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RichTextBox reasonsRichTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employerNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fromColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anEmptyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberAcNumberColumn;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identifierColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invalidRowsTotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn validRowsTotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emptyColumn;
    }
}