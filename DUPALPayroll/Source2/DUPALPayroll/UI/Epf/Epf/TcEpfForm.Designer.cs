namespace DUPALPayroll.UI.Epf.Epf
{
    partial class TcEpfForm
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
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.identifierColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invalidRowsTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.validRowsTotalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generateButton = new System.Windows.Forms.Button();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.epfDataGridView = new System.Windows.Forms.DataGridView();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberAcNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ocGradeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employersContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.membersContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totoalEarningsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zoneCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employerNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.submitionIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.daysOfWorkColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contributionPeriodColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anEmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.reasonsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epfDataGridView)).BeginInit();
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
            this.identifierColumn.HeaderText = "Department";
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
            // epfDataGridView
            // 
            this.epfDataGridView.AllowUserToAddRows = false;
            this.epfDataGridView.AllowUserToDeleteRows = false;
            this.epfDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.epfDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.epfDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNumberColumn,
            this.nicNumberColumn,
            this.lastNameColumn,
            this.initialsColumn,
            this.memberAcNumberColumn,
            this.ocGradeColumn,
            this.totalContributionColumn,
            this.employersContributionColumn,
            this.membersContributionColumn,
            this.totoalEarningsColumn,
            this.memberStatusColumn,
            this.zoneCodeColumn,
            this.employerNumberColumn,
            this.submitionIdColumn,
            this.daysOfWorkColumn,
            this.contributionPeriodColumn,
            this.anEmptyColumn});
            this.epfDataGridView.Location = new System.Drawing.Point(6, 53);
            this.epfDataGridView.MultiSelect = false;
            this.epfDataGridView.Name = "epfDataGridView";
            this.epfDataGridView.ReadOnly = true;
            this.epfDataGridView.RowHeadersVisible = false;
            this.epfDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.epfDataGridView.Size = new System.Drawing.Size(739, 204);
            this.epfDataGridView.TabIndex = 16;
            this.epfDataGridView.SelectionChanged += new System.EventHandler(this.epfDataGridView_SelectionChanged);
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
            // nicNumberColumn
            // 
            this.nicNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nicNumberColumn.DataPropertyName = "NICNumber";
            this.nicNumberColumn.HeaderText = "NIC Number";
            this.nicNumberColumn.MinimumWidth = 100;
            this.nicNumberColumn.Name = "nicNumberColumn";
            this.nicNumberColumn.ReadOnly = true;
            // 
            // lastNameColumn
            // 
            this.lastNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lastNameColumn.DataPropertyName = "LastName";
            this.lastNameColumn.HeaderText = "Last Name";
            this.lastNameColumn.MinimumWidth = 120;
            this.lastNameColumn.Name = "lastNameColumn";
            this.lastNameColumn.ReadOnly = true;
            this.lastNameColumn.Width = 120;
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
            // ocGradeColumn
            // 
            this.ocGradeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ocGradeColumn.DataPropertyName = "OccupationClassificationGrade";
            this.ocGradeColumn.HeaderText = "OC Grade";
            this.ocGradeColumn.MinimumWidth = 100;
            this.ocGradeColumn.Name = "ocGradeColumn";
            this.ocGradeColumn.ReadOnly = true;
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
            // employersContributionColumn
            // 
            this.employersContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.employersContributionColumn.DataPropertyName = "EmployersContribution";
            this.employersContributionColumn.HeaderText = "Employers Contribution";
            this.employersContributionColumn.MinimumWidth = 140;
            this.employersContributionColumn.Name = "employersContributionColumn";
            this.employersContributionColumn.ReadOnly = true;
            this.employersContributionColumn.Width = 140;
            // 
            // membersContributionColumn
            // 
            this.membersContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.membersContributionColumn.DataPropertyName = "MembersContribution";
            this.membersContributionColumn.HeaderText = "Members Contribution";
            this.membersContributionColumn.MinimumWidth = 140;
            this.membersContributionColumn.Name = "membersContributionColumn";
            this.membersContributionColumn.ReadOnly = true;
            this.membersContributionColumn.Width = 140;
            // 
            // totoalEarningsColumn
            // 
            this.totoalEarningsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.totoalEarningsColumn.DataPropertyName = "TotalEarnings";
            this.totoalEarningsColumn.HeaderText = "Total Earnings";
            this.totoalEarningsColumn.MinimumWidth = 120;
            this.totoalEarningsColumn.Name = "totoalEarningsColumn";
            this.totoalEarningsColumn.ReadOnly = true;
            this.totoalEarningsColumn.Width = 120;
            // 
            // memberStatusColumn
            // 
            this.memberStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.memberStatusColumn.DataPropertyName = "MemberStatus";
            this.memberStatusColumn.HeaderText = "Member Status";
            this.memberStatusColumn.MinimumWidth = 110;
            this.memberStatusColumn.Name = "memberStatusColumn";
            this.memberStatusColumn.ReadOnly = true;
            this.memberStatusColumn.Width = 110;
            // 
            // zoneCodeColumn
            // 
            this.zoneCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.zoneCodeColumn.DataPropertyName = "ZoneCode";
            this.zoneCodeColumn.HeaderText = "Zone Code";
            this.zoneCodeColumn.MinimumWidth = 100;
            this.zoneCodeColumn.Name = "zoneCodeColumn";
            this.zoneCodeColumn.ReadOnly = true;
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
            // submitionIdColumn
            // 
            this.submitionIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.submitionIdColumn.DataPropertyName = "SubmitionId";
            this.submitionIdColumn.HeaderText = "Submition Id";
            this.submitionIdColumn.MinimumWidth = 100;
            this.submitionIdColumn.Name = "submitionIdColumn";
            this.submitionIdColumn.ReadOnly = true;
            // 
            // daysOfWorkColumn
            // 
            this.daysOfWorkColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.daysOfWorkColumn.DataPropertyName = "DaysOfWork";
            this.daysOfWorkColumn.HeaderText = "Days Of Work";
            this.daysOfWorkColumn.MinimumWidth = 110;
            this.daysOfWorkColumn.Name = "daysOfWorkColumn";
            this.daysOfWorkColumn.ReadOnly = true;
            this.daysOfWorkColumn.Width = 110;
            // 
            // contributionPeriodColumn
            // 
            this.contributionPeriodColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.contributionPeriodColumn.DataPropertyName = "ContributionPeriod";
            this.contributionPeriodColumn.HeaderText = "Contribution Period";
            this.contributionPeriodColumn.MinimumWidth = 130;
            this.contributionPeriodColumn.Name = "contributionPeriodColumn";
            this.contributionPeriodColumn.ReadOnly = true;
            this.contributionPeriodColumn.Width = 130;
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
            this.groupBox.Controls.Add(this.epfDataGridView);
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
            this.openFolderButton.TabIndex = 19;
            this.openFolderButton.Text = "Open Folder";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // TcEpfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcEpfForm";
            this.Text = "TcEPFForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epfDataGridView)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.DataGridView epfDataGridView;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RichTextBox reasonsRichTextBox;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identifierColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invalidRowsTotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn validRowsTotalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emptyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberAcNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocGradeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employersContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn membersContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totoalEarningsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zoneCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employerNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn submitionIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn daysOfWorkColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contributionPeriodColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anEmptyColumn;
        private System.Windows.Forms.Button openFolderButton;
    }
}