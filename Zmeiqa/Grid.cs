using System.Drawing;

namespace Zmeiqa
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
            GridCell = new Cell[height, weight];

            for (int i = 0; i < Height; i++)
            for (int j = 0; j < Weight; j++)
            {
                GridCell[i, j] = new Cell(cellSize,j,i) { IsSelected = false};
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
        public int x;
        public int y;

        public Cell(int size,int x, int y)
        {
            Size = size;
            this.x = x;
            this.y = y;
        }


    }
}