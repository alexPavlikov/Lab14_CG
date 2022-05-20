using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_14
{
    public struct Point3D
    {
        public int X;
        public int Y;
        public int Z;
    }
    public partial class Form1 : Form
    {
        //Bitmap bit;
        //Graphics gr;
        //Pen p, circ;
        //SolidBrush br;

        //int rad = 400;
        //Point ell1, ell2;

        Graphics graphics;
        int height = 50;
        Point3D[] vertices;
        double v11, v12, v13, v21, v22, v23, v32, v33, v43;
        double rho = 250.0, theta = 320.0, phi = 45.0;
        double c1 = 5.0, c2 = 3.5;
        double screenDist = 5;
        private void button1_Click(object sender, EventArgs e)
        {
            CalculateCoefficients(rho, theta, phi);
            DrawCube();
        }
        private void DrawCube()
        {
            DrawPerspectiveLine(vertices[0], vertices[1]);
            DrawPerspectiveLine(vertices[1], vertices[2]);
            DrawPerspectiveLine(vertices[2], vertices[6]);
            DrawPerspectiveLine(vertices[6], vertices[7]);
            DrawPerspectiveLine(vertices[7], vertices[4]);
            DrawPerspectiveLine(vertices[4], vertices[1]);
            DrawPerspectiveLine(vertices[1], vertices[5]);
            DrawPerspectiveLine(vertices[5], vertices[6]);
            DrawPerspectiveLine(vertices[5], vertices[4]);
            DrawPerspectiveLine(vertices[0], vertices[3]);
            DrawPerspectiveLine(vertices[3], vertices[2]);
            DrawPerspectiveLine(vertices[3], vertices[7]);
        }

        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            vertices = new Point3D[8];
            vertices[0].X = height; vertices[0].Y = -height; vertices[0].Z = -height;
            vertices[1].X = height; vertices[1].Y = height; vertices[1].Z = -height;
            vertices[2].X = -height; vertices[2].Y = height; vertices[2].Z = -height;
            vertices[3].X = -height; vertices[3].Y = -height; vertices[3].Z = -height;
            vertices[4].X = height; vertices[4].Y = -height; vertices[4].Z = -height;
            vertices[5].X = height; vertices[5].Y = height; vertices[5].Z = height;
            vertices[6].X = -height; vertices[6].Y = height; vertices[6].Z = height;
            vertices[7].X = -height; vertices[7].Y = -height; vertices[7].Z = height;
        }
        private int IX(double x)
        {
            double xx = x * (pictureBox1.Size.Width / 10.0) + 0.5;
            return (int)xx;
        }
        private int IY(double y)
        {
            double yy = pictureBox1.Size.Height - y * (pictureBox1.Size.Height / 7.0) + 0.5;
            return (int)yy;
        }
        private void CalculateCoefficients(double rho, double theta, double phi)
        {
            double th, ph, coeff, costh, sinth, cosph, sinph;
            coeff = Math.PI / 180;
            th = theta * coeff;
            ph = phi * coeff;
            costh = Math.Cos(th);
            sinth = Math.Sin(th);
            cosph = Math.Cos(ph);
            sinph = Math.Sin(ph);
            v11 = -sinth;
            v12 = -cosph * costh;
            v13 = -sinph * costh;
            v21 = costh;
            v22 = -cosph * sinth;
            v23 = -sinph * sinth;
            v32 = sinph;
            v33 = -cosph;
            v43 = rho;
        }
        private void Perspective(double x, double y, double z, ref double pX, ref double pY)
        {
            double xe, ye, ze;
            xe = v11 * x + v21 * y;
            ye = v12 * x + v22 * y + v32 * z;
            ze = v13 * x + v23 * y + v33 * z + v43;
            pX = screenDist * xe / ze + c1;
            pY = screenDist * ye / ze + c2;
        }
        private void DrawPerspectiveLine(Point3D p1, Point3D p2)
        {
            double X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
            Perspective(p1.X, p1.Y, p1.Z, ref X1, ref Y1);
            Perspective(p2.X, p2.Y, p2.Z, ref X2, ref Y2);
            Point point1 = new Point(IX(X1), IY(Y1));
            Point point2 = new Point(IX(X2), IY(Y2));
            graphics.DrawLine(Pens.Red, point1, point2);
        }
    }
}
