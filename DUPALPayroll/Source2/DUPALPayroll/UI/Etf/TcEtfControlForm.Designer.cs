namespace DUPALPayroll.UI.Etf
{
    partial class TcEtfControlForm
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
            this.etfTabPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.etfTabPage);
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
            this.settingsTabPage.Text = "ETF Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            this.settingsTabPage.Enter += new System.EventHandler(this.settingsTabPage_Enter);
            // 
            // etfTabPage
            // 
            this.etfTabPage.Location = new System.Drawing.Point(4, 22);
            this.etfTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.etfTabPage.Name = "etfTabPage";
            this.etfTabPage.Size = new System.Drawing.Size(775, 686);
            this.etfTabPage.TabIndex = 0;
            this.etfTabPage.Text = "ETF";
            this.etfTabPage.UseVisualStyleBackColor = true;
            this.etfTabPage.Enter += new System.EventHandler(this.etfTabPage_Enter);
            // 
            // TcEtfControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 712);
            this.Controls.Add(this.tabControl);
            this.Name = "TcEtfControlForm";
            this.Text = "TcPayMasterForm";
            this.Load += new System.EventHandler(this.TcEtfControlForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage etfTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
    }
}