using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data;
namespace NextNumber
{
    public partial class HighestScore : Form
    {
        string filename = System.IO.Directory.GetCurrentDirectory() + @"\DuLieu.xml";
        public HighestScore()
        {
            InitializeComponent();
        }

        private void HighestScore_Load(object sender, EventArgs e)
        {
            try
            {
                
                XmlDataDocument xmlData = new XmlDataDocument();
                xmlData.DataSet.ReadXml(filename);
                dataGridView1.DataSource = xmlData.DataSet;
                dataGridView1.DataMember = "dsdiem";    
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
    }
}
