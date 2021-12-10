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

namespace StudentRegistration
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        SqlConnection sqlConnection = new SqlConnection("Data Source= DESKTOP-2H89C5Q\\SQLEXPRESS; Initial Catalog=StudentRegistration;Integrated Security=True;MultipleActiveResultSets=true;");
        SqlCommand SqlCommand;
        SqlDataReader SqlDataReader;
        SqlDataAdapter SqlDataAdapter;
        string id;
        bool Mode = true;
        string sql;
        public void Load()
        {
            try
            {
                sql = "select * from Student";
                SqlCommand = new SqlCommand(sql, sqlConnection);
                sqlConnection.Open();

                SqlDataReader = SqlCommand.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (SqlDataReader.Read())
                {
                    dataGridView1.Rows.Add(SqlDataReader[0], SqlDataReader[1], SqlDataReader[2], SqlDataReader[3]);

                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void getId(string id)
        {
            sql = "select * from Student where id='" + id + "'";
            SqlCommand = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            SqlDataReader = SqlCommand.ExecuteReader();

            while (SqlDataReader.Read())
            {
                txtName.Text = SqlDataReader[1].ToString();
                txtCourse.Text = SqlDataReader[2].ToString();
                txtFee.Text = SqlDataReader[3].ToString();



            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if (Mode)
            {
                sql = "insert into Student(StudentName, Course, Fee) values (@StudentName, @Course, @Fee)";
                sqlConnection.Open();
                SqlCommand = new SqlCommand(sql,sqlConnection);

                SqlCommand.Parameters.AddWithValue("@StudentName", name);
                SqlCommand.Parameters.AddWithValue("@Course", course);
                SqlCommand.Parameters.AddWithValue("@Fee", fee);

                MessageBox.Show("Record Added!");
                SqlCommand.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();

                txtName.Focus();
                sqlConnection.Close();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update Student set StudentName= @StudentName, Course=@Course, Fee=@Fee where id = @id";
                //sqlConnection.Open();
                SqlCommand = new SqlCommand(sql, sqlConnection);

                SqlCommand.Parameters.AddWithValue("@StudentName", name);
                SqlCommand.Parameters.AddWithValue("@Course", course);
                SqlCommand.Parameters.AddWithValue("@Fee", fee);
                SqlCommand.Parameters.AddWithValue("@id", id);

                MessageBox.Show("Record Updated!");
                SqlCommand.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();

                txtName.Focus();
                button2.Text = "Save";
                Mode = true;

            }
            sqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();

            txtName.Focus();
            button2.Text = "Save";
            Mode = true;
        }
    }
}
