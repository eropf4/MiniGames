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

namespace JIZN
{
    public sealed partial class Form1 : Form
    {
        public Grid GridCells { get; set; }
        private List<Line> gridLines;
        public const int CellGup = 2;
        private Point mousLocation;
        private System.Windows.Forms.Timer myTimer;

        public Form1()
        {
            InitializeComponent();

            gridLines = new List<Line>();

            for (var i = 0; i < this.Height; i += CellGup)
            {
                var line = new Line(this.Width, CellGup, new Point(0, i), true);
                gridLines.Add(line);
            }

            for (var i = 0; i < this.Width ; i += CellGup)
            {
                gridLines.Add(new Line(this.Height, CellGup, new Point(i, 0), false));
            }

            GridCells = new Grid(this.Height / CellGup, this.Width/CellGup, CellGup);
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
                if (GridCells[i,j].IsSelected)
                    e.Graphics.FillRectangle(Brushes.GreenYellow,j*CellGup,i*CellGup,CellGup,CellGup );
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var mouseXCell = e.X;
            var mouseYCell = e.Y;

            var a = mouseXCell / CellGup ;
            var b = mouseYCell / CellGup ;
            GridCells[b,a].IsSelected = true;
            
            this.Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            var mouseXCell = e.X;
            var mouseYCell = e.Y;

            var a = mouseXCell / CellGup;
            var b = mouseYCell / CellGup;

            if (!GridCells[b, a].IsSelected)
            {
                GridCells[b, a].IsSelected = true;
                this.Invalidate();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mouseXCell = e.X;
                var mouseYCell = e.Y;

                var a = mouseXCell / CellGup;
                var b = mouseYCell / CellGup;

                if (!GridCells[b, a].IsSelected)
                {
                    GridCells[b, a].IsSelected = true;
                    this.Invalidate();
                }
            }

        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip5_Opening(object sender, CancelEventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Update();
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 40;
            myTimer.Tick += (send,t) => Update();
            myTimer.Start();
        }


        private new void Update()
        {
            var timer = new Stopwatch();
            timer.Start();
            var newGridCell = new Grid(GridCells.Height, GridCells.Weight, CellGup); 
            for (int i = 1; i < GridCells.Height - 1; i++)
            for (int j = 1; j < GridCells.Weight - 1; j++)
            {
                var intFlag = 0;
                for (int k = -1; k <= 1; k++)
                for (int t = -1; t <= 1; t++)
                {
                    if (k == 0 && t == 0) continue;
                    if (GridCells[i + k, j + t].IsSelected)
                        intFlag++;
                }

                if (intFlag < 2 && GridCells[i, j].IsSelected) 
                    newGridCell[i, j].IsSelected = false;
                else if (intFlag > 3 && GridCells[i, j].IsSelected)
                    newGridCell[i, j].IsSelected = false;
                else if (intFlag == 3 && !GridCells[i, j].IsSelected)
                    newGridCell[i, j].IsSelected = true;
                else newGridCell[i, j].IsSelected = GridCells[i, j].IsSelected;
            }

            GridCells = newGridCell;
            timer.Stop();
            Trace.WriteLine(timer.Elapsed);
            this.Invalidate();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            myTimer.Stop();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            GridCells = new Grid(this.Height / CellGup, this.Width / CellGup, CellGup);
            Invalidate();
        }

        private void GetRandomCells()
        {
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < GridCells.Height; i++)
            for (int j = 0; j < GridCells.Weight; j++)
            {
                this.GridCells[i, j].IsSelected = random.Next(20) % 3 == 0 ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetRandomCells();
            Invalidate();
        }
    }
}
