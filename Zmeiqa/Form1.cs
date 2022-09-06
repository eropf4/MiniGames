using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zmeiqa
{
    public sealed partial class Form1 : Form
    {
        public Player Player { get; set; }
        public Grid GridCells { get; set; }
        private List<Line> gridLines;
        public const int CellGup = 20;
        private System.Windows.Forms.Timer myTimer;

        public Random randomX { get; set; }
        public Random randomY { get; set; }

        public Cell Drug { get; set; }

        public Form1()
        {
            InitializeComponent();

            gridLines = new List<Line>();

            for (var i = 0; i < this.Height; i += CellGup)
            {
                var line = new Line(this.Width, CellGup, new Point(0, i), true);
                gridLines.Add(line);
            }

            for (var i = 0; i < this.Width; i += CellGup)
            {
                gridLines.Add(new Line(this.Height, CellGup, new Point(i, 0), false));
            }

            var playerList = new LinkedList<Cell>();

            playerList.AddFirst(new Cell(20, 20, 20));
            playerList.AddFirst(new Cell(20, 20, 21));
            playerList.AddFirst(new Cell(20, 20, 22));
            playerList.AddFirst(new Cell(20, 20, 23));

            var player = new Player(playerList);
            Player = player;


            GridCells = new Grid(this.Height / CellGup, this.Width / CellGup, CellGup);

            foreach (var part in player.PlayerParts)
            {
                GridCells[part.y, part.x].IsSelected = true;
            }

            randomX = new Random();
            randomY = new Random();

            Drug = new Cell(20, randomX.Next(this.Width / CellGup), randomY.Next(this.Height / CellGup));
            GridCells.GridCell[Drug.y, Drug.x].IsSelected = true;

            DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < GridCells.Height; i++)
                for (int j = 0; j < GridCells.Weight; j++)
                {
                    if (GridCells[i, j].IsSelected)
                        e.Graphics.FillRectangle(Brushes.GreenYellow, j * CellGup, i * CellGup, CellGup, CellGup);
                }
        }




        private void StartButton_Click(object sender, EventArgs e)
        {
            Update();
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 50;
            myTimer.Tick += (send, t) => Update();
            myTimer.Start();
        }


        private new void Update()
        {
            var newGridCell = new Grid(GridCells.Height, GridCells.Weight, CellGup);
            Player.AddFirst();

            if (Player.PlayerParts.First.Value.x == Drug.x && Player.PlayerParts.First.Value.y == Drug.y)
            {
                Drug = new Cell(20, randomX.Next(this.Width / CellGup), randomY.Next(this.Height / CellGup));
            }
            else
            {
                Player.DeleteLast();
            }

            newGridCell[Drug.y, Drug.x].IsSelected = true;

            foreach (var part in Player.PlayerParts)
            {
                newGridCell[part.y, part.x].IsSelected = true;
            }


            GridCells = newGridCell;
            this.Invalidate();
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    Player.PlayerDirection = Player.Direction.Up;
                    break;
                case 'a':
                    Player.PlayerDirection = Player.Direction.Left;
                    break;
                case 's':
                    Player.PlayerDirection = Player.Direction.Down;
                    break;
                case 'd':
                    Player.PlayerDirection = Player.Direction.Right;
                    break;
            }
        }
    }
}
