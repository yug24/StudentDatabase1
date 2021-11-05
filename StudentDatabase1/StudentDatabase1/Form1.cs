using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace StudentDatabase1
{
    public partial class Form1 : Form
    {
        private SQLiteConnection sqlconn;
        private SQLiteCommand sqlCmd;
        private DataTable sqlDT = new DataTable();
        private DataSet DS = new DataSet();
        private SQLiteDataAdapter DB;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void SetConnection()
        {
            sqlconn = new SQLiteConnection("Data Source=C:\\Users\\ADMIN\\Desktop\\BDAT 1004\\StudentDatabase1\\StudentDatabase1\\bin\\Debug\\Student.db");
        }

        private void ExecuteQuery(string StudentIDq)
        {
            SetConnection();
            sqlconn.Open();
            sqlCmd = sqlconn.CreateCommand();
            sqlCmd.CommandText = StudentIDq;
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlconn.Close();
        }

        private void LoadData()
        {
            SetConnection();
            sqlconn.Open();
            sqlCmd = sqlconn.CreateCommand();
            string CommandText = "select * from Student";
            DB = new SQLiteDataAdapter(CommandText, sqlconn);
            DS.Reset();
            DB.Fill(DS);
            sqlDT = DS.Tables[0];
            dataGridView1.DataSource = sqlDT;
            sqlconn.Close();

        }
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult Exit;
            Exit = MessageBox.Show("Comfirm If You Want To Exit", "Student Database", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(Exit==DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        void ClearAllText(Control con)
        {
            foreach(Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            cmb1.Text = "";
            cmb2.Text = "";
            rtTranscript.Text = "";
        }

        private void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt32(e.KeyChar);
            if(ascii!=8)
            {
                if(ascii>=48 && ascii<=57)
                {
                    e.Handled = false;
                }
                else
                {
                    MessageBox.Show("This Field Must Contain Only Numbers", "Error:Numbers Only", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudentID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            cmb1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txDMT.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtIES.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtDSA.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtBusiness.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtProgramming.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            txtMDA.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            txtTotal.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            txtAverage.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            txtRanking.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnStudentResult_Click(object sender, EventArgs e)
        {
           if(txDMT.Text=="")
            {
                txDMT.Text = "0";
            }
            if (txtIES.Text == "")
            {
                txtIES.Text = "0";
            }
            if (txtDSA.Text == "")
            {
                txtDSA.Text = "0";
            }
            if (txtBusiness.Text == "")
            {
                txtBusiness.Text = "0";
            }
            if (txtProgramming.Text == "")
            {
                txtProgramming.Text = "0";
            }
            if (txtMDA.Text == "")
            {
                txtMDA.Text = "0";
            }

            int[] r = new int[15];
            r[0] = Convert.ToInt32(txDMT.Text);
            r[1] = Convert.ToInt32(txtIES.Text);
            r[2] = Convert.ToInt32(txtDSA.Text);
            r[3] = Convert.ToInt32(txtBusiness.Text);
            r[4] = Convert.ToInt32(txtProgramming.Text);
            r[5] = Convert.ToInt32(txtDSA.Text);
            r[6] = (r[0] + r[1] + r[2] + r[3] + r[4] + r[5]) / 6;
            r[7] = r[0] + r[1] + r[2] + r[3] + r[4] + r[5];

            txtAverage.Text = Convert.ToString(r[6]);
            txtTotal.Text = Convert.ToString(r[7]);

            if ((r[7]) >= 500)
                txtRanking.Text = ("A");
            else if((r[7]) >= 400)
                txtRanking.Text = ("B");
            else if ((r[7]) >= 300)
                txtRanking.Text = ("C");
            else if ((r[7]) >= 200)
                txtRanking.Text = ("D");
            else if ((r[7]) < 200)
                txtRanking.Text = ("FAIL!");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string StudentIDq = "insert into Student (StudentID,CourseCode,Firstname,Surname,DMT,IES,DSA,Business,Programming,MDA,Total,Average,Ranking)values('" + txtStudentID.Text + "','" + cmb1.Text + "','" + txtFirstname.Text + "','"
                + textBox4.Text + "','" + txDMT.Text + "','" + txtIES.Text + "','" + txtDSA.Text + "','" + txtBusiness.Text + "','" + txtProgramming.Text + "','" + txtMDA.Text + "','" + txtTotal.Text + "','" + txtAverage.Text + "','"
                + txtRanking.Text + "')";
            ExecuteQuery(StudentIDq);
            LoadData();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string StudentIDq = "delete from Student where ID=id" + txtStudentID.Text + cmb1.Text + txtFirstname.Text + textBox4.Text +
                 txDMT.Text + txtIES.Text + txtDSA.Text + txtBusiness.Text + txtProgramming.Text +
                txtMDA.Text + txtTotal.Text + txtAverage.Text + txtRanking.Text ;

            ExecuteQuery(StudentIDq);
            LoadData();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtTranscript.AppendText("Student ID :" + txtStudentID.Text + "\n");
            rtTranscript.AppendText("Name : " + txtFirstname.Text + "\n");
            rtTranscript.AppendText("Course Code : " + cmb1.Text + "\n");
            rtTranscript.AppendText("Data Manipulation Techniques : " + txDMT.Text + "\n");
            rtTranscript.AppendText("Information Encoding St. : " + txtIES.Text + "\n");
            rtTranscript.AppendText("Data System Architecture : " + txtDSA.Text + "\n");
            rtTranscript.AppendText("Business Processes : " + txtBusiness.Text + "\n");
            rtTranscript.AppendText("Data Programming : " + txtProgramming.Text + "\n");
            rtTranscript.AppendText("Maths For DA : " + txtMDA.Text + "\n");
            rtTranscript.AppendText("Total Score : " + txtTotal.Text + "\n");
            rtTranscript.AppendText("Average : " + txtAverage.Text + "\n");
            rtTranscript.AppendText("Grade : " + txtRanking.Text + "\n");
        }
    }
}
