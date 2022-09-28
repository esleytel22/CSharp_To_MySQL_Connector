using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace CSharp_Mysql_Connector
{
    public partial class Form1 : Form
    {

        Bitmap bitmap;

        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd; 

        DataSet DS = new DataSet(); 

        String server = "localhost";
        String username = "root";
        String password = "kccc2003"; 
        String datatbase = "stucon"; 


        private void uploadData()
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + 
                "password=" + password + ";" + "database=" + datatbase; 
            sqlConn.Open();
            sqlCmd.Connection = sqlConn; 
            sqlCmd.CommandText = "SELECT * FROM stucon.stucon"; 

            sqlRd = sqlCmd.ExecuteReader(); 
            sqlDt.Load(sqlRd); 

            sqlRd.Close(); 
            sqlConn.Close();   
            dataGridView1.DataSource = sqlDt; 
        }
        public Form1()
        {
            InitializeComponent();
        }

           private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {


                iExit = MessageBox.Show("Confirm if you want to Exit", "Mysql Connector", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (iExit == DialogResult.Yes)
                {
                    Application.Exit();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(Control c in panel4.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear(); 
                }
                txtSearch.Text = ""; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));

                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

}

        private void Form1_Load(object sender, EventArgs e)
        {
            uploadData(); 
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
                "password=" + password + ";" + "database=" + datatbase;


            try
            {
                sqlConn.Open();
                sqlQuery = "insert into stucon.stucon (Studentid, Firstname, Surname, Address, Postcode, Telephone)" +
                    "values('" + StudentBox.Text + "' , '" + FirstNameBox.Text + "' , '" + LastNameBox.Text + "' , '" +
                    AddressBox.Text + "' , '" + PostalBox.Text + "' , '" + PhoneBox.Text + "')";

                sqlCmd = new MySqlCommand(sqlQuery, sqlConn); 
                sqlRd = sqlCmd.ExecuteReader();

                sqlConn.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
            finally
            {
                sqlConn.Close(); 
            }
            uploadData(); 
        }

        private void FirstNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void StudentBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LastNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
              "password=" + password + ";" + "database=" + datatbase;

            sqlConn.Open(); 

            try
            {
                MySqlCommand mySqlCmd = new MySqlCommand();
                sqlCmd.Connection = sqlConn;

                sqlCmd.CommandText = "Update stucon.stucon set Studentid = @Studentid, Firstname = @Firstname," +
                    "Surname = @Surname, Address = @Address, Postcode = @Postcode, Telephone = @Telephone " +
                    "where Studentid = @Studentid";

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Studentid", StudentBox.Text);
                sqlCmd.Parameters.AddWithValue("@Firstname", FirstNameBox.Text);
                sqlCmd.Parameters.AddWithValue("@Surname", LastNameBox.Text);
                sqlCmd.Parameters.AddWithValue("@Address", AddressBox.Text);
                sqlCmd.Parameters.AddWithValue("@Postcode", PostalBox.Text);
                sqlCmd.Parameters.AddWithValue("@Telephone", PhoneBox.Text);

                //insert update delete function data
                sqlCmd.ExecuteNonQuery();

                sqlConn.Close();
                uploadData(); 
           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                StudentBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                FirstNameBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                LastNameBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                AddressBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                PostalBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                PhoneBox.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" +
             "password=" + password + ";" + "database=" + datatbase;

            sqlConn.Open(); 
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "delete from stucon.stucon where Studentid = @Studentid";
            sqlCmd = new MySqlCommand(sqlQuery, sqlConn);
            sqlConn.Close();

            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index); 
            }

            uploadData(); 



        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

           
            
            try
            {

                //StudentBox.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                DataView dv = sqlDt.DefaultView;
                dv.RowFilter = string.Format(StudentBox.Text, txtSearch.Text); 
                    
                    //Format("Studentid Like'%{0}'", txtSearch.Text);
                dataGridView1.DataSource = dv.ToTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            sqlDt.Load(sqlRd);
            dataGridView1.DataSource = sqlDt;
        }
    }
}
