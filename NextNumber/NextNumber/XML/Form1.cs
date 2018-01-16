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

namespace XML
{
    public partial class Form1 : Form
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
       string filename = @"C:\Users\DUCTRAN\Downloads\NextNumber\XML\Data.xml";
        public Form1()
        {
            InitializeComponent();
        }
        int dem = 0;
      //  public static string path = @"C:\Users\DUCTRAN\Downloads\NextNumber\XML\Data.xml";
        void Sava()
        {
            XmlElement sach = doc.CreateElement("sach");
            XmlElement masach = doc.CreateElement("masach");
            masach.InnerText = "1223";
            sach.AppendChild(masach);
            XmlElement tensach = doc.CreateElement("tensach");
            tensach.InnerText = "hello";
            sach.AppendChild(tensach);
            root.AppendChild(sach);
            doc.Save(filename);
                
        }
      
        void DELETEE()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\Users\DUCTRAN\Downloads\NextNumber\XML\Data.xml");
            foreach (XmlNode xNode in xmlDoc.SelectNodes("sach[masach='"+1223+"']"))
            {
                if (xNode.SelectSingleNode("user").InnerText == "John Doe")
                {
                    xNode.RemoveAll();
                }
            }
            xmlDoc.Save(@"C:\Users\DUCTRAN\Downloads\NextNumber\XML\Data.xml");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            doc.Load(filename);
            root = doc.DocumentElement;
            Sava();
           // DELETEE();
        }
    }
}
