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
using System.Diagnostics.Tracing;

namespace Try1{
    public partial class ToDoList : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ToDoList;Integrated Security=True");
        public ToDoList()
        {
            InitializeComponent();
        }


        void Populate()
        {
            try
            {
                Con.Open();
                string myquery = "SELECT * from ToDoList3 where DateSelected = '" + Calendar.SelectionStart + "'";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(myquery, Con);

                da.Fill(dt);
                DataGrid.DataSource = null;
                DataGrid.AutoGenerateColumns = false;
                DataGrid.ColumnCount = 6;
                    DataGrid.Columns[0].HeaderText = "To Do";
                        DataGrid.Columns[0].DataPropertyName = "ToDo";

                    DataGrid.Columns[1].HeaderText = "Priority";
                        DataGrid.Columns[1].DataPropertyName = "Priority";

                    DataGrid.Columns[2].HeaderText = "Duration (mins)";
                        DataGrid.Columns[2].DataPropertyName = "Duration";

                    DataGrid.Columns[3].HeaderText = "Due Date";
                        DataGrid.Columns[3].DataPropertyName = "DueDate";

                    DataGrid.Columns[4].HeaderText = "Comments";
                        DataGrid.Columns[4].DataPropertyName = "Comments";

                    DataGrid.Columns[5].HeaderText = "Completed";
                        DataGrid.Columns[5].DataPropertyName = "Completed";

                DataGrid.DataSource= dt;
                Con.Close();
                Progress();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        void Clear()
        {
            ToDoTb.Text = "";
            PriorityTb.Text = "";
            DurationTb.Text = "";
            DueDateTb.Text = "";
            CommentsTb.Text = "";
            CompletedCb.Checked = false;
        }

        void Progress()
        {
            Con.Open();
            string myquery1 = "SELECT COUNT(*) from ToDoList3 where DateSelected = '" + Calendar.SelectionStart + "'";
            SqlCommand cmd1 = new SqlCommand(myquery1, Con);
            Int32 countTotal = (Int32)cmd1.ExecuteScalar();

            string myquery2 = "SELECT COUNT(*) from ToDoList3 where DateSelected = '" + Calendar.SelectionStart + "' AND Completed = 1";
            SqlCommand cmd2 = new SqlCommand(myquery2, Con);
            Int32 countCheck = (Int32)cmd2.ExecuteScalar();
            // MessageBox.Show((100*countCheck / countTotal).ToString());

            if (countTotal > 0)
            {
                ProgressBar.Value = 100 * countCheck / countTotal;
            }
            else
            {
                ProgressBar.Value = 0;
            }

            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (Selector.Text == "ADD")
            {
         
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT into ToDoList3 values('" + ToDoTb.Text + "', '" + Int32.Parse(PriorityTb.Text) + "', '" + float.Parse(DurationTb.Text) + "', '" + DueDateTb.Text + "', '" + CommentsTb.Text + "', '" + CompletedCb.Checked + "', '" + Calendar.SelectionStart + "')", Con);

                    cmd.ExecuteNonQuery();
                    // MessageBox.Show("Work sucessfully added");
                    Con.Close();

                    Populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Delete //
            if(Selector.Text == "DELETE")
            {
                try
                {
                    if (ToDoTb.Text == "")
                    {
                        MessageBox.Show("Please enter work to do to delete");
                    }
                    else
                    {
                        Con.Open();
                        string myquery = "DELETE from ToDoList3 where ToDo = '" + ToDoTb.Text + "' AND DateSelected = '" + Calendar.SelectionStart + "'";
                        SqlCommand cmd = new SqlCommand(myquery, Con);
                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("It has been deleted");
                        Con.Close();
                        Populate();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (Selector.Text == "DELETE ALL")
            {
                try
                {
                    if (ToDoTb.Text == "")
                    {
                        MessageBox.Show("Please enter work to do to delete");
                    }
                    else
                    {
                        Con.Open();
                        string myquery = "DELETE from ToDoList3 where ToDo = '" + ToDoTb.Text + "'";
                        SqlCommand cmd = new SqlCommand(myquery, Con);
                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("It has been deleted");
                        Con.Close();
                        Populate();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Edit //
            if (Selector.Text == "EDIT")
            {
                try
                {
                    if (ToDoTb.Text == "")
                    {
                        MessageBox.Show("Please enter work to do to edit");
                    }
                    else
                    {
                        Con.Open();
                        string myquery = "UPDATE ToDoList3 set Priority = '" + Int32.Parse(PriorityTb.Text) + "', Duration = '" + float.Parse(DurationTb.Text) + "', DueDate = '" + DueDateTb.Text + "', Comments = '" + CommentsTb.Text + "', Completed = '" + CompletedCb.Checked + "' where ToDo = '" + ToDoTb.Text + "' AND DateSelected = '" + Calendar.SelectionStart + "'";
                        SqlCommand cmd = new SqlCommand(myquery, Con);
                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("It has been updated");
                        Con.Close();
                        Populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (Selector.Text == "EDIT ALL")
            {
                try
                {
                    if (ToDoTb.Text == "")
                    {
                        MessageBox.Show("Please enter work to do to edit");
                    }
                    else
                    {
                        Con.Open();
                        string myquery = "UPDATE ToDoList3 set Priority = '" + Int32.Parse(PriorityTb.Text) + "', Duration = '" + float.Parse(DurationTb.Text) + "', DueDate = '" + DueDateTb.Text + "', Comments = '" + CommentsTb.Text + "', Completed = '" + CompletedCb.Checked + "' where ToDo = '" + ToDoTb.Text + "'";
                        SqlCommand cmd = new SqlCommand(myquery, Con);
                        cmd.ExecuteNonQuery();
                        // MessageBox.Show("It has been updated");
                        Con.Close();
                        Populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ToDoList_Load(object sender, EventArgs e)
        {
            Populate();
        }

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Selector.Text == "EDIT" ^ Selector.Text == "DELETE" ^ Selector.Text == "EDIT ALL" ^ Selector.Text == "DELETE ALL")
            {
                ToDoTb.Text = DataGrid.SelectedRows[0].Cells[0].Value.ToString();
                PriorityTb.Text = DataGrid.SelectedRows[0].Cells[1].Value.ToString();
                DurationTb.Text = DataGrid.SelectedRows[0].Cells[2].Value.ToString();
                DueDateTb.Text = DataGrid.SelectedRows[0].Cells[3].Value.ToString();
                CommentsTb.Text = DataGrid.SelectedRows[0].Cells[4].Value.ToString();
                CompletedCb.Checked = Boolean.Parse(DataGrid.SelectedRows[0].Cells[5].Value.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void Calendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            Populate();
        }
    }

    
}
