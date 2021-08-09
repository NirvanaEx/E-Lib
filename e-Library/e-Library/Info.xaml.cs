using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;
using System.IO;


namespace e_Library
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
        }
        private void ShowRecord(DataGrid DBGrid, String komanda)
        {
            OleDbConnection DBConnection = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";");
            DBConnection.Open();
            DataSet SetData = new DataSet();

            OleDbDataAdapter DBQuery = new OleDbDataAdapter(komanda, DBConnection);
            DBQuery.Fill(SetData);
            DBGrid.ItemsSource = SetData.Tables[0].DefaultView;

            DBConnection.Close();
        }
        private void search(String s)
        {
            OleDbConnection connect = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";");
            connect.Open();
            OleDbCommand command = new OleDbCommand(s, connect);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataSet data = new DataSet();
            adapter.Fill(data, "Lib");



            connect.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowRecord(dataGrid1, "Select * From Lib");
        }
       
        private void textBoxLibrary1_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            if (textBoxLibrary1.Text != "")
            {
                int num = 0;
                try
                {
                    WrapPanel1.Children.Clear();
                   
                    
                    using (OleDbConnection connection = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";"))
                    {

                        OleDbDataAdapter adapter;
                        adapter = new OleDbDataAdapter("SELECT Name FROM [Lib]", connection);

                        OleDbConnection connect = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";");
                        connect.Open();
                        OleDbCommand command = new OleDbCommand("Select Count(*) From Lib", connect);

                        if (name.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Name LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where name LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (author.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Avtor LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Avtor LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (redac.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Redaction LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Redaction LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (year.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Year_Izdaniya LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Year_Izdaniya LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (pages.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Pages LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Pages LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (category.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Category LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Category LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        if (limit.IsChecked == true)
                        {
                            adapter = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Limit LIKE '" + textBoxLibrary1.Text + "%')", connection);
                            command = new OleDbCommand("Select Count(*) From Lib Where Limit LIKE '" + textBoxLibrary1.Text + "%'", connect);
                        }
                        
                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset, "Lib");

                        num = (int)command.ExecuteScalar();
                        connect.Close();

                        if (num > 0)
                        {
                            List<String> names = new List<String>();
                            List<string> avtors = new List<string>();
                            List<string> redactions = new List<string>();
                            List<string> years = new List<string>();
                            List<string> pagess = new List<string>();
                            List<string> categegoryes = new List<string>();
                            List<string> limites = new List<string>();
                      


                            foreach (DataRow row in dataset.Tables["Lib"].Rows)
                            {
                                names.Add(row["name"].ToString());
                                avtors.Add(row["avtor"].ToString());
                                redactions.Add(row["redaction"].ToString());
                                years.Add(row["year_izdaniya"].ToString());
                                pagess.Add(row["pages"].ToString());
                                categegoryes.Add(row["category"].ToString());
                                limites.Add(row["limit"].ToString());

                                
                            }
                            
                            listView1.ItemsSource = names;
                            
                           



                            BookControl[] bookControls = new BookControl[num];

                            for (int i = 0; i < num; i++)
                            {
                                bookControls[i] = new BookControl();
                                bookControls[i].groupBoxText(names.ElementAt(i));
                                bookControls[i].setLabelText(avtors.ElementAt(i), redactions.ElementAt(i), years.ElementAt(i), pagess.ElementAt(i), categegoryes.ElementAt(i), limites.ElementAt(i));
                                WrapPanel1.Children.Add(bookControls[i]);
                            }
                        
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " THIS textbox " + num.ToString());
                }
            }
            else
            {
                listView1_Loaded(sender, e);
            }
        }
   
        private void listView1_Loaded(object sender, RoutedEventArgs e)
        {
           

            WrapPanel1.Children.Clear();

            int num;
            OleDbConnection connect = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";");
            connect.Open();
            OleDbCommand command = new OleDbCommand("Select Count(*) From Lib", connect);
            num = (int)command.ExecuteScalar();

            using (OleDbConnection connection = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";"))
            {

                OleDbDataAdapter adapter;
                adapter = new OleDbDataAdapter("SELECT * FROM [Lib]", connection);

                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "Lib");

                List<String> names = new List<String>();
                List<string> avtors = new List<string>();
                List<string> redac = new List<string>();
                List<string> year = new List<string>();
                List<string> pages = new List<string>();
                List<string> categ = new List<string>();
                List<string> limit = new List<string>();
           



        

                foreach (DataRow row in dataset.Tables["Lib"].Rows)
                {
                    names.Add(row["name"].ToString());
                    avtors.Add(row["avtor"].ToString());
                    redac.Add(row["redaction"].ToString());
                    year.Add(row["year_izdaniya"].ToString());
                    pages.Add(row["pages"].ToString());
                    categ.Add(row["category"].ToString());
                    limit.Add(row["limit"].ToString());

                 
                }
                
                listView1.ItemsSource = names;
                connection.Close();

                BookControl[] bookControls = new BookControl[num];

                for (int i = 0; i < num; i++)
                {
                    bookControls[i] = new BookControl();
                    bookControls[i].groupBoxText(names.ElementAt(i));
                    bookControls[i].setLabelText(avtors.ElementAt(i), redac.ElementAt(i), year.ElementAt(i), pages.ElementAt(i), categ.ElementAt(i), limit.ElementAt(i));
                    WrapPanel1.Children.Add(bookControls[i]);
                }

            }


        }

        private void textBoxLibrary1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            textBoxLibrary1.Text = "";
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            OleDbConnection DBConnection = new OleDbConnection("Data Source=\"Library.mdb\";User " + "ID=Admin;Provider=\"Microsoft.Jet.OLEDB.4.0\";");
            DBConnection.Open();
            DataSet SetData = new DataSet();
            OleDbDataAdapter DBQuery = null;
            if (comboBoxFilter.Text == "Имя")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Name LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Автор")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Avtor LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Редакция")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Redaction LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Год издания")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Year_Izdaniya LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Страницы")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Pages LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Категория")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Category LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            if (comboBoxFilter.Text == "Ограничение")
            {
                DBQuery = new OleDbDataAdapter("SELECT * FROM [Lib] WHERE (Limit LIKE '" + textBox1.Text + "%')", DBConnection);
            }
            DBQuery.Fill(SetData);
            dataGrid1.ItemsSource = SetData.Tables[0].DefaultView;
            DBConnection.Close();
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(dataGrid1.SelectedIndex.ToString());

        }
    }

}
