namespace DUPALPayroll.UI.Epf
{
    partial class TcEpfControlForm
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
            this.epfTabPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.settingsTabPage);
            this.tabControl.Controls.Add(this.epfTabPage);
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
            this.settingsTabPage.Text = "EPF Settings";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            this.settingsTabPage.Enter += new System.EventHandler(this.settingsTabPage_Enter);
            // 
            // epfTabPage
            // 
            this.epfTabPage.Location = new System.Drawing.Point(4, 22);
            this.epfTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.epfTabPage.Name = "epfTabPage";
            this.epfTabPage.Size = new System.Drawing.Size(775, 686);
            this.epfTabPage.TabIndex = 0;
            this.epfTabPage.Text = "EPF";
            this.epfTabPage.UseVisualStyleBackColor = true;
            this.epfTabPage.Enter += new System.EventHandler(this.epfTabPage_Enter);
            // 
            // TcEpfControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 712);
            this.Controls.Add(this.tabControl);
            this.Name = "TcEpfControlForm";
            this.Text = "TcPayMasterForm";
            this.Load += new System.EventHandler(this.TcEpfControlForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage epfTabPage;
        private System.Windows.Forms.TabPage settingsTabPage;
    }
}