using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Вершина
{
    public abstract class Apex
    {
        protected int x;
        protected int y;
        protected int delx;
        protected int dely;

        public Apex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Apex()
        {
            x = 100;
            y = 100;
        }

        static Apex()
        {
            R = 25;
            color = Color.Turquoise;
        }

        public int delX { get { return delx; } set { delx = value; } }
        public int delY { get { return dely; } set { dely = value; } }

        public abstract void DrawApex(Graphics g);

        public abstract bool Check(int x, int y);

        public static int R { get; set; }
        public static Color color { get; set; }

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

    }

    //Круг
    public class Circle : Apex
    {
        public Circle(int x, int y) : base(x, y) { }
        public Circle() : base() { }


        public override void DrawApex(Graphics g)
        {
            g.FillEllipse(new SolidBrush(color), x - R, y - R, 2 * R, 2 * R);
        }

        public override bool Check(int ex, int ey)
        {
            //посчитать катеты, сравнить гипотенузы с радиусом
            if (((x - ex) * (x - ex) + (y - ey) * (y - ey)) <= R * R) return true;
            else return false;
        }

    }

    //Треугольник
    public class Triangle : Apex
    {
        public Triangle(int x, int y) : base(x, y) { }
        public Triangle() : base() { }

        public override void DrawApex(Graphics g)
        {
            Point point1 = new Point(x, y - R);
            Point point2 = new Point(x + R, y + R);
            Point point3 = new Point(x - R, y + R);

            Point[] points = { point1, point2, point3};

            g.FillPolygon(new SolidBrush(color), points);
        }

        public override bool Check(int ex, int ey)
        {
            if (ey <= y + R && ey + 2 * ex - y + R - 2 * x >= 0 && ey - 2 * ex - y + R + 2 * x >= 0) return true;
            else return false;
        }
    }

    //Квадрат
    public class Square : Apex
    {
        public Square(int x, int y) : base(x, y) { }
        public Square() : base() { }

        public override void DrawApex(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), x - R, y - R, 2 * R, 2 * R);
        }

        public override bool Check(int ex, int ey)
        {
            if ((y - R) <= ey && (y + R) >= ey && (x - R) <= ex && (x + R) >= ex) return true;
            else return false;
        }

    }

}