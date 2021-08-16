using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Signup
{
    public partial class Form1 : Form

    {

        //CS Connection string
        string cs = ConfigurationManager.ConnectionStrings["rscs"].ConnectionString;


        public Form1()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image ";
            // ofd.Filter = "PNG File(*.png)|*.png";
            //  ofd.Filter = "JPG File(*.jpg)|*.jpg";
            ofd.Filter = "All Image File(*.*)|*.*";
            //ofd.ShowDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into solution_details values (@name, @phone, @role, @pass, @img)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@role", comboBox1.SelectedItem);
            cmd.Parameters.AddWithValue("@pass", textBox3.Text);
            cmd.Parameters.AddWithValue("@img", SavePhote());

            con.Open();

            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted Successfully !");

            }
              else
            {
                MessageBox.Show("Data not updated !");

            }

        }

        private byte[] SavePhote()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
           
        }
        void BindGridView()
        {
            // Connection Between GridView & Database
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from solution_details";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);


            /// data in Gridview
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }


    }
}
