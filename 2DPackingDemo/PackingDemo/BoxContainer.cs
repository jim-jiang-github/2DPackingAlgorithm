using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingDemo
{
    public class BoxContainer : HashSet<Box>
    {
        private readonly int _width;
        private readonly int _height;

        public BoxContainer(int width, int height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Do packing
        /// </summary>
        /// <returns>no packed boxes</returns>
        public IEnumerable<Box> Packing()
        {
            var boxes = this.OrderByDescending(b => b.Height);
            Box rootBox = boxes.FirstOrDefault();
            if (rootBox == null)
            {
                return new Box[0];
            }
            foreach (var box in boxes)
            {
                box.RightNode = null;
                box.BottomNode = null;
            }
            var noPackedBoxes = boxes.Skip(1)
                .Select(b => new { Box = b, Packed = SetBoxLocaltion(rootBox, b, _width, _height) })
                .Where(x => !x.Packed)
                .Select(x => x.Box);
            return noPackedBoxes;
        }

        private bool SetBoxLocaltion(Box parentBox, Box newBox, int containerWidth, int containerHeight)
        {
            if (parentBox != null)
            {
                int rightRemainingWidth = containerWidth - parentBox.Width;
                int rightRemainingHeight = parentBox.Height;
                int bottomRemainingWidth = containerWidth;
                int bottomRemainingHeight = containerHeight - parentBox.Height;
                var result = SetBoxLocaltion(parentBox.RightNode, newBox, rightRemainingWidth, rightRemainingHeight);
                if (result)
                {
                    if (parentBox.RightNode == null)
                    {
                        parentBox.RightNode = newBox;
                        newBox.X = parentBox.Right;
                        newBox.Y = parentBox.Y;
                    }
                }
                else
                {
                    result = SetBoxLocaltion(parentBox.BottomNode, newBox, bottomRemainingWidth, bottomRemainingHeight);
                    if (result)
                    {
                        if (parentBox.BottomNode == null)
                        {
                            parentBox.BottomNode = newBox;
                            newBox.X = parentBox.X;
                            newBox.Y = parentBox.Bottom;
                        }
                    }
                }
                return result;
            }
            else if (newBox.Width <= containerWidth && newBox.Height <= containerHeight)
            {
                return true;
            }
            return false;
        }

        internal void DrawBox(System.Drawing.Graphics graphics)
        {
            foreach (var box in this)
            {
                box.Draw(graphics);
                graphics.FillEllipse(System.Drawing.Brushes.Red, new System.Drawing.Rectangle(box.X + box.Width / 2 - 4, box.Y + box.Height / 2 - 4, 8, 8));
            }
            foreach (var box in this)
            {
                if (box.RightNode != null)
                {
                    graphics.DrawLine(System.Drawing.Pens.Red
                        , new System.Drawing.Point(box.X + box.Width / 2, box.Y + box.Height / 2)
                        , new System.Drawing.Point(box.RightNode.X + box.RightNode.Width / 2, box.RightNode.Y + box.RightNode.Height / 2));
                }
                if (box.BottomNode != null)
                {
                    graphics.DrawLine(System.Drawing.Pens.Red
                        , new System.Drawing.Point(box.X + box.Width / 2, box.Y + box.Height / 2)
                        , new System.Drawing.Point(box.BottomNode.X + box.BottomNode.Width / 2, box.BottomNode.Y + box.BottomNode.Height / 2));
                }
            }
        }
    }
}
