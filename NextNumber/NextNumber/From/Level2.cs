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
        private int demClick = 0;
        private int _counter;
        private bool _isStart;
        private bool _isEnd;
        private bool _isDie;
        private const int DEFAULT_TIME = 30;
        int[] arr = new int[19];

        public Level2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!timer2.Enabled)
            {
                _counter = 0;
                timer2.Interval = 1000;
                timer2.Tick += timer2_Tick;
                timer2.Start();
                timer2.Enabled = true;
                _isStart = true;
            }

            Button[] btArr = new Button[18];
            int[] h = new int[18];
            h = Random();
            Random rd = new Random();
            Point[] poit = new Point[18];
            Size[] size = new Size[18];
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
                PaneLevel2.Controls.Add(btArr[i]);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lb_time.Text = Math.Abs(_counter - DEFAULT_TIME).ToString("00");
            if (!_isEnd && _counter == DEFAULT_TIME)
            {
                timer2.Stop();
                _isEnd = true;
                timer2.Enabled = false;
                MessageBox.Show("thua cuộc");
            }
            _counter++;
        }

        private void Myclick(object sender, EventArgs e)
        {
            int x = Int32.Parse((sender as Button).Text);

            demClick++;
            arr[demClick] = x;
            (sender as Button).Enabled = false;

            if (demClick == 18)
            {
                if (arr[1] == 1 && arr[2] == 2 && arr[3] == 3 && arr[4] == 4 && arr[5] == 5 && arr[6] == 6 && arr[7] == 7 && arr[8] == 8 && arr[9] == 9 && arr[10] == 10 && arr[11] == 11 && arr[12] == 12 && arr[13] == 13 && arr[14] == 14 && arr[15] == 15 && arr[16] == 16 && arr[17] == 17 && arr[18] == 18)
                {
                    MessageBox.Show("Chúc mừng bạn đã chiến thắng");
                }
                else
                {
                    MessageBox.Show("Thua cuộc, Thử lại lần nữa");
                }
            }
        }
        private Point[] InitPoint()
        {
            Point[] poit = new Point[18];
            poit[0] = new Point(0, 0);
            poit[1] = new Point(96, 0);
            poit[2] = new Point(185, 0);
            poit[3] = new Point(248, 0);
            poit[4] = new Point(96, 35);
            poit[5] = new Point(151, 35);
            poit[6] = new Point(0, 83);
            poit[7] = new Point(58, 83);
            poit[8] = new Point(104, 83);
            poit[9] = new Point(194, 83);
            poit[10] = new Point(58, 131);
            poit[11] = new Point(148, 130);
            poit[12] = new Point(214, 131);
            poit[13] = new Point(214, 177);
            poit[14] = new Point(57, 179);
            poit[15] = new Point(104, 179);
            poit[16] = new Point(58, 225);
            poit[17] = new Point(148, 215);
            return poit;
        }

        private Size[] InitSize()
        {
            Size[] size = new Size[18];
            size[0] = new Size(99, 84);
            size[1] = new Size(91, 37);
            size[2] = new Size(68, 37);
            size[3] = new Size(48, 37);
            size[4] = new Size(58, 49);
            size[5] = new Size(145, 49);
            size[6] = new Size(59, 188);
            size[7] = new Size(48, 49);
            size[8] = new Size(91, 49);
            size[9] = new Size(102, 49);
            size[10] = new Size(91, 49);
            size[11] = new Size(69, 86);
            size[12] = new Size(82, 49);
            size[13] = new Size(82, 40);
            size[14] = new Size(48, 49);
            size[15] = new Size(45, 49);
            size[16] = new Size(91, 49);
            size[17] = new Size(148, 59);
            return size;
        }
        private int[] Random()
        {
            int[] lotteryPool = new int[18];
            int[] lotteryPool1 = new int[18];
            for (int x = 0; x < lotteryPool.Length; x++) // generates an array of all 50 possible numbers
            {
                lotteryPool[x] = x + 1;
            }

            Random rnd = new Random();
            int randomIndex;
            int y;
            int dem = 0;
            for (y = 0; y < 18; y++)
            {
                randomIndex = rnd.Next(0, 18);
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
    }
}
