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
        //Apex shape;
        //int delx, dely;

        int whatShape; // какая фигура выбрана? (0 - круг, 1 - квадрат, 2 - треугольник)
        bool isDraw; // существует ли вершина
        bool isDrag; // флаг на перетаскивание

        List<Apex> list = new List<Apex>();
        
        public Form1()
        {
            InitializeComponent();
            /*delx = 0;
            dely = 0;*/
            isDraw = false;
            isDrag = false;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (isDraw) 
            { 
                //shape.DrawApex(e.Graphics); 
                foreach (Apex i in list)
                {
                    i.DrawApex(e.Graphics);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            bool usl = false;
            
            foreach(Apex i in list)
            {
                if (i.Check(i.X, i.Y)) { usl = true; break; }
            }

            //попали ли в вершину
            if (isDraw && usl)
            {
                if (e.Button == MouseButtons.Left)
                {   
                    //включить флаг на перетаскивание
                    isDrag = true;

                    //зафиксировать расстояние между мышкой и серединой вершины (х, у)
                    
                    /*delx = e.X - shape.X;
                    dely = e.Y - shape.Y;*/ 

                    foreach (Apex i in list)
                    {
                        i.delX = e.X - i.X;
                        i.delY = e.Y - i.Y;
                    }

                }
                if(e.Button == MouseButtons.Right)
                {
                    isDraw = false;
                    isDrag = false;

                    int index = -1;

                    foreach (Apex i in list)
                    {
                        index++;
                        if (i.Check(i.X, i.Y) == true) 
                        {
                            list[index] = null;
                            //list.RemoveAt(index);
                            index--;
                        }   
                    }
                }
            }
            else
            {
                isDraw = true;
                switch (whatShape)
                {
                    /*case 0: shape = new Circle(e.X, e.Y); break;
                    case 1: shape = new Square(e.X, e.Y); break;
                    case 2: shape = new Triangle(e.X, e.Y); break;*/

                    case 0: list.Add(new Circle(e.X, e.Y)) ; break;
                    case 1: list.Add(new Square(e.X, e.Y)); break;
                    case 2: list.Add(new Triangle(e.X, e.Y)); break;
                }
            }

            //this.Invalidate();
            this.Refresh(); //действует оперативнее, но затратнее
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {
                foreach (Apex i in list)
                {
                    i.X = e.X - i.delX;
                    i.Y = e.Y - i.delY;
                }

                /*shape.X = e.X - delx;
                shape.Y = e.Y - dely;*/
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
            if (isDraw)
            {
                isDrag = false;
                //isDraw = false;

                this.Invalidate();
            }
        }

    }
}
