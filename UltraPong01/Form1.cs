using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraPong01
{
    public partial class Form1 : Form
    {
        int bx = 20;
        int by = 20;
        int bxv = 5;
        int byv = 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bx += bxv;
            by += byv;

            if (bx < 0 || bx > pictureBox1.Width-20) { bxv *= -1; }
            if (by < 0 || by > pictureBox1.Height-20) { byv *= -1; }

            Refresh(); //calls the paint function
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 2);
            e.Graphics.DrawEllipse(whitePen, bx, by, 20, 20);
        }
    }
}
