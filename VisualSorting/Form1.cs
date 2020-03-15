using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualSorting
{
    public partial class Form1 : Form
    {
        Bitmap Backbuffer;
        Pen pen;
        int keyPress;
        const int Rate = 1;
        public const int Height = 1080;
        public const int Width = 1920;
        public const float BorderWidth = 2.0f;
       
        Sorting sort;
        int counter = 0;
        int length = 0;

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            this.Size = new Size(Form1.Width, Form1.Height);
            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.BackColor = Color.DarkCyan;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = this.Size;

            Timer GameTimer = new Timer();
            GameTimer.Interval = Rate;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();

            pen = new Pen(Color.Black, Form1.BorderWidth);
            pen.Alignment = PenAlignment.Inset;
            pen.Width = 10;

            sort = new Sorting(Form1.Height, Form1.Width);
            length = sort.sortValues.Count - 1;
        }



        void GameTimer_Tick(object sender, EventArgs e)
        {
            if (keyPress == (int)Keys.Space)
            {
                if (counter < length)
                {
                    sort.BubbleSort(counter);
                    counter++;
                }
                else
                {
                    counter = 0;
                    length--;
                }
            }
            Draw(counter);
        }

        void DrawSort(Graphics g, Pen pen)
        {
            for (var i = 0; i < sort.sortValues.Count; i++)
            {
                if(sort.sortValues[i].swaped == true)
                {
                    pen.Color = Color.DarkCyan;
                    g.DrawLine(pen, sort.sortValues[i].x, 0, sort.sortValues[i].x, Form1.Height);
                    pen.Color = Color.Red;
                }
                pen.Color = Color.Black;
                g.DrawLine(pen, sort.sortValues[i].x, 0, sort.sortValues[i].x, sort.sortValues[i].length);
            }
        }

        void Draw(int counter)
        {
            Invalidate();

            if (Backbuffer != null)
            {
                using (var g = Graphics.FromImage(Backbuffer))
                {
                    DrawSort(g, pen);
                    Highlight(counter, g);
                }
            }
        }

        void Highlight(int counter, Graphics g)
        {
            pen.Color = Color.Red;
            g.DrawLine(pen, sort.sortValues[counter].x, 0, sort.sortValues[counter].x, sort.sortValues[counter].length);
            pen.Color = Color.Black;
        }


        void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }
        }

        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
                Backbuffer.Dispose();

            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                keyPress = (int)Keys.Space;
            }
            else
            {
                keyPress = 1;
            }
        }

    }
}
