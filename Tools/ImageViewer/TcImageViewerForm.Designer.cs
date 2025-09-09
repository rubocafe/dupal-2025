namespace ImageViewer
{
    partial class TcImageViewerForm
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.zoomTrackBar = new System.Windows.Forms.TrackBar();
            this.actualSizeButton = new System.Windows.Forms.Button();
            this.fitToWidthButton = new System.Windows.Forms.Button();
            this.fitToHeightButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(0, -1);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(905, 582);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox.MouseHover += new System.EventHandler(this.pictureBox_MouseHover);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openButton.Location = new System.Drawing.Point(12, 587);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.Location = new System.Drawing.Point(93, 587);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // zoomTrackBar
            // 
            this.zoomTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomTrackBar.LargeChange = 100;
            this.zoomTrackBar.Location = new System.Drawing.Point(788, 587);
            this.zoomTrackBar.Maximum = 500;
            this.zoomTrackBar.Minimum = 10;
            this.zoomTrackBar.Name = "zoomTrackBar";
            this.zoomTrackBar.Size = new System.Drawing.Size(104, 45);
            this.zoomTrackBar.SmallChange = 100;
            this.zoomTrackBar.TabIndex = 3;
            this.zoomTrackBar.TickFrequency = 50;
            this.zoomTrackBar.Value = 100;
            this.zoomTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoomTrackBar_MouseUp);
            // 
            // actualSizeButton
            // 
            this.actualSizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.actualSizeButton.Location = new System.Drawing.Point(545, 587);
            this.actualSizeButton.Name = "actualSizeButton";
            this.actualSizeButton.Size = new System.Drawing.Size(75, 23);
            this.actualSizeButton.TabIndex = 4;
            this.actualSizeButton.Text = "Actual Size";
            this.actualSizeButton.UseVisualStyleBackColor = true;
            this.actualSizeButton.Click += new System.EventHandler(this.actualSizeButton_Click);
            // 
            // fitToWidthButton
            // 
            this.fitToWidthButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fitToWidthButton.Location = new System.Drawing.Point(626, 587);
            this.fitToWidthButton.Name = "fitToWidthButton";
            this.fitToWidthButton.Size = new System.Drawing.Size(75, 23);
            this.fitToWidthButton.TabIndex = 5;
            this.fitToWidthButton.Text = "Fit to Width";
            this.fitToWidthButton.UseVisualStyleBackColor = true;
            this.fitToWidthButton.Click += new System.EventHandler(this.fitToWidthButton_Click);
            // 
            // fitToHeightButton
            // 
            this.fitToHeightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fitToHeightButton.Location = new System.Drawing.Point(707, 587);
            this.fitToHeightButton.Name = "fitToHeightButton";
            this.fitToHeightButton.Size = new System.Drawing.Size(75, 23);
            this.fitToHeightButton.TabIndex = 6;
            this.fitToHeightButton.Text = "Fit to Height";
            this.fitToHeightButton.UseVisualStyleBackColor = true;
            this.fitToHeightButton.Click += new System.EventHandler(this.fitToHeightButton_Click);
            // 
            // TcImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 622);
            this.Controls.Add(this.fitToHeightButton);
            this.Controls.Add(this.fitToWidthButton);
            this.Controls.Add(this.actualSizeButton);
            this.Controls.Add(this.zoomTrackBar);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.pictureBox);
            this.MinimumSize = new System.Drawing.Size(920, 660);
            this.Name = "TcImageViewerForm";
            this.Text = " Image Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.TcImageViewerForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Button actualSizeButton;
        private System.Windows.Forms.Button fitToWidthButton;
        private System.Windows.Forms.Button fitToHeightButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

