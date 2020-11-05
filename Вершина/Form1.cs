using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Вершина
{
    public partial class Form1 : Form
    {
        Apex shape;
        int whatShape; // какая фигура выбрана? (0 - круг, 1 - квадрат, 2 - треугольник)
        bool isDraw = false; // существует ли вершина
        bool isDrag; // флаг на перетаскивание
        int delx, dely;
        
        public Form1()
        {
            InitializeComponent();
            delx = 0;
            dely = 0;
            isDrag = false;
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (isDraw) shape.DrawApex(e.Graphics);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //попали ли в вершину
            if (isDraw && shape.Check(e.X, e.Y))
            {
                if (e.Button == MouseButtons.Left)
                {   
                    //включить флаг на перетаскивание
                    isDrag = true;

                    //зафиксировать расстояние между мышкой и серединой вершины (х, у)
                    delx = e.X - shape.X;
                    dely = e.Y - shape.Y;

                }
                if(e.Button == MouseButtons.Right)
                {
                    isDraw = false;
                    isDrag = false;
                }
            }
            else
            {
                isDraw = true;
                switch (whatShape)
                {
                    case 0: shape = new Circle(e.X, e.Y); break;
                    case 1: shape = new Square(e.X, e.Y); break;
                    case 2: shape = new Triangle(e.X, e.Y); break;
                }
            }

            //this.Invalidate();
            this.Refresh(); //действует оперативнее, но затратнее
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                shape.X = e.X - delx;
                shape.Y = e.Y - dely;
            }
            this.Invalidate();
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whatShape = 0;
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whatShape = 1;
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whatShape = 2;
        }

        //двойная буфферизация
        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //выключаем флаг
            isDrag = false;  
        }

    }
}
