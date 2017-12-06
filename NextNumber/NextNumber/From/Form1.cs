using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NextNumber.From;

namespace NextNumber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            Level1 lv1 = new Level1();
            this.Hide();
            lv1.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
