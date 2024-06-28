using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace UltraPong01
{
    public partial class Form1 : Form
    {

        string portNum = "COM5"; //edit
        SerialPort sp;
        bool serialOpen = false;

        int bx = 20;
        int by = 20;
        int bxv = 5;
        int byv = 5;

        //Player 1
        int p1x = 30;
        int p1y = 200;
        int p1h = 50; //height! 

        //Player 2
        int p2x = 400; //use PB Width!
        int p2y = 200;
        int p2h = 50; //height! 

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

            //update player positions


            //check p1 <--> ball collision
            if(bxv<0 && bx<p1x+10 && by>p1y && by+20 < p1y+p1h)
            {
                bxv *= -1; bx += bxv;
            }

            //get serial Data
            if (serialOpen)
            {
              //  p1y = getSerialData();
            }
           
            Refresh(); //calls the paint function
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 2);
            e.Graphics.DrawEllipse(whitePen, bx, by, 20, 20);

            e.Graphics.DrawRectangle(whitePen, p1x, p1y, 10, p1h);
            e.Graphics.DrawRectangle(whitePen, p2x, p2y, 10, p2h);

        }

        void startSerial()
        {
            portNum = textBox1.Text;
            if (portNum.Contains("COM")) // 'some' validation
            {
                sp = new SerialPort(portNum, 9600, Parity.None, 8, StopBits.One);
                sp.Open();
                sp.ReadTimeout = 10000;
                textBox2.Text = ("Connecting to " + portNum);

                button1.BackColor = Color.GreenYellow;
                timer1.Enabled = true;
                serialOpen = true;
            }
        }

        int getSerialData()
        {
            if (sp.IsOpen)
            {
                string buffer = sp.ReadExisting();
                if (buffer != null)
                {
                    if (buffer.Contains("Button1Pressed"))
                    {
                        textBox2.Text = buffer; //for debugging
                    }
                }
            }          
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startSerial();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            p1y = (int)e.Y;
        }
    }
}
