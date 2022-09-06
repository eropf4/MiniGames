using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JIZN
{
    public class Grid
    {
        public int Height;
        public int Weight;
        public Cell[,] GridCell;

        public Grid(int height, int weight, int cellSize)
        {
            Height = height;
            Weight = weight;
            GridCell = new Cell[height,weight];

            for (int i = 0;i < Height;i++)
            for (int j = 0; j < Weight; j++)
            {
                GridCell[i,j] = new Cell(cellSize){IsSelected = false};
            }
        }

        public Cell this[int i, int j]
        {
            get => GridCell[i, j];
            set => GridCell[i, j] = value;
        }
    }

    public class Cell
    {
        public bool IsSelected;
        readonly int Size;

        public Cell(int size)
        {
            Size = size;
        }
    }
}
