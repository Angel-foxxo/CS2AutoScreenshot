using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

public class SettingsTableLayoutPanel : TableLayoutPanel
{
    private Color _gridLineColor = Color.Gray;
    private int _gridLineWidth = 1;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color GridLineColor
    {
        get { return _gridLineColor; }
        set
        {
            _gridLineColor = value;
            Invalidate(); // Trigger repaint
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int GridLineWidth
    {
        get { return _gridLineWidth; }
        set
        {
            _gridLineWidth = value;
            Invalidate(); // Trigger repaint
        }
    }

    public SettingsTableLayoutPanel()
    {
        HorizontalLinePadding = this.AdjustForDPI(10);
        VerticalLinePadding = this.AdjustForDPI(5);
    }

    private int HorizontalLinePadding = 0;
    private int VerticalLinePadding = 0;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        using (Pen pen = new Pen(_gridLineColor, _gridLineWidth))
        {
            // Draw horizontal lines (for row separators)
            int y = 0;
            for (int row = 0; row < RowCount - 1; row++)
            {
                y += GetRowHeight(row);
                g.DrawLine(pen, 0 + HorizontalLinePadding, y, Width - HorizontalLinePadding, y);
            }

            // Draw vertical lines (for column separators)
            int x = 0;
            for (int col = 0; col < ColumnCount - 1; col++)
            {
                x += GetColumnWidth(col);
                g.DrawLine(pen, x, 0 + VerticalLinePadding, x, Height - VerticalLinePadding);
            }
        }
    }

    private int GetRowHeight(int row)
    {
        if (row < 0 || row >= RowCount) return 0;

        // Calculate row height based on the table layout
        int totalHeight = Height;
        int fixedHeight = 0;
        int percentCount = 0;
        float percentTotal = 0;

        // Calculate fixed heights and percentage totals
        for (int i = 0; i < RowStyles.Count && i < RowCount; i++)
        {
            if (RowStyles[i].SizeType == SizeType.Absolute)
            {
                fixedHeight += (int)RowStyles[i].Height;
            }
            else if (RowStyles[i].SizeType == SizeType.Percent)
            {
                percentTotal += RowStyles[i].Height;
            }
        }

        // Handle the specific row
        if (row < RowStyles.Count)
        {
            if (RowStyles[row].SizeType == SizeType.Absolute)
            {
                return (int)RowStyles[row].Height;
            }
            else if (RowStyles[row].SizeType == SizeType.Percent)
            {
                return (int)((totalHeight - fixedHeight) * (RowStyles[row].Height / 100f));
            }
        }

        // Default: distribute remaining space equally among AutoSize rows
        int remainingRows = RowCount - RowStyles.Count;
        int remainingHeight = totalHeight - fixedHeight;
        if (percentTotal > 0)
        {
            remainingHeight -= (int)((totalHeight - fixedHeight) * (percentTotal / 100f));
        }

        return remainingRows > 0 ? remainingHeight / (remainingRows + 1) : remainingHeight;
    }

    private int GetColumnWidth(int col)
    {
        if (col < 0 || col >= ColumnCount) return 0;

        // Calculate column width based on the table layout
        int totalWidth = Width;
        int fixedWidth = 0;
        int percentCount = 0;
        float percentTotal = 0;

        // Calculate fixed widths and percentage totals
        for (int i = 0; i < ColumnStyles.Count && i < ColumnCount; i++)
        {
            if (ColumnStyles[i].SizeType == SizeType.Absolute)
            {
                fixedWidth += (int)ColumnStyles[i].Width;
            }
            else if (ColumnStyles[i].SizeType == SizeType.Percent)
            {
                percentTotal += ColumnStyles[i].Width;
            }
        }

        // Handle the specific column
        if (col < ColumnStyles.Count)
        {
            if (ColumnStyles[col].SizeType == SizeType.Absolute)
            {
                return (int)ColumnStyles[col].Width;
            }
            else if (ColumnStyles[col].SizeType == SizeType.Percent)
            {
                return (int)((totalWidth - fixedWidth) * (ColumnStyles[col].Width / 100f));
            }
        }

        // Default: distribute remaining space equally among AutoSize columns
        int remainingCols = ColumnCount - ColumnStyles.Count;
        int remainingWidth = totalWidth - fixedWidth;
        if (percentTotal > 0)
        {
            remainingWidth -= (int)((totalWidth - fixedWidth) * (percentTotal / 100f));
        }

        return remainingCols > 0 ? remainingWidth / (remainingCols + 1) : remainingWidth;
    }
}