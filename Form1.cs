using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseMove
{
    public partial class Form1 : Form
    { 
        System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
        TimeSpan timeSpan;
        bool breaker = true;
        double width = Screen.PrimaryScreen.Bounds.Width - 20;
        Random rand = new Random();
        
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            label2.Text = "ScreenWidth: " + Screen.PrimaryScreen.Bounds.Width.ToString();
            label3.Text = "ScreenHeight: " + Screen.PrimaryScreen.Bounds.Height.ToString();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        
        public async void btn_Start_Click(object sender, EventArgs e)
        {
            breaker = true;
            Loop();
        }
        private void btn_Start_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public bool BreakLoop()
        {
            //breaker = _breaker;
            breaker = false;
            return breaker;
        }
        public async Task Loop()
        {
            s.Reset();
            s.Start();
            this.Cursor = new Cursor(Cursor.Current.Handle);
            await Task.Run(() =>
            {
                while (breaker ==true)
                {
                    if (breaker == true)
                    {
                        int YAchse = Cursor.Position.Y;
                        if (Cursor.Position.Y!=YAchse || Cursor.Position.Y > YAchse || Cursor.Position.Y < YAchse)
                        {
                               
                            breaker = false;
                            s.Stop();
                            break;
                        }
                        
                        if (width == Convert.ToDouble(Cursor.Position.X)) //erreichen des Bildschirmrandes
                        {
                            //Cursor.Position = new Point(0, Cursor.Position.Y);                        
                            Cursor.Position = new System.Drawing.Point(0, rand.Next(1, Screen.PrimaryScreen.Bounds.Height));
                            Thread.Sleep(10);

                        }
                        else
                        {
                            Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                            Thread.Sleep(10);

                        }
                    }
                    else
                    {
                        s.Stop();
                        
                        
                        break;
                    }
                }
            });
        }       
        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = timeSpan.Hours.ToString("00") + ":" + timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X || e.Alt && e.KeyCode==Keys.F4)//it should stop now
            {
                BreakLoop();
                this.Close();
            }            
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            BreakLoop();
            s.Stop();           
            timeSpan = s.Elapsed;
            s.Reset();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Text = "MouseX: " + Cursor.Position.X.ToString();
            label5.Text = "MouseY: " + Cursor.Position.Y.ToString();            
        }
    }
}
