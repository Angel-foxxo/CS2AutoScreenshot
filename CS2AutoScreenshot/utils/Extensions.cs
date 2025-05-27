using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public static class Extensions
{
    public static int AdjustForDPI(this Control control, float value)
    {
        using (Graphics g = control.CreateGraphics())
        {
            float dpi = g.DpiX;  // DPI for horizontal axis
            return (int)(value * dpi / 96f);  // Adjust value based on DPI
        }
    }

    public static void RemoveAt<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            arr[a] = arr[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref arr, arr.Length - 1);
    }

    public static Color Brighten(this Control control, Color color, float brightnessFactor)
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

    public static GraphicsPath GetRoundedRect(this Control control, Rectangle rect, int radius)
    {
        var path = new GraphicsPath();
        radius = Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2);
        int diameter = radius * 2;
        var arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));

        // Top left arc
        path.AddArc(arcRect, 180, 90);
        // Top line
        path.AddLine(arcRect.Right, rect.Top, rect.Right - radius, rect.Top);
        // Top right arc
        arcRect.X = rect.Right - diameter;
        path.AddArc(arcRect, 270, 90);
        // Right side
        path.AddLine(rect.Right, arcRect.Bottom, rect.Right, rect.Bottom - radius);
        // Bottom right arc
        arcRect.Y = rect.Bottom - diameter;
        path.AddArc(arcRect, 0, 90);
        // Bottom side
        path.AddLine(arcRect.Left, rect.Bottom, rect.Left + radius, rect.Bottom);
        // Bottom left arc
        arcRect.X = rect.Left;
        path.AddArc(arcRect, 90, 90);
        // Left side
        path.AddLine(rect.Left, arcRect.Top, rect.Left, rect.Top + radius);

        return path;
    }

    public static GraphicsPath FitPathToRectangle(GraphicsPath path, RectangleF targetRectangle)
    {
        // Get the bounds of the current path
        RectangleF pathBounds = path.GetBounds();

        // Calculate the scale factors
        float scaleX = targetRectangle.Width / pathBounds.Width;
        float scaleY = targetRectangle.Height / pathBounds.Height;

        // Use the smaller scale to maintain aspect ratio
        float scale = Math.Min(scaleX, scaleY);

        // Calculate translation to center the path in the target rectangle
        float translateX = targetRectangle.X - pathBounds.X * scale +
                           (targetRectangle.Width - pathBounds.Width * scale) / 2;
        float translateY = targetRectangle.Y - pathBounds.Y * scale +
                           (targetRectangle.Height - pathBounds.Height * scale) / 2;

        // Create a transformation matrix
        Matrix transform = new Matrix();

        // Apply scaling and translation
        transform.Translate(translateX, translateY);
        transform.Scale(scale, scale);

        // Transform the path
        path.Transform(transform);

        return path;
    }
}
