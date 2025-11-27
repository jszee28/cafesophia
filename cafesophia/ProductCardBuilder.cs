using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cafesophia
{
    public static class ProductCardBuilder
    {
        /// <summary>
        /// Callback signature: (itemId, name, price, stock)
        /// </summary>
        public static Panel CreateCard(int itemId, string name, decimal price, int stock, string imagePath, Action<int, string, decimal, int> onClick)
        {
            // Item Sale
            var card = new Panel
            {
                Size = new Size(120, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Margin = new Padding(6),
                Cursor = Cursors.Hand,
                Tag = new { itemId, name, price, stock }
            };
            // Check
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 238, 232);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            var pb = CreatePictureBox(imagePath);
            card.Controls.Add(pb);

            var lblName = new Label
            {
                Text = name,
                Location = new Point(4, 90),
                Size = new Size(112, 32),
                // Text
                Font = new Font("Century Gothic", 8),
                AutoEllipsis = true
            };
            card.Controls.Add(lblName);
            lblName.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 238, 232);
            lblName.MouseLeave += (s, e) => card.BackColor = Color.White;

            var lblPrice = new Label
            {
                Text = string.Format("₱{0:N2}", price),
                Location = new Point(4, 120),
                Size = new Size(112, 24),
                // Text
                Font = new Font("Century Gothic", 9, FontStyle.Bold),
                ForeColor = Color.Green
            };
            card.Controls.Add(lblPrice);
            lblPrice.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 238, 232);
            lblPrice.MouseLeave += (s, e) => card.BackColor = Color.White;

            void FireCallback() => onClick?.Invoke(itemId, name, price, stock);

            card.Click += (s, e) => FireCallback();
            pb.Click += (s, e) => FireCallback();
            lblName.Click += (s, e) => FireCallback();
            lblPrice.Click += (s, e) => FireCallback();

            return card;
        }
        // END CreateCard

        // Default Set Cards
        private static PictureBox CreatePictureBox(string imagePath)
        {
            var pb = new PictureBox
            {
                Location = new Point(20, 6),
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.LightGray
            };

            try
            {
                string candidate = imagePath ?? string.Empty;

                // Normalize separators and trim whitespace
                candidate = candidate.Trim();
                candidate = candidate.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

                // Build a list of plausible paths to try (in priority order)
                var tries = new List<string>();

                // 1) exact candidate as-is (may be absolute)
                if (!string.IsNullOrEmpty(candidate)) tries.Add(candidate);

                // 2) candidate without any leading separators (common when DB stores "/images/...")
                // Use char[] overload to avoid ReadOnlySpan<> ambiguity on some frameworks
                var trimmedRoot = candidate.TrimStart(new char[] { Path.DirectorySeparatorChar });
                if (!string.IsNullOrEmpty(trimmedRoot) && !tries.Contains(trimmedRoot)) tries.Add(trimmedRoot);

                // 3) in Application.StartupPath combined with candidate/trimmedRoot
                if (!string.IsNullOrEmpty(candidate))
                {
                    try { tries.Add(Path.Combine(Application.StartupPath, candidate)); } catch { }
                }
                if (!string.IsNullOrEmpty(trimmedRoot))
                {
                    try { tries.Add(Path.Combine(Application.StartupPath, trimmedRoot)); } catch { }
                }

                // 4) common folders inside app: images, images/uploads
                try
                {
                    if (!string.IsNullOrEmpty(trimmedRoot))
                    {
                        tries.Add(Path.Combine(Application.StartupPath, "images", trimmedRoot));
                        tries.Add(Path.Combine(Application.StartupPath, "images", "uploads", Path.GetFileName(trimmedRoot)));
                    }
                    // try images/uploads with filename only
                    if (!string.IsNullOrEmpty(candidate))
                    {
                        tries.Add(Path.Combine(Application.StartupPath, "images", "uploads", Path.GetFileName(candidate)));
                        tries.Add(Path.Combine(Application.StartupPath, "images", Path.GetFileName(candidate)));
                    }
                }
                catch { /* ignore path build errors */ }

                // 5) filename only (look in startup path)
                if (!string.IsNullOrEmpty(candidate))
                {
                    tries.Add(Path.Combine(Application.StartupPath, Path.GetFileName(candidate)));
                }

                // Try each candidate and use the first existing file
                string found = null;
                foreach (var t in tries)
                {
                    if (string.IsNullOrWhiteSpace(t)) continue;
                    try
                    {
                        var normalized = t.Trim();
                        if (File.Exists(normalized))
                        {
                            found = normalized;
                            break;
                        }
                    }
                    catch
                    {
                        // ignore IO/path exceptions and continue trying others
                    }
                }

                if (!string.IsNullOrEmpty(found))
                {
                    // Load image safely
                    using (var fs = File.OpenRead(found))
                    {
                        pb.Image = Image.FromStream(fs);
                    }
                }
                else
                {
                    pb.Image = CreatePlaceholder(80, 80);
                }
            }
            catch
            {
                pb.Image = CreatePlaceholder(80, 80);
            }

            return pb;
        }
        // Card Placeholder
        private static Bitmap CreatePlaceholder(int w, int h)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                using (var f = new Font("Century Gothic", 7))
                using (var b = new SolidBrush(Color.DimGray))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("No Image", f, b, new RectangleF(0, 0, w, h), sf);
                }
            }
            return bmp;
        }
    }
}
