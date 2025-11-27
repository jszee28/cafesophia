using System;
using System.Drawing;
using System.Drawing.Printing;

namespace cafesophia
{
    public class ReceiptPrinter
    {
        private readonly ReceiptData _data;

        public ReceiptPrinter(ReceiptData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public void Print()
        {
            var pd = new PrintDocument();
            pd.DocumentName = "Sale Receipt";
            pd.PrintPage += PrintPage;
            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                // surface to caller
                throw new InvalidOperationException("Printing failed: " + ex.Message, ex);
            }
            finally
            {
                pd.PrintPage -= PrintPage;
                pd.Dispose();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            int x = 10;
            int y = 10;
            int lineHeight = 16;

            using (var font = new Font("Courier New", 10))
            using (var bold = new Font("Courier New", 10, FontStyle.Bold))
            {
                DrawCenteredText(g, "SOPHIA'S CAFE", bold, e.PageBounds, ref y);
                DrawCenteredText(g, _data.Date.ToString("yyyy-MM-dd HH:mm:ss"), font, e.PageBounds, ref y);

                // New: print Order Type / Service Type on receipt
                var orderTypeText = string.IsNullOrWhiteSpace(_data.OrderType) ? "Order Type: N/A" : $"Order Type: {_data.OrderType}";
                // left align the Order Type so it's visible before the items
                g.DrawString(orderTypeText, font, Brushes.Black, x, y);
                y += lineHeight;

                DrawEquals(g, font, x, ref y);

                // header columns
                g.DrawString(string.Format("{0,-18}{1,5}{2,9}{3,10}", "Item Name", "Qty", "Price", "Total"), font, Brushes.Black, x, y);
                y += lineHeight;
                DrawDashes(g, font, x, ref y);

                foreach (var it in _data.Items)
                {
                    string line = string.Format("{0,-18}{1,5}{2,9}{3,10}", Truncate(it.ItemName, 18), it.Quantity, it.UnitPrice.ToString("N2"), it.Subtotal.ToString("N2"));
                    g.DrawString(line, font, Brushes.Black, x, y);
                    y += lineHeight;
                }

                DrawDashes(g, font, x, ref y);

                DrawRightAlignedLine(g, font, "TOTAL:", _data.Total.ToString("N2"), e.PageBounds, ref y);
                DrawRightAlignedLine(g, font, "Amount Paid:", _data.AmountPaid.ToString("N2"), e.PageBounds, ref y);
                DrawRightAlignedLine(g, font, "Change:", _data.Change.ToString("N2"), e.PageBounds, ref y);

                DrawEquals(g, font, x, ref y);
                DrawCenteredText(g, "Thank you for your purchase!", font, e.PageBounds, ref y);
                DrawEquals(g, font, x, ref y);

                e.HasMorePages = false;
            }
        }

        private void DrawCenteredText(Graphics g, string text, Font font, Rectangle pageBounds, ref int y)
        {
            var size = g.MeasureString(text, font);
            float px = (pageBounds.Width - size.Width) / 2f;
            g.DrawString(text, font, Brushes.Black, px, y);
            y += (int)size.Height + 4;
        }

        private void DrawEquals(Graphics g, Font font, int x, ref int y)
        {
            g.DrawString(new string('=', 45), font, Brushes.Black, x, y);
            y += 16;
        }

        private void DrawDashes(Graphics g, Font font, int x, ref int y)
        {
            g.DrawString(new string('-', 45), font, Brushes.Black, x, y);
            y += 16;
        }

        private void DrawRightAlignedLine(Graphics g, Font font, string label, string value, Rectangle pageBounds, ref int y)
        {
            // label on left, value aligned to right fixed width using Courier spacing
            g.DrawString(label, font, Brushes.Black, x: 10, y: y);
            string right = string.Format("{0,30}", value);
            g.DrawString(right, font, Brushes.Black, 10, y);
            y += 16;
        }

        private string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}
