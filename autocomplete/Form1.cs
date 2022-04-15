using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace autocomplete
{
    public partial class Form1 : Form
    {
        XmlSerializer xs;
        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("Data Source=Desktop-U561OEJ;Initial Catalog=autocomp;Integrated Security=True;Pooling=False");


            DataSet ds = new DataSet();
            ds.ReadXml(@"C:\Users\HELLO\Desktop\save.xml");
            //var p = XElement.Load("save.xml");
            AutoCompleteStringCollection datasource = new AutoCompleteStringCollection();
            //XElement e = p.Element("heading");
            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                datasource.Add(ds.Tables[0].Rows[i]["email"].ToString());

            }



            this.textBox1.AutoCompleteCustomSource = datasource;
            this.textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        public static void SaveData(object obj, string filename)
        {
            /*XmlSerializer sr = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);*/

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("save.xml");
            string[] f = File.ReadAllLines("save.xml");
            bool existeEl = false;
            foreach (var line in f)
            {
                if (line.Contains(textBox1.Text))
                {
                    existeEl = true;
                }
            }

            if (!existeEl)
            {
                XmlNode Auth = doc.CreateElement("acc");
                XmlNode email = doc.CreateElement("email");
                email.InnerText = textBox1.Text;
                Auth.AppendChild(email);
                doc.DocumentElement.AppendChild(Auth);
                doc.Save("save.xml");
            }
        }
    }
}
