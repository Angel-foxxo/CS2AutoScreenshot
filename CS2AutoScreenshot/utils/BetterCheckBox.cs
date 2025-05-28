using CS2AutoScreenshot;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RadGenGUI.Controls
{
    public class BetterCheckBox : CheckBox
    {
        public Color FillColor = Color.White;
        public Color BorderColor = Color.Black;
        public Color AccentColor = Color.Blue;

        public Size CheckBoxSize = new(12, 12);

        public int BorderWidth = 1;

        private bool Pressed;
        private bool Hovered;

        public BetterCheckBox()
        {
            Padding = new Padding(0);
            Margin = new Padding(0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var foreColor = ForeColor;
            var fillColor = FillColor;
            var borderColor = BorderColor;
            var accentColor = AccentColor;


            if(!Enabled)
            {
                foreColor = this.Brighten(foreColor, 0.7f);
                fillColor = this.Brighten(fillColor, 0.7f);
                borderColor = this.Brighten(borderColor, 0.7f);
                accentColor = this.Brighten(accentColor, 0.7f);
            }

            var checkBoxRect = new Rectangle(ClientRectangle.Left, (ClientRectangle.Height - CheckBoxSize.Height) / 2,
            this.AdjustForDPI(CheckBoxSize.Width), this.AdjustForDPI(CheckBoxSize.Height));

            InvokePaintBackground(this, e);

            using var backBrush = new SolidBrush(fillColor);

            if (Pressed)
            {
                backBrush.Color = borderColor;
            }

            using var checkPen = new Pen(foreColor, this.AdjustForDPI(1));

            using var borderPen = new Pen(borderColor);
            if (Hovered)
            {
                borderPen.Color = accentColor;
            }
            borderPen.Width = BorderWidth;


            e.Graphics.FillRectangle(backBrush, checkBoxRect);
            e.Graphics.DrawRectangle(borderPen, checkBoxRect);

            if (Checked)
            {
                var checkMiddle = new Point(checkBoxRect.Left + checkBoxRect.Height / 2 - this.AdjustForDPI(1.1f),
                checkBoxRect.Top + checkBoxRect.Width / 2 + this.AdjustForDPI(3));

                var checkLeft = new Point(checkBoxRect.Left + 2,
                    checkBoxRect.Top + checkBoxRect.Width / 2);

                var checkRight = new Point(checkBoxRect.Right - 2,
                    checkBoxRect.Top + this.AdjustForDPI(3));

                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(checkPen, checkMiddle, checkLeft);
                e.Graphics.DrawLine(checkPen, checkMiddle, checkRight);
            }

            var textSize = TextRenderer.MeasureText(Text, Font);
            var textRect = new Rectangle(checkBoxRect.X + checkBoxRect.Width, checkBoxRect.Y + ((checkBoxRect.Height - textSize.Height) / 2), checkBoxRect.Width + textSize.Width, textSize.Height);

            using var foreBrush = new SolidBrush(foreColor);
            TextRenderer.DrawText(e.Graphics, Text, Font, textRect, foreColor);
        }

        protected override void OnMouseEnter(EventArgs eventargs)
        {
            base.OnMouseEnter(eventargs);

            Hovered = true;
        }

        protected override void OnMouseLeave(EventArgs eventargs)
        {
            base.OnMouseLeave(eventargs);

            Hovered = false;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            Pressed = true;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);

            Pressed = false;

            Invalidate();
        }
    }
}
