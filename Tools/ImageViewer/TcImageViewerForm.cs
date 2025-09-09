using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Harshan Nishantha
// 2012-02-20

namespace ImageViewer
{
    public partial class TcImageViewerForm : Form
    {
        private TcCanvas Canvas { get; set; }

        public TcImageViewerForm()
        {
            InitializeComponent();

            Canvas = new TcCanvas();
        }

        private void actualSizeButton_Click(object sender, EventArgs e)
        {
            try
            {
                ShowInActualSize();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ShowInActualSize()
        {
            zoomTrackBar.Value = 100;
            Canvas.SetActualSize();
            pictureBox.Invalidate();
        }

        private void fitToWidthButton_Click(object sender, EventArgs e)
        {
            try
            {
                Canvas.FitToWidth(pictureBox.Size.Width);
                pictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void fitToHeightButton_Click(object sender, EventArgs e)
        {
            try
            {
                Canvas.FitToHeight(pictureBox.Size.Height);
                pictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void zoomTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Canvas.SetZoom(zoomTrackBar.Value);
                pictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Canvas.Draw(e.Graphics, pictureBox.Size);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void TcImageViewerForm_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error");
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "";

                string filter           = "";
                string allImmagesFilter = "";
                string imagesFilter     = "";
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                foreach (ImageCodecInfo codec in codecs)
                {
                    allImmagesFilter += string.Format("{0}", codec.FilenameExtension);
                    imagesFilter += string.Format("{0} Files ({1})|{1}|", codec.FormatDescription, codec.FilenameExtension);
                }

                filter = string.Format("All Images|{0}|{1}All Files|*.*", allImmagesFilter, imagesFilter);

                openFileDialog.Filter = filter;

                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string file = openFileDialog.FileName;

                    Canvas.Image = Image.FromFile(file);
                    ShowInActualSize();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Canvas.ClearImage();

            pictureBox.Invalidate();
        }

        #region Pan

        private bool isPanning = false;
        private Point startingPoint = Point.Empty;
        private Point endPoint = Point.Empty;

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isPanning = true;
            startingPoint = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Canvas != null)
            {
                if (isPanning)
                {
                    endPoint = e.Location;
                    Canvas.SetPan(startingPoint, endPoint, pictureBox.Size);
                    pictureBox.Invalidate();
                    startingPoint = endPoint;
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPanning)
            {
                isPanning = false;
                startingPoint = Point.Empty;
                endPoint = Point.Empty;
            }
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            if (Canvas != null)
            {
                if (Canvas.CanPan(pictureBox.Size))
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        #endregion
    }
}
