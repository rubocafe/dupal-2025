namespace DUPALPayroll.UI.OfficeStaff.Analyze
{
    partial class TcOfficeStaffAnalyzeForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.amountsLabel = new System.Windows.Forms.Label();
            this.reasonsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.virtualNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfBirthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ocGradeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.daysWorkedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfJoinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankTransferAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basicSalaryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.braColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grossSalaryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epfDeductionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netSalaryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalRemunerationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressLine1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressLine2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(12, 47);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer.Panel1.Controls.Add(this.amountsLabel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.reasonsRichTextBox);
            this.splitContainer.Size = new System.Drawing.Size(754, 588);
            this.splitContainer.SplitterDistance = 482;
            this.splitContainer.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNumberColumn,
            this.virtualNumberColumn,
            this.nicColumn,
            this.nameColumn,
            this.initialsColumn,
            this.lastNameColumn,
            this.dateOfBirthColumn,
            this.ageColumn,
            this.designationColumn,
            this.ocGradeColumn,
            this.memberStatusColumn,
            this.daysWorkedColumn,
            this.dateOfJoinColumn,
            this.bankColumn,
            this.bankCodeColumn,
            this.branchColumn,
            this.branchCodeColumn,
            this.accountNumberColumn,
            this.bankTransferAmountColumn,
            this.basicSalaryColumn,
            this.braColumn,
            this.grossSalaryColumn,
            this.epfDeductionColumn,
            this.netSalaryColumn,
            this.totalRemunerationColumn,
            this.payeColumn,
            this.holdColumn,
            this.epfContributionColumn,
            this.etfContributionColumn,
            this.addressLine1Column,
            this.addressLine2Column,
            this.cityColumn});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(754, 442);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // amountsLabel
            // 
            this.amountsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.amountsLabel.AutoSize = true;
            this.amountsLabel.Location = new System.Drawing.Point(0, 449);
            this.amountsLabel.Name = "amountsLabel";
            this.amountsLabel.Size = new System.Drawing.Size(13, 13);
            this.amountsLabel.TabIndex = 5;
            this.amountsLabel.Text = "--";
            // 
            // reasonsRichTextBox
            // 
            this.reasonsRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reasonsRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.reasonsRichTextBox.Name = "reasonsRichTextBox";
            this.reasonsRichTextBox.ReadOnly = true;
            this.reasonsRichTextBox.Size = new System.Drawing.Size(754, 102);
            this.reasonsRichTextBox.TabIndex = 0;
            this.reasonsRichTextBox.Text = "";
            // 
            // analyzeButton
            // 
            this.analyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeButton.Location = new System.Drawing.Point(688, 651);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(75, 23);
            this.analyzeButton.TabIndex = 1;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 656);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "--";
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(12, 12);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(320, 21);
            this.filterComboBox.TabIndex = 3;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(574, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(189, 20);
            this.searchTextBox.TabIndex = 5;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // lineNumberColumn
            // 
            this.lineNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lineNumberColumn.DataPropertyName = "LineNumber";
            this.lineNumberColumn.HeaderText = "Line Number";
            this.lineNumberColumn.MinimumWidth = 100;
            this.lineNumberColumn.Name = "lineNumberColumn";
            this.lineNumberColumn.ReadOnly = true;
            // 
            // virtualNumberColumn
            // 
            this.virtualNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.virtualNumberColumn.DataPropertyName = "EmployeeNumber";
            this.virtualNumberColumn.HeaderText = "Employee Number";
            this.virtualNumberColumn.MinimumWidth = 120;
            this.virtualNumberColumn.Name = "virtualNumberColumn";
            this.virtualNumberColumn.ReadOnly = true;
            this.virtualNumberColumn.Width = 120;
            // 
            // nicColumn
            // 
            this.nicColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nicColumn.DataPropertyName = "NIC";
            this.nicColumn.HeaderText = "NIC";
            this.nicColumn.MinimumWidth = 100;
            this.nicColumn.Name = "nicColumn";
            this.nicColumn.ReadOnly = true;
            // 
            // nameColumn
            // 
            this.nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.MinimumWidth = 100;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
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
            this.lastNameColumn.DataPropertyName = "LastName";
            this.lastNameColumn.HeaderText = "Last Name";
            this.lastNameColumn.MinimumWidth = 100;
            this.lastNameColumn.Name = "lastNameColumn";
            this.lastNameColumn.ReadOnly = true;
            // 
            // dateOfBirthColumn
            // 
            this.dateOfBirthColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateOfBirthColumn.DataPropertyName = "DateOfBirth";
            this.dateOfBirthColumn.HeaderText = "Date Of Birth";
            this.dateOfBirthColumn.MinimumWidth = 100;
            this.dateOfBirthColumn.Name = "dateOfBirthColumn";
            this.dateOfBirthColumn.ReadOnly = true;
            // 
            // ageColumn
            // 
            this.ageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ageColumn.DataPropertyName = "AgeString";
            this.ageColumn.HeaderText = "Age";
            this.ageColumn.MinimumWidth = 80;
            this.ageColumn.Name = "ageColumn";
            this.ageColumn.ReadOnly = true;
            this.ageColumn.Width = 80;
            // 
            // designationColumn
            // 
            this.designationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.designationColumn.DataPropertyName = "Designation";
            this.designationColumn.HeaderText = "Designation";
            this.designationColumn.MinimumWidth = 120;
            this.designationColumn.Name = "designationColumn";
            this.designationColumn.ReadOnly = true;
            this.designationColumn.Width = 120;
            // 
            // ocGradeColumn
            // 
            this.ocGradeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ocGradeColumn.DataPropertyName = "OCGrade";
            this.ocGradeColumn.HeaderText = "OC Grade";
            this.ocGradeColumn.MinimumWidth = 80;
            this.ocGradeColumn.Name = "ocGradeColumn";
            this.ocGradeColumn.ReadOnly = true;
            this.ocGradeColumn.Width = 80;
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
            // daysWorkedColumn
            // 
            this.daysWorkedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.daysWorkedColumn.DataPropertyName = "DaysWorked";
            this.daysWorkedColumn.HeaderText = "Days Worked";
            this.daysWorkedColumn.MinimumWidth = 100;
            this.daysWorkedColumn.Name = "daysWorkedColumn";
            this.daysWorkedColumn.ReadOnly = true;
            // 
            // dateOfJoinColumn
            // 
            this.dateOfJoinColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateOfJoinColumn.DataPropertyName = "DateOfJoin";
            this.dateOfJoinColumn.HeaderText = "Date of Join";
            this.dateOfJoinColumn.MinimumWidth = 100;
            this.dateOfJoinColumn.Name = "dateOfJoinColumn";
            this.dateOfJoinColumn.ReadOnly = true;
            // 
            // bankColumn
            // 
            this.bankColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bankColumn.DataPropertyName = "Bank";
            this.bankColumn.HeaderText = "Bank";
            this.bankColumn.MinimumWidth = 100;
            this.bankColumn.Name = "bankColumn";
            this.bankColumn.ReadOnly = true;
            // 
            // bankCodeColumn
            // 
            this.bankCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bankCodeColumn.DataPropertyName = "BankCode";
            this.bankCodeColumn.HeaderText = "Bank Code";
            this.bankCodeColumn.MinimumWidth = 100;
            this.bankCodeColumn.Name = "bankCodeColumn";
            this.bankCodeColumn.ReadOnly = true;
            // 
            // branchColumn
            // 
            this.branchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.branchColumn.DataPropertyName = "Branch";
            this.branchColumn.HeaderText = "Branch";
            this.branchColumn.MinimumWidth = 120;
            this.branchColumn.Name = "branchColumn";
            this.branchColumn.ReadOnly = true;
            this.branchColumn.Width = 120;
            // 
            // branchCodeColumn
            // 
            this.branchCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.branchCodeColumn.DataPropertyName = "BranchCode";
            this.branchCodeColumn.HeaderText = "Branch Code";
            this.branchCodeColumn.MinimumWidth = 100;
            this.branchCodeColumn.Name = "branchCodeColumn";
            this.branchCodeColumn.ReadOnly = true;
            // 
            // accountNumberColumn
            // 
            this.accountNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.accountNumberColumn.DataPropertyName = "AccountNumber";
            this.accountNumberColumn.HeaderText = "Account Number";
            this.accountNumberColumn.MinimumWidth = 120;
            this.accountNumberColumn.Name = "accountNumberColumn";
            this.accountNumberColumn.ReadOnly = true;
            this.accountNumberColumn.Width = 120;
            // 
            // bankTransferAmountColumn
            // 
            this.bankTransferAmountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bankTransferAmountColumn.DataPropertyName = "BankTransferAmount";
            this.bankTransferAmountColumn.HeaderText = "Bank Transfer Amount";
            this.bankTransferAmountColumn.MinimumWidth = 140;
            this.bankTransferAmountColumn.Name = "bankTransferAmountColumn";
            this.bankTransferAmountColumn.ReadOnly = true;
            this.bankTransferAmountColumn.Width = 140;
            // 
            // basicSalaryColumn
            // 
            this.basicSalaryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.basicSalaryColumn.DataPropertyName = "BasicSalary";
            this.basicSalaryColumn.HeaderText = "Basic Salary";
            this.basicSalaryColumn.MinimumWidth = 120;
            this.basicSalaryColumn.Name = "basicSalaryColumn";
            this.basicSalaryColumn.ReadOnly = true;
            this.basicSalaryColumn.Width = 120;
            // 
            // braColumn
            // 
            this.braColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.braColumn.DataPropertyName = "BRA";
            this.braColumn.HeaderText = "BRA";
            this.braColumn.MinimumWidth = 120;
            this.braColumn.Name = "braColumn";
            this.braColumn.ReadOnly = true;
            this.braColumn.Width = 120;
            // 
            // grossSalaryColumn
            // 
            this.grossSalaryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.grossSalaryColumn.DataPropertyName = "GrossSalary";
            this.grossSalaryColumn.HeaderText = "Gross Salary";
            this.grossSalaryColumn.MinimumWidth = 100;
            this.grossSalaryColumn.Name = "grossSalaryColumn";
            this.grossSalaryColumn.ReadOnly = true;
            // 
            // epfDeductionColumn
            // 
            this.epfDeductionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.epfDeductionColumn.DataPropertyName = "EPFDeduction";
            this.epfDeductionColumn.HeaderText = "EPF Deduction";
            this.epfDeductionColumn.MinimumWidth = 120;
            this.epfDeductionColumn.Name = "epfDeductionColumn";
            this.epfDeductionColumn.ReadOnly = true;
            this.epfDeductionColumn.Width = 120;
            // 
            // netSalaryColumn
            // 
            this.netSalaryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.netSalaryColumn.DataPropertyName = "NetSalary";
            this.netSalaryColumn.HeaderText = "Net Salary";
            this.netSalaryColumn.MinimumWidth = 120;
            this.netSalaryColumn.Name = "netSalaryColumn";
            this.netSalaryColumn.ReadOnly = true;
            this.netSalaryColumn.Width = 120;
            // 
            // totalRemunerationColumn
            // 
            this.totalRemunerationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.totalRemunerationColumn.DataPropertyName = "TotalRemuneration";
            this.totalRemunerationColumn.HeaderText = "Total Remuneration";
            this.totalRemunerationColumn.MinimumWidth = 130;
            this.totalRemunerationColumn.Name = "totalRemunerationColumn";
            this.totalRemunerationColumn.ReadOnly = true;
            this.totalRemunerationColumn.Width = 130;
            // 
            // payeColumn
            // 
            this.payeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.payeColumn.DataPropertyName = "Paye";
            this.payeColumn.HeaderText = "PAYE";
            this.payeColumn.MinimumWidth = 100;
            this.payeColumn.Name = "payeColumn";
            this.payeColumn.ReadOnly = true;
            // 
            // holdColumn
            // 
            this.holdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.holdColumn.DataPropertyName = "Hold";
            this.holdColumn.HeaderText = "Hold";
            this.holdColumn.MinimumWidth = 120;
            this.holdColumn.Name = "holdColumn";
            this.holdColumn.ReadOnly = true;
            this.holdColumn.Width = 120;
            // 
            // epfContributionColumn
            // 
            this.epfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.epfContributionColumn.DataPropertyName = "EPFContribution";
            this.epfContributionColumn.HeaderText = "EPF Contribution";
            this.epfContributionColumn.MinimumWidth = 120;
            this.epfContributionColumn.Name = "epfContributionColumn";
            this.epfContributionColumn.ReadOnly = true;
            this.epfContributionColumn.Width = 120;
            // 
            // etfContributionColumn
            // 
            this.etfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.etfContributionColumn.DataPropertyName = "ETFContribution";
            this.etfContributionColumn.HeaderText = "ETF Contribution";
            this.etfContributionColumn.MinimumWidth = 120;
            this.etfContributionColumn.Name = "etfContributionColumn";
            this.etfContributionColumn.ReadOnly = true;
            this.etfContributionColumn.Width = 120;
            // 
            // addressLine1Column
            // 
            this.addressLine1Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.addressLine1Column.DataPropertyName = "AddressLine1";
            this.addressLine1Column.HeaderText = "Address Line 1";
            this.addressLine1Column.MinimumWidth = 100;
            this.addressLine1Column.Name = "addressLine1Column";
            this.addressLine1Column.ReadOnly = true;
            // 
            // addressLine2Column
            // 
            this.addressLine2Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.addressLine2Column.DataPropertyName = "AddressLine2";
            this.addressLine2Column.HeaderText = "Address Line 2";
            this.addressLine2Column.MinimumWidth = 100;
            this.addressLine2Column.Name = "addressLine2Column";
            this.addressLine2Column.ReadOnly = true;
            // 
            // cityColumn
            // 
            this.cityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cityColumn.DataPropertyName = "City";
            this.cityColumn.HeaderText = "City";
            this.cityColumn.MinimumWidth = 100;
            this.cityColumn.Name = "cityColumn";
            this.cityColumn.ReadOnly = true;
            // 
            // TcOfficeStaffAnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcOfficeStaffAnalyzeForm";
            this.Text = "TcAnalyzeForm";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox reasonsRichTextBox;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label amountsLabel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn virtualNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfBirthColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocGradeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn daysWorkedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfJoinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankTransferAmountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basicSalaryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn braColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn grossSalaryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epfDeductionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn netSalaryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalRemunerationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn payeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn holdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn etfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressLine1Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressLine2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityColumn;
    }
}