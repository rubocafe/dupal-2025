namespace DUPALPayroll.UI.Tools
{
    partial class TcToolsForm
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
            this.decodePayMasterTabPage = new System.Windows.Forms.TabPage();
            this.comparePayMasterTabPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.decodePayMasterTabPage);
            this.tabControl.Controls.Add(this.comparePayMasterTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(783, 712);
            this.tabControl.TabIndex = 0;
            // 
            // decodePayMasterTabPage
            // 
            this.decodePayMasterTabPage.Location = new System.Drawing.Point(4, 22);
            this.decodePayMasterTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.decodePayMasterTabPage.Name = "decodePayMasterTabPage";
            this.decodePayMasterTabPage.Size = new System.Drawing.Size(775, 686);
            this.decodePayMasterTabPage.TabIndex = 6;
            this.decodePayMasterTabPage.Text = "Decode PayMaster";
            this.decodePayMasterTabPage.UseVisualStyleBackColor = true;
            this.decodePayMasterTabPage.Enter += new System.EventHandler(this.decodePayMasterTabPage_Enter);
            // 
            // comparePayMasterTabPage
            // 
            this.comparePayMasterTabPage.Location = new System.Drawing.Point(4, 22);
            this.comparePayMasterTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.comparePayMasterTabPage.Name = "comparePayMasterTabPage";
            this.comparePayMasterTabPage.Size = new System.Drawing.Size(775, 686);
            this.comparePayMasterTabPage.TabIndex = 7;
            this.comparePayMasterTabPage.Text = "Compare PayMaster";
            this.comparePayMasterTabPage.UseVisualStyleBackColor = true;
            this.comparePayMasterTabPage.Enter += new System.EventHandler(this.comparePayMasterTabPage_Enter);
            // 
            // TcToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 712);
            this.Controls.Add(this.tabControl);
            this.Name = "TcToolsForm";
            this.Text = "TcPayMasterForm";
            this.Load += new System.EventHandler(this.TcToolsForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage decodePayMasterTabPage;
        private System.Windows.Forms.TabPage comparePayMasterTabPage;
    }
}