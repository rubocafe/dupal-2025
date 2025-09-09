namespace DUPALPayroll.UI.CommissionAgents.Tools.Decode
{
    partial class TcPayMasterDecodeForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileInfoLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lineNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationAccountNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.particularsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationAccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationBankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destinationBranchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDecimalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originatingAccountNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originatingAccountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originatingBankColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originatingBranchColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currencyCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditDebitCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tranIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnCodeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.returnDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securityFieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fillerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasErrorsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secondaryRowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(9, 656);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(13, 13);
            this.statusLabel.TabIndex = 11;
            this.statusLabel.Text = "--";
            // 
            // loadDataButton
            // 
            this.loadDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadDataButton.Location = new System.Drawing.Point(688, 651);
            this.loadDataButton.Name = "loadDataButton";
            this.loadDataButton.Size = new System.Drawing.Size(75, 23);
            this.loadDataButton.TabIndex = 10;
            this.loadDataButton.Text = "Load Data";
            this.loadDataButton.UseVisualStyleBackColor = true;
            this.loadDataButton.Click += new System.EventHandler(this.loadDataButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "dat";
            this.openFileDialog.FileName = "Temp.dat";
            this.openFileDialog.Filter = "PayMaster File|*.dat|All files|*.*";
            this.openFileDialog.Title = "Select PayMaster File";
            // 
            // fileInfoLabel
            // 
            this.fileInfoLabel.AutoSize = true;
            this.fileInfoLabel.Location = new System.Drawing.Point(9, 9);
            this.fileInfoLabel.Name = "fileInfoLabel";
            this.fileInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.fileInfoLabel.TabIndex = 13;
            this.fileInfoLabel.Text = "--";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Search:";
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(574, 6);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(189, 20);
            this.searchTextBox.TabIndex = 15;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
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
            this.destinationAccountNameColumn,
            this.particularsColumn,
            this.destinationAccountColumn,
            this.destinationBankColumn,
            this.destinationBranchColumn,
            this.amountDecimalColumn,
            this.amountColumn,
            this.originatingAccountNameColumn,
            this.originatingAccountColumn,
            this.originatingBankColumn,
            this.originatingBranchColumn,
            this.referenceColumn,
            this.valueDateColumn,
            this.currencyCodeColumn,
            this.creditDebitCodeColumn,
            this.tranIdColumn,
            this.transactionCodeColumn,
            this.returnCodeColumn,
            this.returnDateColumn,
            this.securityFieldColumn,
            this.fillerColumn,
            this.ErrorsColumn,
            this.hasErrorsColumn,
            this.secondaryRowColumn});
            this.dataGridView.Location = new System.Drawing.Point(12, 36);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(754, 596);
            this.dataGridView.TabIndex = 20;
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
            // destinationAccountNameColumn
            // 
            this.destinationAccountNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.destinationAccountNameColumn.DataPropertyName = "DestinationAccountName";
            this.destinationAccountNameColumn.HeaderText = "Destination Account Name";
            this.destinationAccountNameColumn.MinimumWidth = 170;
            this.destinationAccountNameColumn.Name = "destinationAccountNameColumn";
            this.destinationAccountNameColumn.ReadOnly = true;
            this.destinationAccountNameColumn.Width = 170;
            // 
            // particularsColumn
            // 
            this.particularsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.particularsColumn.DataPropertyName = "Particulars";
            this.particularsColumn.HeaderText = "Particulars";
            this.particularsColumn.MinimumWidth = 100;
            this.particularsColumn.Name = "particularsColumn";
            this.particularsColumn.ReadOnly = true;
            // 
            // destinationAccountColumn
            // 
            this.destinationAccountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.destinationAccountColumn.DataPropertyName = "DestinationAccount";
            this.destinationAccountColumn.HeaderText = "Destination Account";
            this.destinationAccountColumn.MinimumWidth = 130;
            this.destinationAccountColumn.Name = "destinationAccountColumn";
            this.destinationAccountColumn.ReadOnly = true;
            this.destinationAccountColumn.Width = 130;
            // 
            // destinationBankColumn
            // 
            this.destinationBankColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.destinationBankColumn.DataPropertyName = "DestinationBank";
            this.destinationBankColumn.HeaderText = "Destination Bank";
            this.destinationBankColumn.MinimumWidth = 120;
            this.destinationBankColumn.Name = "destinationBankColumn";
            this.destinationBankColumn.ReadOnly = true;
            this.destinationBankColumn.Width = 120;
            // 
            // destinationBranchColumn
            // 
            this.destinationBranchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.destinationBranchColumn.DataPropertyName = "DestinationBranch";
            this.destinationBranchColumn.HeaderText = "Destination Branch";
            this.destinationBranchColumn.MinimumWidth = 130;
            this.destinationBranchColumn.Name = "destinationBranchColumn";
            this.destinationBranchColumn.ReadOnly = true;
            this.destinationBranchColumn.Width = 130;
            // 
            // amountDecimalColumn
            // 
            this.amountDecimalColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.amountDecimalColumn.DataPropertyName = "AmountDecimal";
            this.amountDecimalColumn.HeaderText = "Amount (SLR)";
            this.amountDecimalColumn.MinimumWidth = 100;
            this.amountDecimalColumn.Name = "amountDecimalColumn";
            this.amountDecimalColumn.ReadOnly = true;
            // 
            // amountColumn
            // 
            this.amountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.amountColumn.DataPropertyName = "Amount";
            this.amountColumn.HeaderText = "Amount";
            this.amountColumn.MinimumWidth = 100;
            this.amountColumn.Name = "amountColumn";
            this.amountColumn.ReadOnly = true;
            // 
            // originatingAccountNameColumn
            // 
            this.originatingAccountNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.originatingAccountNameColumn.DataPropertyName = "OriginatingAccountName";
            this.originatingAccountNameColumn.HeaderText = "Originating Account Name";
            this.originatingAccountNameColumn.MinimumWidth = 170;
            this.originatingAccountNameColumn.Name = "originatingAccountNameColumn";
            this.originatingAccountNameColumn.ReadOnly = true;
            this.originatingAccountNameColumn.Width = 170;
            // 
            // originatingAccountColumn
            // 
            this.originatingAccountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.originatingAccountColumn.DataPropertyName = "OriginatingAccount";
            this.originatingAccountColumn.HeaderText = "Originating Account";
            this.originatingAccountColumn.MinimumWidth = 130;
            this.originatingAccountColumn.Name = "originatingAccountColumn";
            this.originatingAccountColumn.ReadOnly = true;
            this.originatingAccountColumn.Width = 130;
            // 
            // originatingBankColumn
            // 
            this.originatingBankColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.originatingBankColumn.DataPropertyName = "OriginatingBank";
            this.originatingBankColumn.HeaderText = "Originating Bank";
            this.originatingBankColumn.MinimumWidth = 120;
            this.originatingBankColumn.Name = "originatingBankColumn";
            this.originatingBankColumn.ReadOnly = true;
            this.originatingBankColumn.Width = 120;
            // 
            // originatingBranchColumn
            // 
            this.originatingBranchColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.originatingBranchColumn.DataPropertyName = "OriginatingBranch";
            this.originatingBranchColumn.HeaderText = "Originating Branch";
            this.originatingBranchColumn.MinimumWidth = 120;
            this.originatingBranchColumn.Name = "originatingBranchColumn";
            this.originatingBranchColumn.ReadOnly = true;
            this.originatingBranchColumn.Width = 120;
            // 
            // referenceColumn
            // 
            this.referenceColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.referenceColumn.DataPropertyName = "Reference";
            this.referenceColumn.HeaderText = "Reference";
            this.referenceColumn.MinimumWidth = 130;
            this.referenceColumn.Name = "referenceColumn";
            this.referenceColumn.ReadOnly = true;
            this.referenceColumn.Width = 130;
            // 
            // valueDateColumn
            // 
            this.valueDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.valueDateColumn.DataPropertyName = "ValueDate";
            this.valueDateColumn.HeaderText = "Value Date";
            this.valueDateColumn.MinimumWidth = 100;
            this.valueDateColumn.Name = "valueDateColumn";
            this.valueDateColumn.ReadOnly = true;
            // 
            // currencyCodeColumn
            // 
            this.currencyCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.currencyCodeColumn.DataPropertyName = "CurrencyCode";
            this.currencyCodeColumn.HeaderText = "Currency Code";
            this.currencyCodeColumn.MinimumWidth = 100;
            this.currencyCodeColumn.Name = "currencyCodeColumn";
            this.currencyCodeColumn.ReadOnly = true;
            // 
            // creditDebitCodeColumn
            // 
            this.creditDebitCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.creditDebitCodeColumn.DataPropertyName = "CreditDebitCode";
            this.creditDebitCodeColumn.HeaderText = "Credit Debit Code";
            this.creditDebitCodeColumn.MinimumWidth = 130;
            this.creditDebitCodeColumn.Name = "creditDebitCodeColumn";
            this.creditDebitCodeColumn.ReadOnly = true;
            this.creditDebitCodeColumn.Width = 130;
            // 
            // tranIdColumn
            // 
            this.tranIdColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tranIdColumn.DataPropertyName = "TranId";
            this.tranIdColumn.HeaderText = "Tran Id";
            this.tranIdColumn.MinimumWidth = 70;
            this.tranIdColumn.Name = "tranIdColumn";
            this.tranIdColumn.ReadOnly = true;
            this.tranIdColumn.Width = 70;
            // 
            // transactionCodeColumn
            // 
            this.transactionCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.transactionCodeColumn.DataPropertyName = "TransactionCode";
            this.transactionCodeColumn.HeaderText = "Transaction Code";
            this.transactionCodeColumn.MinimumWidth = 120;
            this.transactionCodeColumn.Name = "transactionCodeColumn";
            this.transactionCodeColumn.ReadOnly = true;
            this.transactionCodeColumn.Width = 120;
            // 
            // returnCodeColumn
            // 
            this.returnCodeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.returnCodeColumn.DataPropertyName = "ReturnCode";
            this.returnCodeColumn.HeaderText = "Return Code";
            this.returnCodeColumn.MinimumWidth = 100;
            this.returnCodeColumn.Name = "returnCodeColumn";
            this.returnCodeColumn.ReadOnly = true;
            // 
            // returnDateColumn
            // 
            this.returnDateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.returnDateColumn.DataPropertyName = "ReturnDate";
            this.returnDateColumn.HeaderText = "Return Date";
            this.returnDateColumn.MinimumWidth = 100;
            this.returnDateColumn.Name = "returnDateColumn";
            this.returnDateColumn.ReadOnly = true;
            // 
            // securityFieldColumn
            // 
            this.securityFieldColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.securityFieldColumn.DataPropertyName = "SecurityField";
            this.securityFieldColumn.HeaderText = "Security Field";
            this.securityFieldColumn.MinimumWidth = 100;
            this.securityFieldColumn.Name = "securityFieldColumn";
            this.securityFieldColumn.ReadOnly = true;
            // 
            // fillerColumn
            // 
            this.fillerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fillerColumn.DataPropertyName = "Filler";
            this.fillerColumn.HeaderText = "Filler";
            this.fillerColumn.MinimumWidth = 50;
            this.fillerColumn.Name = "fillerColumn";
            this.fillerColumn.ReadOnly = true;
            // 
            // ErrorsColumn
            // 
            this.ErrorsColumn.DataPropertyName = "Errors";
            this.ErrorsColumn.HeaderText = "Errors";
            this.ErrorsColumn.Name = "ErrorsColumn";
            this.ErrorsColumn.ReadOnly = true;
            this.ErrorsColumn.Visible = false;
            // 
            // hasErrorsColumn
            // 
            this.hasErrorsColumn.DataPropertyName = "HasErrors";
            this.hasErrorsColumn.HeaderText = "Has Errors";
            this.hasErrorsColumn.Name = "hasErrorsColumn";
            this.hasErrorsColumn.ReadOnly = true;
            this.hasErrorsColumn.Visible = false;
            // 
            // secondaryRowColumn
            // 
            this.secondaryRowColumn.DataPropertyName = "SecondaryRow";
            this.secondaryRowColumn.HeaderText = "Secondary Row";
            this.secondaryRowColumn.Name = "secondaryRowColumn";
            this.secondaryRowColumn.ReadOnly = true;
            this.secondaryRowColumn.Visible = false;
            // 
            // TcPayMasterDecodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 686);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.fileInfoLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.loadDataButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TcPayMasterDecodeForm";
            this.Text = "TcPayMasterDecodeForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button loadDataButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label fileInfoLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationAccountNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn particularsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationAccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationBankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destinationBranchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDecimalColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originatingAccountNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originatingAccountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originatingBankColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originatingBranchColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currencyCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditDebitCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tranIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactionCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnCodeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn returnDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn securityFieldColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fillerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hasErrorsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn secondaryRowColumn;
    }
}