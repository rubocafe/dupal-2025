using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Harshan Nishantha
// 2014-02-20

namespace ImageViewer
{
    public class TcCanvas
    {
        public Image Image { get; set; }
        private Image DrawingImage { get; set; }
        private Point targetOriginOffset { get; set; }
        private Size targetImageSize { get; set; }
        private Size customSize { get; set; }

        private float zoom { get; set; }
        private bool isZoomMode { get; set; }


        public TcCanvas()
        {
            Reset();
        }

        ~TcCanvas()
        {
            if (Image != null)
            {
                Image.Dispose();
            }

            if (DrawingImage != null)
            {
                DrawingImage.Dispose();
            }
        }

        public void ClearImage()
        {
            Image = null;
            DrawingImage = null;
        }

        public void Reset()
        {
            targetOriginOffset  = Point.Empty;
            targetImageSize     = Size.Empty;
            customSize          = Size.Empty;

            if (DrawingImage != null)
            {
                DrawingImage.Dispose();
                DrawingImage = null;
            }

            SetZoom(100);
        }

        public void SetZoom(float zoom)
        {
            this.zoom   = zoom;
            isZoomMode  = true;

            targetOriginOffset = Point.Empty;

            SetDrawingImage();
        }

        public void SetActualSize()
        {
            if (Image != null)
            {
                Reset();
                customSize = new Size(Image.Width, Image.Height);
                isZoomMode = false;
                SetDrawingImage();
            }
        }

        public void FitToWidth(int width)
        {
            if (Image != null)
            {
                Reset();
                float height = ((float)width / Image.Width) * Image.Height;
                customSize = new Size(width, (int)Math.Round(height));
                isZoomMode = false;
                SetDrawingImage();
            }
        }

        public void FitToHeight(int height)
        {
            if (Image != null)
            {
                Reset();
                float width = ((float)height / Image.Height) * Image.Width;
                customSize = new Size((int)Math.Round(width), height);
                isZoomMode = false;
                SetDrawingImage();
            }
        }

        public bool CanPan(Size viewerSize)
        {
            if (viewerSize.Width < targetImageSize.Width ||
                viewerSize.Height < targetImageSize.Height)
            {
                return true;
            }

            return false;
        }

        public void SetPan(Point start, Point end, Size viewerSize)
        {
            int xOffset = end.X - start.X;
            int yOffset = end.Y - start.Y;

            xOffset += targetOriginOffset.X;
            yOffset += targetOriginOffset.Y;

            if (viewerSize.Width > targetImageSize.Width)
            {
                xOffset = 0;
            }
            else
            {
                // Stop on right margin
                if ((xOffset + targetImageSize.Width) < viewerSize.Width)
                {
                    xOffset += (viewerSize.Width - (xOffset + targetImageSize.Width));
                }

                // Stop on left margin
                if (xOffset > 0)
                {
                    xOffset  = 0;
                }
            }

            if (viewerSize.Height > targetImageSize.Height)
            {
                yOffset = 0;
            }
            else
            {
                // Stop on Bottom margin
                if ((yOffset + targetImageSize.Height) < viewerSize.Height)
                {
                    yOffset += (viewerSize.Height - (yOffset + targetImageSize.Height));
                }

                // Stop on Top margin
                if (yOffset > 0)
                {
                    yOffset  = 0;
                }
            }

            targetOriginOffset = new Point(xOffset, yOffset);
        }

        private Rectangle GetTargetBounds()
        {
            Rectangle targetBounds = new Rectangle(0, 0, customSize.Width, customSize.Height);

            if (isZoomMode)
            {
                float ratio = zoom / 100f;
                targetBounds.Width  = (int)Math.Round(Image.Width * ratio);
                targetBounds.Height = (int)Math.Round(Image.Height * ratio);
            }

            return targetBounds;
        }

        private void SetDrawingImage()
        {
            if (DrawingImage != null)
            {
                DrawingImage.Dispose();
                DrawingImage = null;
            }

            if (Image != null)
            {
                Rectangle targetBounds = GetTargetBounds();

                Image destinationImage = new Bitmap(targetBounds.Width, targetBounds.Height);
                using (Graphics graphic = Graphics.FromImage(destinationImage))
                {
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    // Fill with background color
                    graphic.FillRectangle(new SolidBrush(System.Drawing.Color.White), targetBounds);

                    graphic.DrawImage(Image, targetBounds);
                }

                DrawingImage = destinationImage;
            }
        }

        public void Draw(Graphics graphics, Size canvasSize)
        {
            //graphics.Clear(Color.FromArgb(100, Color.Black));
            graphics.FillRectangle(new SolidBrush(Color.Black), 1, 1, canvasSize.Width - 2, canvasSize.Height - 2);

            PointF targetOrigin = new PointF(0, 0);

            if (DrawingImage != null)
            {   
                targetImageSize = DrawingImage.Size;

                if (canvasSize.Width > targetImageSize.Width)
                {
                    targetOrigin.X = (canvasSize.Width - targetImageSize.Width) / 2;
                }

                if (canvasSize.Height > targetImageSize.Height)
                {
                    targetOrigin.Y = (canvasSize.Height - targetImageSize.Height) / 2;
                }

                targetOrigin.X += targetOriginOffset.X;
                targetOrigin.Y += targetOriginOffset.Y;

                graphics.DrawImage(DrawingImage, targetOrigin);
            }
        }
    }
}
