using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextNumber
{
    public partial class EnterName : Form
    {
        public string playerN { get; set; }
        public EnterName()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            playerN = textBox1.Text;
        }
    }
}
