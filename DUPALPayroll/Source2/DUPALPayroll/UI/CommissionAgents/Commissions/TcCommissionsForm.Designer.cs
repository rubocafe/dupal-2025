namespace DUPALPayroll.UI.CommissionAgents.Commissions
{
    partial class TcCommissionsForm
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
            this.reloadButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.fileInfoLabel = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.amountsLabel = new System.Windows.Forms.Label();
            this.duplicatesDataGridView = new System.Windows.Forms.DataGridView();
            this.filterComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.virtualNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlOrBpoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.daysWorkedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salesManagerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grossCommissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epfReductionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.netCommissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.etfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfJoinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEmployeeNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dTlOrBpoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dMemberStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDaysWorkedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicatesSalesManagerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicatesGrossCommissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicatesEPFDeductionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicatesNetCommissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dPayeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEpfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dEtfContributionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duplicatesDateOfJoinColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // reloadButton
            // 
            this.reloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reloadButton.Location = new System.Drawing.Point(688, 651);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(75, 23);
            this.reloadButton.TabIndex = 4;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
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
            this.employeeNumberColumn,
            this.virtualNumberColumn,
            this.nicColumn,
            this.tlOrBpoColumn,
            this.nameColumn,
            this.memberStatusColumn,
            this.daysWorkedColumn,
            this.salesManagerColumn,
            this.bankColumn,
            this.branchColumn,
            this.accountNumberColumn,
            this.grossCommissionColumn,
            this.epfReductionColumn,
            this.netCommissionColumn,
            this.payeColumn,
            this.epfContributionColumn,
            this.etfContributionColumn,
            this.dateOfJoinColumn,
            this.addressColumn});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(751, 416);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // fileInfoLabel
            // 
            this.fileInfoLabel.AutoSize = true;
            this.fileInfoLabel.Location = new System.Drawing.Point(12, 9);
            this.fileInfoLabel.Name = "fileInfoLabel";
            this.fileInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.fileInfoLabel.TabIndex = 7;
            this.fileInfoLabel.Text = "--";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(12, 73);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.amountsLabel);
            this.splitContainer.Panel1.Controls.Add(this.dataGridView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.duplicatesDataGridView);
            this.splitContainer.Size = new System.Drawing.Size(751, 561);
            this.splitContainer.SplitterDistance = 450;
            this.splitContainer.TabIndex = 8;
            // 
            // amountsLabel
            // 
            this.amountsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.amountsLabel.AutoSize = true;
            this.amountsLabel.Location = new System.Drawing.Point(0, 422);
            this.amountsLabel.Name = "amountsLabel";
            this.amountsLabel.Size = new System.Drawing.Size(13, 13);
            this.amountsLabel.TabIndex = 4;
            this.amountsLabel.Text = "--";
            // 
            // duplicatesDataGridView
            // 
            this.duplicatesDataGridView.AllowUserToAddRows = false;
            this.duplicatesDataGridView.AllowUserToDeleteRows = false;
            this.duplicatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.duplicatesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dEmployeeNumberColumn,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dTlOrBpoColumn,
            this.dataGridViewTextBoxColumn3,
            this.dMemberStatusColumn,
            this.dDaysWorkedColumn,
            this.duplicatesSalesManagerColumn,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn7,
            this.duplicatesGrossCommissionColumn,
            this.duplicatesEPFDeductionColumn,
            this.duplicatesNetCommissionColumn,
            this.dPayeColumn,
            this.dEpfContributionColumn,
            this.dEtfContributionColumn,
            this.duplicatesDateOfJoinColumn,
            this.dataGridViewTextBoxColumn6});
            this.duplicatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duplicatesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.duplicatesDataGridView.Name = "duplicatesDataGridView";
            this.duplicatesDataGridView.ReadOnly = true;
            this.duplicatesDataGridView.RowHeadersVisible = false;
            this.duplicatesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.duplicatesDataGridView.Size = new System.Drawing.Size(751, 107);
            this.duplicatesDataGridView.TabIndex = 9;
            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(12, 35);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(231, 21);
            this.filterComboBox.TabIndex = 10;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Search:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(574, 35);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(189, 20);
            this.searchTextBox.TabIndex = 12;
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
            // employeeNumberColumn
            // 
            this.employeeNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.employeeNumberColumn.DataPropertyName = "EmployeeNumber";
            this.employeeNumberColumn.HeaderText = "Employee Number";
            this.employeeNumberColumn.MinimumWidth = 120;
            this.employeeNumberColumn.Name = "employeeNumberColumn";
            this.employeeNumberColumn.ReadOnly = true;
            this.employeeNumberColumn.Width = 120;
            // 
            // virtualNumberColumn
            // 
            this.virtualNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.virtualNumberColumn.DataPropertyName = "VirtualNumber";
            this.virtualNumberColumn.HeaderText = "Virtual Number";
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
            // tlOrBpoColumn
            // 
            this.tlOrBpoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tlOrBpoColumn.DataPropertyName = "TLorBPO";
            this.tlOrBpoColumn.HeaderText = "TL/BPO";
            this.tlOrBpoColumn.MinimumWidth = 100;
            this.tlOrBpoColumn.Name = "tlOrBpoColumn";
            this.tlOrBpoColumn.ReadOnly = true;
            // 
            // nameColumn
            // 
            this.nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.MinimumWidth = 100;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
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
            // salesManagerColumn
            // 
            this.salesManagerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.salesManagerColumn.DataPropertyName = "SalesManager";
            this.salesManagerColumn.HeaderText = "Sales Manager";
            this.salesManagerColumn.MinimumWidth = 120;
            this.salesManagerColumn.Name = "salesManagerColumn";
            this.salesManagerColumn.ReadOnly = true;
            this.salesManagerColumn.Width = 120;
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
            // grossCommissionColumn
            // 
            this.grossCommissionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.grossCommissionColumn.DataPropertyName = "GrossCommission";
            this.grossCommissionColumn.HeaderText = "Gross Commission";
            this.grossCommissionColumn.MinimumWidth = 120;
            this.grossCommissionColumn.Name = "grossCommissionColumn";
            this.grossCommissionColumn.ReadOnly = true;
            this.grossCommissionColumn.Width = 120;
            // 
            // epfReductionColumn
            // 
            this.epfReductionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.epfReductionColumn.DataPropertyName = "EPFDeduction";
            this.epfReductionColumn.HeaderText = "EPF Deduction";
            this.epfReductionColumn.MinimumWidth = 120;
            this.epfReductionColumn.Name = "epfReductionColumn";
            this.epfReductionColumn.ReadOnly = true;
            this.epfReductionColumn.Width = 120;
            // 
            // netCommissionColumn
            // 
            this.netCommissionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.netCommissionColumn.DataPropertyName = "NetCommission";
            this.netCommissionColumn.HeaderText = "Net Commission";
            this.netCommissionColumn.MinimumWidth = 120;
            this.netCommissionColumn.Name = "netCommissionColumn";
            this.netCommissionColumn.ReadOnly = true;
            this.netCommissionColumn.Width = 120;
            // 
            // payeColumn
            // 
            this.payeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.payeColumn.DataPropertyName = "PAYE";
            this.payeColumn.HeaderText = "PAYE";
            this.payeColumn.MinimumWidth = 100;
            this.payeColumn.Name = "payeColumn";
            this.payeColumn.ReadOnly = true;
            // 
            // epfContributionColumn
            // 
            this.epfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.epfContributionColumn.DataPropertyName = "EpfContribution";
            this.epfContributionColumn.HeaderText = "EPF Contribution";
            this.epfContributionColumn.MinimumWidth = 120;
            this.epfContributionColumn.Name = "epfContributionColumn";
            this.epfContributionColumn.ReadOnly = true;
            this.epfContributionColumn.Width = 120;
            // 
            // etfContributionColumn
            // 
            this.etfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.etfContributionColumn.DataPropertyName = "EtfContribution";
            this.etfContributionColumn.HeaderText = "ETF Contribution";
            this.etfContributionColumn.MinimumWidth = 120;
            this.etfContributionColumn.Name = "etfContributionColumn";
            this.etfContributionColumn.ReadOnly = true;
            this.etfContributionColumn.Width = 120;
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
            // addressColumn
            // 
            this.addressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.addressColumn.DataPropertyName = "Address";
            this.addressColumn.HeaderText = "Address";
            this.addressColumn.MinimumWidth = 150;
            this.addressColumn.Name = "addressColumn";
            this.addressColumn.ReadOnly = true;
            this.addressColumn.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "LineNumber";
            this.dataGridViewTextBoxColumn1.HeaderText = "Line Number";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dEmployeeNumberColumn
            // 
            this.dEmployeeNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dEmployeeNumberColumn.DataPropertyName = "EmployeeNumber";
            this.dEmployeeNumberColumn.HeaderText = "Employee Number";
            this.dEmployeeNumberColumn.MinimumWidth = 120;
            this.dEmployeeNumberColumn.Name = "dEmployeeNumberColumn";
            this.dEmployeeNumberColumn.ReadOnly = true;
            this.dEmployeeNumberColumn.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "VirtualNumber";
            this.dataGridViewTextBoxColumn2.HeaderText = "Virtual Number";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "NIC";
            this.dataGridViewTextBoxColumn4.HeaderText = "NIC";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dTlOrBpoColumn
            // 
            this.dTlOrBpoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dTlOrBpoColumn.DataPropertyName = "TLorBPO";
            this.dTlOrBpoColumn.HeaderText = "TL/BPO";
            this.dTlOrBpoColumn.MinimumWidth = 100;
            this.dTlOrBpoColumn.Name = "dTlOrBpoColumn";
            this.dTlOrBpoColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dMemberStatusColumn
            // 
            this.dMemberStatusColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dMemberStatusColumn.DataPropertyName = "MemberStatus";
            this.dMemberStatusColumn.HeaderText = "Member Status";
            this.dMemberStatusColumn.MinimumWidth = 110;
            this.dMemberStatusColumn.Name = "dMemberStatusColumn";
            this.dMemberStatusColumn.ReadOnly = true;
            this.dMemberStatusColumn.Width = 110;
            // 
            // dDaysWorkedColumn
            // 
            this.dDaysWorkedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dDaysWorkedColumn.DataPropertyName = "DaysWorked";
            this.dDaysWorkedColumn.HeaderText = "Days Worked";
            this.dDaysWorkedColumn.MinimumWidth = 100;
            this.dDaysWorkedColumn.Name = "dDaysWorkedColumn";
            this.dDaysWorkedColumn.ReadOnly = true;
            // 
            // duplicatesSalesManagerColumn
            // 
            this.duplicatesSalesManagerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.duplicatesSalesManagerColumn.DataPropertyName = "SalesManager";
            this.duplicatesSalesManagerColumn.HeaderText = "Sales Manager";
            this.duplicatesSalesManagerColumn.MinimumWidth = 120;
            this.duplicatesSalesManagerColumn.Name = "duplicatesSalesManagerColumn";
            this.duplicatesSalesManagerColumn.ReadOnly = true;
            this.duplicatesSalesManagerColumn.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Bank";
            this.dataGridViewTextBoxColumn8.HeaderText = "Bank";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Branch";
            this.dataGridViewTextBoxColumn9.HeaderText = "Branch";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "AccountNumber";
            this.dataGridViewTextBoxColumn7.HeaderText = "Account Number";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // duplicatesGrossCommissionColumn
            // 
            this.duplicatesGrossCommissionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.duplicatesGrossCommissionColumn.DataPropertyName = "GrossCommission";
            this.duplicatesGrossCommissionColumn.HeaderText = "Gross Commission";
            this.duplicatesGrossCommissionColumn.MinimumWidth = 120;
            this.duplicatesGrossCommissionColumn.Name = "duplicatesGrossCommissionColumn";
            this.duplicatesGrossCommissionColumn.ReadOnly = true;
            this.duplicatesGrossCommissionColumn.Width = 120;
            // 
            // duplicatesEPFDeductionColumn
            // 
            this.duplicatesEPFDeductionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.duplicatesEPFDeductionColumn.DataPropertyName = "EPFDeduction";
            this.duplicatesEPFDeductionColumn.HeaderText = "EPF Deduction";
            this.duplicatesEPFDeductionColumn.MinimumWidth = 120;
            this.duplicatesEPFDeductionColumn.Name = "duplicatesEPFDeductionColumn";
            this.duplicatesEPFDeductionColumn.ReadOnly = true;
            this.duplicatesEPFDeductionColumn.Width = 120;
            // 
            // duplicatesNetCommissionColumn
            // 
            this.duplicatesNetCommissionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.duplicatesNetCommissionColumn.DataPropertyName = "NetCommission";
            this.duplicatesNetCommissionColumn.HeaderText = "Net Commission";
            this.duplicatesNetCommissionColumn.MinimumWidth = 120;
            this.duplicatesNetCommissionColumn.Name = "duplicatesNetCommissionColumn";
            this.duplicatesNetCommissionColumn.ReadOnly = true;
            this.duplicatesNetCommissionColumn.Width = 120;
            // 
            // dPayeColumn
            // 
            this.dPayeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dPayeColumn.DataPropertyName = "Paye";
            this.dPayeColumn.HeaderText = "PAYE";
            this.dPayeColumn.MinimumWidth = 100;
            this.dPayeColumn.Name = "dPayeColumn";
            this.dPayeColumn.ReadOnly = true;
            // 
            // dEpfContributionColumn
            // 
            this.dEpfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dEpfContributionColumn.DataPropertyName = "EpfContribution";
            this.dEpfContributionColumn.HeaderText = "EPF Contribution";
            this.dEpfContributionColumn.MinimumWidth = 120;
            this.dEpfContributionColumn.Name = "dEpfContributionColumn";
            this.dEpfContributionColumn.ReadOnly = true;
            this.dEpfContributionColumn.Width = 120;
            // 
            // dEtfContributionColumn
            // 
            this.dEtfContributionColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dEtfContributionColumn.DataPropertyName = "EtfContribution";
            this.dEtfContributionColumn.HeaderText = "ETF Contribution";
            this.dEtfContributionColumn.MinimumWidth = 120;
            this.dEtfContributionColumn.Name = "dEtfContributionColumn";
            this.dEtfContributionColumn.ReadOnly = true;
            this.dEtfContributionColumn.Width = 120;
            // 
            // duplicatesDateOfJoinColumn
            // 
            this.duplicatesDateOfJoinColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.duplicatesDateOfJoinColumn.DataPropertyName = "DateOfJoin";
            this.duplicatesDateOfJoinColumn.HeaderText = "Date Of Join";
            this.duplicatesDateOfJoinColumn.MinimumWidth = 100;
            this.duplicatesDateOfJoinColumn.Name = "duplicatesDateOfJoinColumn";
            this.duplicatesDateOfJoinColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn6.HeaderText = "Address";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 150;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // TcCommissionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.fileInfoLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.reloadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcCommissionsForm";
            this.Text = "TcMasterDataForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.duplicatesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label fileInfoLabel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView duplicatesDataGridView;
        private System.Windows.Forms.ComboBox filterComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label amountsLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn virtualNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlOrBpoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn daysWorkedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn salesManagerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn grossCommissionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epfReductionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn netCommissionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn payeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn epfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn etfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfJoinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEmployeeNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dTlOrBpoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMemberStatusColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDaysWorkedColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn duplicatesSalesManagerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn duplicatesGrossCommissionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn duplicatesEPFDeductionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn duplicatesNetCommissionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dPayeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEpfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dEtfContributionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn duplicatesDateOfJoinColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}