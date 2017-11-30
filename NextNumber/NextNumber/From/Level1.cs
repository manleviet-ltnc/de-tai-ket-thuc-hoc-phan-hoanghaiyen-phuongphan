using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NextNumber.From
{
    public partial class Level1 : Form
    {
        int demClick = 0;
        public Level1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
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
        int[] arr = new int[8];
        private void Myclick(object sender, EventArgs e)
        {

            int x = Int32.Parse((sender as Button).Text);

            demClick++;
            arr[demClick] = x;
            (sender as Button).Enabled = false;

            if (demClick == 7)
            {
                if (arr[1] == 1 && arr[2] == 2 && arr[3] == 3 && arr[4] == 4 && arr[5] == 5 && arr[6] == 6 && arr[7] == 7)
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
            for (int x = 0; x < lotteryPool.Length; x++)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
