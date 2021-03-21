using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += Form1_Paint;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int width = 200;
            int height = 200;
            int cx = 100;
            int cy = 100;
            int sec_arrow_length = 80;
            int mins_arrow_length = 60;
            int hrs_arrow_length = 40;

            // Draw frame
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(new Point(0, 0), new Size(width, height)));
            e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(0, 0), new Size(width, height)));

            // Draw numbers
            Font drawFont = new Font("Arial", 8);
            e.Graphics.DrawString("12", drawFont, Brushes.Red, new PointF(cx - 5, 2));
            e.Graphics.DrawString("6", drawFont, Brushes.Red, new PointF(cx - 5, height - 15));
            e.Graphics.DrawString("3", drawFont, Brushes.Red, new PointF(width - 10, cy - 5));
            e.Graphics.DrawString("9", drawFont, Brushes.Red, new PointF(5, cy - 5));

            // Draw center circle
            var center = new Point(cx, cy);
            e.Graphics.DrawEllipse(Pens.Black, new Rectangle(new Point(cx - 3, cy - 3), new Size(6, 6)));

            //Get current time
            DateTime dateTime = DateTime.Now;
            var secs = dateTime.Second;
            var mins = dateTime.Minute;
            var hrs = dateTime.Hour;

            // Draw seconds arrow
            int sec_degrees = secs * 360 / 60; //seconds value to degree
            e.Graphics.DrawLine(Pens.Red, center, DegreesToPoint(sec_degrees, sec_arrow_length, center));

            // Draw minutes arrow
            int min_degrees = mins * 360 / 60; //minutes value to degree
            e.Graphics.DrawLine(new Pen(Color.Black, 2f), center, DegreesToPoint(min_degrees, mins_arrow_length, center));

            // Draw hours arrow
            double hrs_degrees = hrs % 12 * 30 + mins * 0.5; //hours value to degree
            e.Graphics.DrawLine(new Pen(Color.Black, 3f), center, DegreesToPoint(hrs_degrees, hrs_arrow_length, center));
        }

        private Point DegreesToPoint(double degrees, int length, Point center)
        {
            double radians = Math.PI * degrees / 180;
            int x, y;
            if (degrees >= 0 && degrees <= 180)
            {
                x = center.X + (int)(length * Math.Sin(radians));
                y = center.Y - (int)(length * Math.Cos(radians));
            }
            else
            {
                x = center.X - (int)(length * -Math.Sin(radians));
                y = center.Y - (int)(length * Math.Cos(radians));
            }

            return new Point(x, y);
        }
    }
}