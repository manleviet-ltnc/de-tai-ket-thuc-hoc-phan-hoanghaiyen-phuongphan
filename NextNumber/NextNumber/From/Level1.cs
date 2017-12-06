using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NextNumber.From
{
    public partial class Level1 : Form
    {
        private bool _islost = false;
        private int demClick = 0;
        private int _counter;
        private bool _isStart;
        private bool _isEnd;
        private bool _isDie;
        private const int DEFAULT_TIME = 10;
        int[] arr = new int[8];


        public Level1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if(!timer1.Enabled)
            {
                _counter = 0;
                timer1.Interval = 1000;
                timer1.Tick += Timer1_Tick;
                timer1.Start();
                timer1.Enabled = true;
                _isStart = true;
            }
            this.timer2.Start();

            Button[] btArr = new Button[7];
            int[] h = new int[7];
            h = Random();
            Random rd = new Random();
            Point[] poit = new Point[7];
            Size[] size = new Size[7];
            poit = InitPoint();

            size = InitSize();
            for (int i = 0; i < btArr.Length; i++)
            {

                btArr[i] = new Button();
                // btArr[i].BackColor = Color.Yellow;
                btArr[i].Size = size[i];
                btArr[i].Location = poit[i];

                btArr[i].Text = h[i].ToString();
                btArr[i].Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                btArr[i].Enabled = true;
                btArr[i].Click += Myclick;
                panel1.Controls.Add(btArr[i]);
            }
        }

       private void Timer1_Tick(object sender, EventArgs e)
        {
            
            lb_time.Text = Math.Abs(_counter - DEFAULT_TIME).ToString("00");
            if (!_isEnd && _counter == DEFAULT_TIME)
            {
                timer1.Stop();
                _isEnd = true;
                timer1.Enabled = false;
                MessageBox.Show("thua cuộc");
               // Enables();
            }
            _counter++;
        }
       private void Enables()
       {
           panel1.Enabled = false;
       }
        private void Myclick(object sender, EventArgs e)
        {
            int x = Int32.Parse((sender as Button).Text);
            if (_islost == false)
            {
                demClick++;
            }
            else
            {
                demClick = 0;
            }
            
            arr[demClick] = x;
            (sender as Button).Enabled = false;

            if (demClick == 7)
            {
                if (arr[1] == 1 && arr[2] == 2 && arr[3] == 3 && arr[4] == 4 && arr[5] == 5 && arr[6] == 6 && arr[7] == 7)
                {
                  
                    won();
                    
                }
                else
                {
                    
                    lost();
                }
            }
        }

        private void lost()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            btnStart.Hide();
            Button rePlay = new Button();
            rePlay.Location = new Point(12, 13);
            rePlay.Size = new Size(75, 37);
            rePlay.Text = "rePlay";
            rePlay.Click += rePlay_click;
            this.Controls.Add(rePlay);
            Enables();
            MessageBox.Show("Thua cuộc, Thử lại lần nữa");
                   
        }

        private void rePlay_click(object sender, EventArgs e)
        {

            this.Hide();
            new Level1().Show();
        }

        private void won()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            btnStart.Hide();
             Button level2= new Button();
             level2.Location = new Point(12, 13);
             level2.Size = new Size(75, 37);
             level2.Text = "Level2";
             level2.Click += level2_click;
             this.Controls.Add(level2);
             MessageBox.Show("Chúc mừng bạn đã chiến thắng");
            
        }

        private void level2_click(object sender, EventArgs e)
        {
            this.Hide();
            new Level2().Show();
        }
        private Point[] InitPoint()
        {
            Point[] poit = new Point[7];
            poit[0] = new Point(0, 3);
            poit[1] = new Point(110, 3);
            poit[2] = new Point(0, 103);
            poit[3] = new Point(75, 104);
            poit[4] = new Point(156, 104);
            poit[5] = new Point(75, 187);
            poit[6] = new Point(196, 163);
            return poit;
        }

        private Size[] InitSize()
        {
            Size[] size = new Size[7];
            size[0] = new Size(95, 96);
            size[1] = new Size(121, 95);
            size[2] = new Size(75, 160);
            size[3] = new Size(75, 80);
            size[4] = new Size(75, 53);
            size[5] = new Size(115, 76);
            size[6] = new Size(35, 100);
            return size;
        }
        private int[] Random()
        {
            int[] lotteryPool = new int[7];
            int[] lotteryPool1 = new int[7];
            for (int x = 0; x < lotteryPool.Length; x++) // generates an array of all 50 possible numbers
            {
                lotteryPool[x] = x + 1;
            }

            Random rnd = new Random();
            int randomIndex;
            int y;
            int dem = 0;
            for (y = 0; y < 7; y++)
            {
                randomIndex = rnd.Next(0, 7); 
                if (lotteryPool[randomIndex] != 0) 
                {
                    Console.WriteLine(lotteryPool[randomIndex]);
                    lotteryPool1[dem] = lotteryPool[randomIndex];
                    dem++;
                    lotteryPool[randomIndex] = 0;         
                }
                else
                {
                    y--;
                }
            }

            return lotteryPool1;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
          //  this.progressBar1.Increment(1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lb_time_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
