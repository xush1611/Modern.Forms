﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SkiaSharp;

namespace Modern.Forms
{
    public class ListViewControlItem
    {
        public string Text { get; set; }
        public SKBitmap Image { get; set; }
        public bool Selected { get; set; }
        public object Tag { get; set; }

        public Rectangle Bounds { get; private set; }

        public void SetBounds (int x, int y, int width, int height)
        {
            Bounds = new Rectangle (x, y, width, height);
        }

        internal void DrawItem (SKCanvas canvas)
        {
            var background_color = Selected ? Theme.RibbonItemHighlightColor : Theme.FormBackColor;

            canvas.FillRectangle (Bounds, background_color);

            if (Image != null)
                canvas.DrawBitmap (Image, Bounds.Left + (Bounds.Width - 32) / 2, Bounds.Top + 3);

            canvas.Save ();
            canvas.ClipRect (new SKRect (Bounds.Left, Bounds.Top, Bounds.Right, Bounds.Bottom));

            var lines = Text.Split (' ');

            canvas.DrawCenteredText (lines[0].Trim (), Theme.UIFont, 12, Bounds.Left + Bounds.Width / 2, Bounds.Top + 50, Theme.DarkText);

            if (lines.Length > 1)
                canvas.DrawCenteredText (lines[1].Trim (), Theme.UIFont, 12, Bounds.Left + Bounds.Width / 2, Bounds.Top + 66, Theme.DarkText);

            canvas.Restore ();
        }
    }
}