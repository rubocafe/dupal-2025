namespace DUPALPayroll.UI.CareTakers.MasterData
{
    partial class TcCareTakersMasterForm
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
            this.siteNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteEngineerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameWithInitialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nicColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSiteNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSiteCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dSiteEngineerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.siteNameColumn,
            this.siteCodeColumn,
            this.siteEngineerColumn,
            this.nameWithInitialsColumn,
            this.nicColumn,
            this.bankColumn,
            this.branchColumn,
            this.bankCodeColumn,
            this.branchCodeColumn,
            this.accountNumberColumn,
            this.addressColumn});
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
            this.dSiteNameColumn,
            this.dSiteCodeColumn,
            this.dSiteEngineerColumn,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn13});
            this.duplicatesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duplicatesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.duplicatesDataGridView.Name = "duplicatesDataGridView";
            this.duplicatesDataGridView.ReadOnly = true;
            this.duplicatesDataGridView.RowHeadersVisible = false;
            this.duplicatesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.duplicatesDataGridView.Size = new System.Drawing.Size(751, 107);
            this.duplicatesDataGridView.TabIndex = 10;
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
            // siteNameColumn
            // 
            this.siteNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.siteNameColumn.DataPropertyName = "SiteName";
            this.siteNameColumn.HeaderText = "Site Name";
            this.siteNameColumn.MinimumWidth = 120;
            this.siteNameColumn.Name = "siteNameColumn";
            this.siteNameColumn.ReadOnly = true;
            this.siteNameColumn.Width = 120;
            // 
            // siteCodeColumn
            // 
            this.siteCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.siteCodeColumn.DataPropertyName = "SiteCode";
            this.siteCodeColumn.HeaderText = "Site Code";
            this.siteCodeColumn.MinimumWidth = 120;
            this.siteCodeColumn.Name = "siteCodeColumn";
            this.siteCodeColumn.ReadOnly = true;
            this.siteCodeColumn.Width = 120;
            // 
            // siteEngineerColumn
            // 
            this.siteEngineerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.siteEngineerColumn.DataPropertyName = "SiteEngineer";
            this.siteEngineerColumn.HeaderText = "Site Engineer";
            this.siteEngineerColumn.MinimumWidth = 120;
            this.siteEngineerColumn.Name = "siteEngineerColumn";
            this.siteEngineerColumn.ReadOnly = true;
            this.siteEngineerColumn.Width = 120;
            // 
            // nameWithInitialsColumn
            // 
            this.nameWithInitialsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nameWithInitialsColumn.DataPropertyName = "Name";
            this.nameWithInitialsColumn.HeaderText = "Name";
            this.nameWithInitialsColumn.MinimumWidth = 140;
            this.nameWithInitialsColumn.Name = "nameWithInitialsColumn";
            this.nameWithInitialsColumn.ReadOnly = true;
            this.nameWithInitialsColumn.Width = 140;
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
            this.branchColumn.MinimumWidth = 100;
            this.branchColumn.Name = "branchColumn";
            this.branchColumn.ReadOnly = true;
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
            // addressColumn
            // 
            this.addressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.addressColumn.DataPropertyName = "Address";
            this.addressColumn.HeaderText = "Address";
            this.addressColumn.MinimumWidth = 100;
            this.addressColumn.Name = "addressColumn";
            this.addressColumn.ReadOnly = true;
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
            // dSiteNameColumn
            // 
            this.dSiteNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dSiteNameColumn.DataPropertyName = "SiteName";
            this.dSiteNameColumn.HeaderText = "Site Name";
            this.dSiteNameColumn.MinimumWidth = 120;
            this.dSiteNameColumn.Name = "dSiteNameColumn";
            this.dSiteNameColumn.ReadOnly = true;
            this.dSiteNameColumn.Width = 120;
            // 
            // dSiteCodeColumn
            // 
            this.dSiteCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dSiteCodeColumn.DataPropertyName = "SiteCode";
            this.dSiteCodeColumn.HeaderText = "Site Code";
            this.dSiteCodeColumn.MinimumWidth = 120;
            this.dSiteCodeColumn.Name = "dSiteCodeColumn";
            this.dSiteCodeColumn.ReadOnly = true;
            this.dSiteCodeColumn.Width = 120;
            // 
            // dSiteEngineerColumn
            // 
            this.dSiteEngineerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dSiteEngineerColumn.DataPropertyName = "SiteEngineer";
            this.dSiteEngineerColumn.HeaderText = "Site Engineer";
            this.dSiteEngineerColumn.MinimumWidth = 120;
            this.dSiteEngineerColumn.Name = "dSiteEngineerColumn";
            this.dSiteEngineerColumn.ReadOnly = true;
            this.dSiteEngineerColumn.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 140;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 140;
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
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Bank";
            this.dataGridViewTextBoxColumn7.HeaderText = "Bank";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Branch";
            this.dataGridViewTextBoxColumn9.HeaderText = "Branch";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "BankCode";
            this.dataGridViewTextBoxColumn8.HeaderText = "Bank Code";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "BranchCode";
            this.dataGridViewTextBoxColumn10.HeaderText = "Branch Code";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "AccountNumber";
            this.dataGridViewTextBoxColumn6.HeaderText = "Account Number";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Address";
            this.dataGridViewTextBoxColumn13.HeaderText = "Address";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // TcCareTakersMasterForm
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
            this.Name = "TcCareTakersMasterForm";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn siteNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteEngineerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameWithInitialsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nicColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSiteNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSiteCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dSiteEngineerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    }
}