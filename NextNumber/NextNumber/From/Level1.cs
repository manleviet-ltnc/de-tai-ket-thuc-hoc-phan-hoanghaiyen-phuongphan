﻿using System;
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

           
            int[] h = new int[37];
            h = Random();

            int top = 0; //Giá  trị Location Top
            int k = 0;
            for (int i = 0; i < 6; i++)
            {
                
                int Left = 0; //Giá trị Location Left
                for (int j = 0; j < 6; j++)
                {
                    //Tạo 1 button mới
                    Button bt = new Button();
                    //Thêm cài đặt cho button
                  
                    bt.Text = h[k+j].ToString();
                    bt.Size = new Size(40, 40);
                    bt.Left = Left;// += 50;
                    Left += 40;
                    bt.Top = top;
                  

                    //Tạo sự kiên cho button và gán tới hàm sử lý sự kiện
                    bt.Click += new EventHandler(Myclick);
                    panel1.Controls.Add(bt);
                    //this.Controls.Add(bt);
                }
                k += 6;
                top += 40;
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

            if (demClick == 36)
            {
                for (int i = 1; i < 37; i++)
                {
                    if (arr[i] == i)
                    {
                        if (i == 36)
                        {
                            won();
                        }
                       
                    }
                    else
                    {
                       
                        lost();
                        break;
                    }
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
      
        private int[] Random()
        {
            int[] lotteryPool = new int[37];
            int[] lotteryPool1 = new int[37];
            for (int x = 0; x < lotteryPool.Length; x++) // generates an array of all 50 possible numbers
            {
                lotteryPool[x] = x + 1;
            }

            Random rnd = new Random();
            int randomIndex;
            int y;
            int dem = 0;
            for (y = 0; y < 37; y++)
            {
                randomIndex = rnd.Next(0, 37); 
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
