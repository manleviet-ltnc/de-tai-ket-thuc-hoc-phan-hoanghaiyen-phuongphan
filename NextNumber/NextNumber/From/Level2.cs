using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextNumber.From
{
    public partial class Level2 : Form
    {
        private bool _islost = false;
        private int demClick = 0;
        private int _counter;
        private bool _isStart;
        private bool _isEnd;
        private bool _isDie;
        private const int DEFAULT_TIME = 10;
        int[] arr = new int[12];
        public Level2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                _counter = 0;
                timer1.Interval = 1000;
                timer1.Tick += Timer1_Tick;
                timer1.Start();
                timer1.Enabled = true;
                _isStart = true;
            }
            this.timer2.Start();

            Button[] btArr = new Button[11];
            int[] h = new int[11];
            h = Random();
            Random rd = new Random();
            Point[] poit = new Point[11];
            Size[] size = new Size[11];
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

            if (demClick == 11)
            {
                if (arr[1] == 1 && arr[2] == 2 && arr[3] == 3 && arr[4] == 4 && arr[5] == 5 && arr[6] == 6 && arr[7] == 7 && arr[8] == 8 && arr[9] == 9 && arr[10] == 10 && arr[11] == 11)
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
            new Level2().Show();
        }

        private void won()
        {
            this.timer1.Stop();
            this.timer2.Stop();
            btnStart.Hide();
             Button level3= new Button();
             level3.Location = new Point(12, 13);
             level3.Size = new Size(75, 37);
             level3.Text = "Level3";
             level3.Click += level3_click;
             this.Controls.Add(level3);
             MessageBox.Show("Chúc mừng bạn đã chiến thắng");
            
        }

        private void level3_click(object sender, EventArgs e)
        {
            this.Hide();
            new Level3().Show();
        }

      
        private Point[] InitPoint()
        {
            Point[] poit = new Point[11];
            poit[0] = new Point(3, 3);
            poit[1] = new Point(84, 3);
            poit[2] = new Point(165, 3);
            poit[3] = new Point(2, 59);
            poit[4] = new Point(84, 77);
            poit[5] = new Point(195, 62);
            poit[6] = new Point(3, 124);
            poit[7] = new Point(114, 119);
            poit[8] = new Point(3, 180);
            poit[9] = new Point(84, 197);
            poit[10] = new Point(165, 189);
           
            return poit;
        }

        private Size[] InitSize()
        {
            Size[] size = new Size[11];
            size[0] = new Size(75, 50);
            size[1] = new Size(75, 67);
            size[2] = new Size(75, 53);
            size[3] = new Size(75, 59);
            size[4] = new Size(101, 36);
            size[5] = new Size(45, 102);
            size[6] = new Size(105, 50);
            size[7] = new Size(75, 64);
            size[8] = new Size(75, 91);
            size[9] = new Size(75, 74);
            size[10] = new Size(75, 82);
           
            return size;
        }
        private int[] Random()
        {
            int[] lotteryPool = new int[11];
            int[] lotteryPool1 = new int[11];
            for (int x = 0; x < lotteryPool.Length; x++) // generates an array of all 50 possible numbers
            {
                lotteryPool[x] = x + 1;
            }

            Random rnd = new Random();
            int randomIndex;
            int y;
            int dem = 0;
            for (y = 0; y < 11; y++)
            {
                randomIndex = rnd.Next(0, 11); 
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
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

        private void Level2_Load(object sender, EventArgs e)
        {

        }

       
    }
    
}
