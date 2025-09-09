namespace DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData
{
    partial class TcSupervisorsAndBackOfficeMasterForm
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
            this.reloadDataButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.fileInfoLabel = new System.Windows.Forms.Label();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.duplicatesDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.virtualNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.initialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ocGradeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.basicSalaryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfJoinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressLine1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressLine2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dInitialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dLastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dOcGradeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dBasicSalaryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDateOfJoinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dAddressLine1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dAddressLine2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dCityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.duplicatesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(9, 656);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "--";
            // 
            // reloadDataButton
            // 
            this.reloadDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadDataButton.Location = new System.Drawing.Point(688, 651);
            this.reloadDataButton.Name = "reloadDataButton";
            this.reloadDataButton.Size = new System.Drawing.Size(75, 23);
            this.reloadDataButton.TabIndex = 4;
            this.reloadDataButton.Text = "Reload";
            this.reloadDataButton.UseVisualStyleBackColor = true;
            this.reloadDataButton.Click += new System.EventHandler(this.reloadDataButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineNumberColumn,
            this.virtualNumberColumn,
            this.nicColumn,
            this.nameColumn,
            this.initialsColumn,
            this.lastNameColumn,
            this.designationColumn,
            this.ocGradeColumn,
            this.basicSalaryColumn,
            this.dateOfJoinColumn,
            this.bankColumn,
            this.bankCodeColumn,
            this.branchColumn,
            this.branchCodeColumn,
            this.accountNumberColumn,
            this.addressLine1Column,
            this.addressLine2Column,
            this.cityColumn});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(751, 450);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // fileInfoLabel
            // 
            this.fileInfoLabel.AutoSize = true;
            this.fileInfoLabel.Location = new System.Drawing.Point(12, 9);
            this.fileInfoLabel.Name = "fileInfoLabel";
            this.fileInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.fileInfoLabel.TabIndex = 0;
            this.fileInfoLabel.Text = "--";
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(12, 35);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(379, 21);
            this.filterComboBox.TabIndex = 1;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(12, 73);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.duplicatesDataGridView);
            this.splitContainer.Size = new System.Drawing.Size(751, 561);
            this.splitContainer.SplitterDistance = 450;
            this.splitContainer.TabIndex = 9;
            // 
            // duplicatesDataGridView
            // 
            this.duplicatesDataGridView.AllowUserToAddRows = false;
            this.duplicatesDataGridView.AllowUserToDeleteRows = false;
            this.duplicatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.duplicatesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dInitialsColumn,
            this.dLastNameColumn,
            this.dataGridViewTextBoxColumn5,
            this.dOcGradeColumn,
            this.dBasicSalaryColumn,
            this.dDateOfJoinColumn,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dAddressLine1Column,
            this.dAddressLine2Column,
            this.dCityColumn});
            this.duplicatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duplicatesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.duplicatesDataGridView.Name = "duplicatesDataGridView";
            this.duplicatesDataGridView.ReadOnly = true;
            this.duplicatesDataGridView.RowHeadersVisible = false;
            this.duplicatesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.duplicatesDataGridView.Size = new System.Drawing.Size(751, 107);
            this.duplicatesDataGridView.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(574, 36);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(189, 20);
            this.searchTextBox.TabIndex = 3;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // lineNumberColumn
            // 
            this.lineNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.lineNumberColumn.DataPropertyName = "LineNumber";
            this.lineNumberColumn.HeaderText = "Line";
            this.lineNumberColumn.MinimumWidth = 70;
            this.lineNumberColumn.Name = "lineNumberColumn";
            this.lineNumberColumn.ReadOnly = true;
            this.lineNumberColumn.Width = 70;
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
            this.nameColumn.MinimumWidth = 140;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 140;
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
            // dateOfJoinColumn
            // 
            this.dateOfJoinColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dateOfJoinColumn.DataPropertyName = "DateOfJoin";
            this.dateOfJoinColumn.HeaderText = "Date Of Join";
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
            this.branchColumn.MinimumWidth = 100;
            this.branchColumn.Name = "branchColumn";
            this.branchColumn.ReadOnly = true;
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LineNumber";
            this.dataGridViewTextBoxColumn1.HeaderText = "Line";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "EmployeeNumber";
            this.dataGridViewTextBoxColumn2.HeaderText = "Employee Number";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "NIC";
            this.dataGridViewTextBoxColumn3.HeaderText = "NIC";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn4.HeaderText = "Name";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 140;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 140;
            // 
            // dInitialsColumn
            // 
            this.dInitialsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dInitialsColumn.DataPropertyName = "Initials";
            this.dInitialsColumn.HeaderText = "Initials";
            this.dInitialsColumn.MinimumWidth = 70;
            this.dInitialsColumn.Name = "dInitialsColumn";
            this.dInitialsColumn.ReadOnly = true;
            this.dInitialsColumn.Width = 70;
            // 
            // dLastNameColumn
            // 
            this.dLastNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dLastNameColumn.DataPropertyName = "LastName";
            this.dLastNameColumn.HeaderText = "Last Name";
            this.dLastNameColumn.MinimumWidth = 100;
            this.dLastNameColumn.Name = "dLastNameColumn";
            this.dLastNameColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Designation";
            this.dataGridViewTextBoxColumn5.HeaderText = "Designation";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dOcGradeColumn
            // 
            this.dOcGradeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dOcGradeColumn.DataPropertyName = "OCGrade";
            this.dOcGradeColumn.HeaderText = "OC Grade";
            this.dOcGradeColumn.MinimumWidth = 80;
            this.dOcGradeColumn.Name = "dOcGradeColumn";
            this.dOcGradeColumn.ReadOnly = true;
            this.dOcGradeColumn.Width = 80;
            // 
            // dBasicSalaryColumn
            // 
            this.dBasicSalaryColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dBasicSalaryColumn.DataPropertyName = "BasicSalary";
            this.dBasicSalaryColumn.HeaderText = "Basic Salary";
            this.dBasicSalaryColumn.MinimumWidth = 120;
            this.dBasicSalaryColumn.Name = "dBasicSalaryColumn";
            this.dBasicSalaryColumn.ReadOnly = true;
            this.dBasicSalaryColumn.Width = 120;
            // 
            // dDateOfJoinColumn
            // 
            this.dDateOfJoinColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dDateOfJoinColumn.DataPropertyName = "DateOfJoin";
            this.dDateOfJoinColumn.HeaderText = "Date Of Join";
            this.dDateOfJoinColumn.MinimumWidth = 100;
            this.dDateOfJoinColumn.Name = "dDateOfJoinColumn";
            this.dDateOfJoinColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Bank";
            this.dataGridViewTextBoxColumn9.HeaderText = "Bank";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "BankCode";
            this.dataGridViewTextBoxColumn10.HeaderText = "Bank Code";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Branch";
            this.dataGridViewTextBoxColumn11.HeaderText = "Branch";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "BranchCode";
            this.dataGridViewTextBoxColumn12.HeaderText = "Branch Code";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "AccountNumber";
            this.dataGridViewTextBoxColumn13.HeaderText = "Account Number";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 120;
            // 
            // dAddressLine1Column
            // 
            this.dAddressLine1Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dAddressLine1Column.DataPropertyName = "AddressLine1";
            this.dAddressLine1Column.HeaderText = "Address Line 1";
            this.dAddressLine1Column.MinimumWidth = 100;
            this.dAddressLine1Column.Name = "dAddressLine1Column";
            this.dAddressLine1Column.ReadOnly = true;
            // 
            // dAddressLine2Column
            // 
            this.dAddressLine2Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dAddressLine2Column.DataPropertyName = "AddressLine2";
            this.dAddressLine2Column.HeaderText = "Address Line 2";
            this.dAddressLine2Column.MinimumWidth = 100;
            this.dAddressLine2Column.Name = "dAddressLine2Column";
            this.dAddressLine2Column.ReadOnly = true;
            // 
            // dCityColumn
            // 
            this.dCityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dCityColumn.DataPropertyName = "City";
            this.dCityColumn.HeaderText = "City";
            this.dCityColumn.MinimumWidth = 100;
            this.dCityColumn.Name = "dCityColumn";
            this.dCityColumn.ReadOnly = true;
            // 
            // TcSupervisorsAndBackOfficeMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.fileInfoLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.reloadDataButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcSupervisorsAndBackOfficeMasterForm";
            this.Text = "TcMasterDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.duplicatesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button reloadDataButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label fileInfoLabel;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.DataGridView duplicatesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn virtualNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn initialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ocGradeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn basicSalaryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfJoinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressLine1Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressLine2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dInitialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dLastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dOcGradeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dBasicSalaryColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDateOfJoinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dAddressLine1Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn dAddressLine2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn dCityColumn;
    }
}