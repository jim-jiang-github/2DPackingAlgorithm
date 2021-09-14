using System;
using System.Collections.Generic;
using System.Text;

namespace PackingDemo
{
    public class Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; }
        public int Height { get; }
        public int Right => X + Width;
        public int Bottom => Y + Height;

        public Box RightNode { get; set; }
        public Box BottomNode { get; set; }

        public Box(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #region Draw

        static readonly Random random = new Random();
        readonly System.Drawing.Color backColor = System.Drawing.Color.FromArgb(128, random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        internal void Draw(System.Drawing.Graphics graphics)
        {
            using System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(backColor);
            graphics.FillRectangle(solidBrush, new System.Drawing.Rectangle(X, Y, Width, Height));
            graphics.DrawString($"[{ToString()}]", System.Windows.Forms.Control.DefaultFont, System.Drawing.Brushes.Black, new System.Drawing.PointF(X, Y));
        }

        public override string ToString()
        {
            return $"{X},{Y},{Width},{Height}";
        }

        #endregion
    }
}
