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
    public partial class Array : Form
    {
        public Array()
        {
            InitializeComponent();
        }

        private void bt_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đang nhấn nút " + ((Button)sender).Tag);
        }

        void CreateArrayButton()
        {
            int top = 0; //Giá  trị Location Top
            for (int i = 0; i < 10; i++)
            {
                int Left = 0; //Giá trị Location Left
                for (int j = 0; j < 10; j++)
                {
                    //Tạo 1 button mới
                    Button bt = new Button();
                    //Thêm cài đặt cho button
                    bt.Name = "Button" + i + " " + j;
                   
                    bt.Size = new Size(50, 50);
                    bt.Left = Left += 50;
                    bt.Top = top;
                    bt.BackColor = Color.Green;
                    bt.ForeColor = Color.White;

                    //Tạo sự kiên cho button và gán tới hàm sử lý sự kiện
                    bt.Click += new EventHandler(bt_Click);
                    this.Controls.Add(bt);
                }
                top += 50;
            }
        }

        private void Array_Load(object sender, EventArgs e)
        {
            CreateArrayButton();
        }
    }
}
