using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RadGenGUI.Controls
{
    public class BetterButton : Button
    {
        private bool Hovered = false;
        private bool Clicked = false;

        public bool ForceClicked = false;

        private Color adjustedForeColor;
        public Color AdjustedForeColor
        {
            get => adjustedForeColor;
        }

        private Color adjustedBackColor;
        public Color AdjustedBackColor
        {
            get => adjustedBackColor;
        }

        public Color ClickedBackColor = Color.Gray;
        public int CornerRadius = 5;

        public BetterButton()
        {
            adjustedBackColor = BackColor;
            adjustedForeColor = ForeColor;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Cursor = Cursors.Hand;
            Hovered = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Cursor= Cursors.Default;
            Hovered = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            Clicked = true;
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            Clicked = false;
            base.OnMouseUp(mevent);
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            adjustedBackColor = BackColor;
            adjustedForeColor = ForeColor;

            pevent.Graphics.Clear(Parent.BackColor);
            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            pevent.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var backBrush = new SolidBrush(adjustedBackColor);
            using var textBrush = new SolidBrush(adjustedForeColor);

            if (Hovered)
            {
                adjustedBackColor = Brighten(BackColor, 1.3f);
            }

            if(Clicked || ForceClicked)
            {
                adjustedBackColor = ClickedBackColor;
            }

            if (!Enabled)
            {
                adjustedBackColor = Brighten(adjustedBackColor, 0.6f);
                adjustedForeColor = Brighten(adjustedForeColor, 0.6f);
            }


            backBrush.Color = adjustedBackColor;
            textBrush.Color = adjustedForeColor;

            FillRoundedRectangle(pevent.Graphics, backBrush, rect, this.AdjustForDPI(CornerRadius));

            var stringFormat = new StringFormat
            {
                FormatFlags = StringFormatFlags.NoWrap,
            };

            if (TextAlign == ContentAlignment.MiddleCenter)
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
            }
            if (TextAlign == ContentAlignment.MiddleLeft)
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
            }

            pevent.Graphics.DrawString(Text, Font, textBrush, ClientRectangle, stringFormat);
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));
            if (brush == null)
                throw new ArgumentNullException(nameof(brush));

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        public static Color Brighten(Color color, float brightnessFactor)
        {
            // Ensure brightnessFactor is within valid range (can be extended if necessary)
            brightnessFactor = Math.Max(0, brightnessFactor);

            // Adjust each color channel by multiplying it with the brightness factor
            int r = (int)(color.R * brightnessFactor);
            int g = (int)(color.G * brightnessFactor);
            int b = (int)(color.B * brightnessFactor);

            // Ensure values don't exceed 255
            r = Math.Min(255, r);
            g = Math.Min(255, g);
            b = Math.Min(255, b);

            // Return the new color
            return Color.FromArgb(color.A, r, g, b);
        }
    }
}
