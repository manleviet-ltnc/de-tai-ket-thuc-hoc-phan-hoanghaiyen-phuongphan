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
    public partial class Level3 : Form
    {
        private bool _islost = false;
        private int demClick = 0;
        private int demClick1 = 0;
        private int _counter;
        private bool _isStart;
        private bool _isEnd;
        private bool _isDie;
        private const int DEFAULT_TIME = 10;
        int[] arr = new int[6];
        int[] h1;
        int[] h2;
    
        Button[] btArr = new Button[5];
        public Level3()
        {
            InitializeComponent();
        }
        private void btnStart_Click_1(object sender, EventArgs e)
        {
            if (!timer3.Enabled)
            {
                _counter = 0;
                timer3.Interval = 1000;
                timer3.Tick += Timer3_Tick;
                timer3.Start();
                timer3.Enabled = true;
                _isStart = true;
            }
            this.timer4.Start();

           
            h1 = new int[5];
            h2 = new int[5];
            h1 = Random();
            h2 = Random();
            for (int i = 0; i < 5; i++)
            {
                h2[i] = h2[i] + 5;
            }
            
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

                btArr[i].Text = h1[i].ToString();
                btArr[i].Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                btArr[i].Enabled = true;
                btArr[i].Click += Myclick;
                panel1.Controls.Add(btArr[i]);
            }
        }
        private void Timer3_Tick(object sender, EventArgs e)
        {

            lb_time.Text = Math.Abs(_counter - DEFAULT_TIME).ToString("00");
            if (!_isEnd && _counter == DEFAULT_TIME)
            {
                timer3.Stop();
                _isEnd = true;
                timer3.Enabled = false;
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

                    if (demClick == 6)
                    {
                        demClick = 0;
                    }
            }
            else
            {
                demClick = 0;
            }

            arr[demClick] = x;
            (sender as Button).Enabled = false;

            if (demClick == 5)
            {
                if (arr[1] == 1 && arr[2] == 2 && arr[3] == 3 && arr[4] == 4 && arr[5] == 5)
                {

                    resetButtonText();
                    
                }
            }
        }

        private void resetButtonText()
        {
           
            for (int i = 0; i < 5; i++)
            {
                
                btArr[i].Text=h2[i].ToString();
                btArr[i].Enabled = true;
                btArr[i].Click += Myclick1;
            }
           
           
        }

        private void Myclick1(object sender, EventArgs e)
        {
            int x = Int32.Parse((sender as Button).Text);
            if (_islost == false)
            {

                demClick1++;

               
            }
            else
            {
                demClick1= 0;
            }

            arr[demClick1] = x;
            (sender as Button).Enabled = false;

            if (demClick1== 5)
            {
                if (arr[0] == 6 && arr[1] == 7 && arr[2] == 8 && arr[3] == 9 && arr[4] == 10)
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
            this.timer3.Stop();
            this.timer4.Stop();
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
            new Level3().Show();
        }

        private void won()
        {
            this.timer3.Stop();
            this.timer4.Stop();
            btnStart.Hide();
            Button level3 = new Button();
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
            poit[1] = new Point(106, 3);
            poit[2] = new Point(106, 85);
            poit[3] = new Point(3, 123);
            poit[4] = new Point(186, 123);
            /*
            poit[5] = new Point(195, 62);
            poit[6] = new Point(3, 124);
            poit[7] = new Point(114, 119);
            poit[8] = new Point(3, 180);
            poit[9] = new Point(84, 197);
            poit[10] = new Point(165, 189);
            */
            return poit;
        }

        private Size[] InitSize()
        {
            Size[] size = new Size[11];
            size[0] = new Size(97, 120);
            size[1] = new Size(139, 76);
            size[2] = new Size(139, 32);
            size[3] = new Size(176, 148);
            size[4] = new Size(59, 148);

           /* size[5] = new Size(45, 102);
            size[6] = new Size(105, 50);
            size[7] = new Size(75, 64);
            size[8] = new Size(75, 91);
            size[9] = new Size(75, 74);
            size[10] = new Size(75, 82);
            */
            return size;
        }
        private int[] Random()
        {
            int[] lotteryPool = new int[5];
            int[] lotteryPool1 = new int[5];
            for (int x = 0; x < lotteryPool.Length; x++) // generates an array of all 50 possible numbers
            {
                lotteryPool[x] = x + 1;
            }

            Random rnd = new Random();
            int randomIndex;
            int y;
            int dem = 0;
            for (y = 0; y < 5; y++)
            {
                randomIndex = rnd.Next(0, 5);
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

        private void timer4_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
        }

       
       

    }
}
