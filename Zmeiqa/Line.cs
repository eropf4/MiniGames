using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Zmeiqa
{
    class Line
    {
        public bool IsHorizontal { get; set; }
        public int Length { get; set; }
        public int CellGap { get; set; }
        public Point StartPosition { get; set; }

        public Point EndPosition => IsHorizontal ? new Point(StartPosition.X + Length, StartPosition.Y) : new Point(StartPosition.X, StartPosition.Y + Length);


        public Line(int length, int cellGap, Point startPosition, bool isHorizontal)
        {
            IsHorizontal = isHorizontal;
            CellGap = cellGap;
            StartPosition = startPosition;
            Length = length;
        }
    }
}