namespace DUPALPayroll.UI.CommissionAgents
{
    partial class TcCommissionAgentsForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.masterDataTabPage = new System.Windows.Forms.TabPage();
            this.banksAndBranchesTabPage = new System.Windows.Forms.TabPage();
            this.commissionsTabPage = new System.Windows.Forms.TabPage();
            this.commissionsHeldTabPage = new System.Windows.Forms.TabPage();
            this.analyzeTabPage = new System.Windows.Forms.TabPage();
            this.payMasterTabPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.masterDataTabPage);
            this.tabControl.Controls.Add(this.banksAndBranchesTabPage);
            this.tabControl.Controls.Add(this.commissionsTabPage);
            this.tabControl.Controls.Add(this.commissionsHeldTabPage);
            this.tabControl.Controls.Add(this.analyzeTabPage);
            this.tabControl.Controls.Add(this.payMasterTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(783, 712);
            this.tabControl.TabIndex = 0;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Size = new System.Drawing.Size(775, 686);
            this.settingsTabPage.TabIndex = 6;
            this.settingsTabPage.Text = "Commission Agents";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            this.settingsTabPage.Enter += new System.EventHandler(this.settingsTabPage_Enter);
            // 
            // masterDataTabPage
            // 
            this.masterDataTabPage.Location = new System.Drawing.Point(4, 22);
            this.masterDataTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.masterDataTabPage.Name = "masterDataTabPage";
            this.masterDataTabPage.Size = new System.Drawing.Size(775, 686);
            this.masterDataTabPage.TabIndex = 0;
            this.masterDataTabPage.Text = "Master Data";
            this.masterDataTabPage.UseVisualStyleBackColor = true;
            this.masterDataTabPage.Enter += new System.EventHandler(this.masterDataTabPage_Enter);
            // 
            // banksAndBranchesTabPage
            // 
            this.banksAndBranchesTabPage.Location = new System.Drawing.Point(4, 22);
            this.banksAndBranchesTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.banksAndBranchesTabPage.Name = "banksAndBranchesTabPage";
            this.banksAndBranchesTabPage.Size = new System.Drawing.Size(775, 686);
            this.banksAndBranchesTabPage.TabIndex = 1;
            this.banksAndBranchesTabPage.Text = "Banks and Branches";
            this.banksAndBranchesTabPage.UseVisualStyleBackColor = true;
            this.banksAndBranchesTabPage.Enter += new System.EventHandler(this.banksAndBranchesTabPage_Enter);
            // 
            // commissionsTabPage
            // 
            this.commissionsTabPage.Location = new System.Drawing.Point(4, 22);
            this.commissionsTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.commissionsTabPage.Name = "commissionsTabPage";
            this.commissionsTabPage.Size = new System.Drawing.Size(775, 686);
            this.commissionsTabPage.TabIndex = 2;
            this.commissionsTabPage.Text = "Commissions";
            this.commissionsTabPage.UseVisualStyleBackColor = true;
            this.commissionsTabPage.Enter += new System.EventHandler(this.commissionsTabPage_Enter);
            // 
            // commissionsHeldTabPage
            // 
            this.commissionsHeldTabPage.Location = new System.Drawing.Point(4, 22);
            this.commissionsHeldTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.commissionsHeldTabPage.Name = "commissionsHeldTabPage";
            this.commissionsHeldTabPage.Size = new System.Drawing.Size(775, 686);
            this.commissionsHeldTabPage.TabIndex = 3;
            this.commissionsHeldTabPage.Text = "Commissions Held";
            this.commissionsHeldTabPage.UseVisualStyleBackColor = true;
            this.commissionsHeldTabPage.Enter += new System.EventHandler(this.commissionHeldTabPage_Enter);
            // 
            // analyzeTabPage
            // 
            this.analyzeTabPage.Location = new System.Drawing.Point(4, 22);
            this.analyzeTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeTabPage.Name = "analyzeTabPage";
            this.analyzeTabPage.Size = new System.Drawing.Size(775, 686);
            this.analyzeTabPage.TabIndex = 5;
            this.analyzeTabPage.Text = "Analysis";
            this.analyzeTabPage.UseVisualStyleBackColor = true;
            this.analyzeTabPage.Enter += new System.EventHandler(this.analyzeTabPage_Enter);
            // 
            // payMasterTabPage
            // 
            this.payMasterTabPage.Location = new System.Drawing.Point(4, 22);
            this.payMasterTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.payMasterTabPage.Name = "payMasterTabPage";
            this.payMasterTabPage.Size = new System.Drawing.Size(775, 686);
            this.payMasterTabPage.TabIndex = 4;
            this.payMasterTabPage.Text = "Pay Master";
            this.payMasterTabPage.UseVisualStyleBackColor = true;
            this.payMasterTabPage.Enter += new System.EventHandler(this.payMasterTabPage_Enter);
            // 
            // TcCommissionAgentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 712);
            this.Controls.Add(this.tabControl);
            this.Name = "TcCommissionAgentsForm";
            this.Text = "TcPayMasterForm";
            this.Load += new System.EventHandler(this.TcCommissionAgentsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage masterDataTabPage;
        private System.Windows.Forms.TabPage banksAndBranchesTabPage;
        private System.Windows.Forms.TabPage commissionsTabPage;
        private System.Windows.Forms.TabPage commissionsHeldTabPage;
        private System.Windows.Forms.TabPage payMasterTabPage;
        private System.Windows.Forms.TabPage analyzeTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
    }
}