using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Terrios.MVVM.Model;

namespace Terrios
{
    public partial class MainWindow : Window
    {
        private static bool isadmin = false;
        public bool getisadmin() { return isadmin; }
        public object nom { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        // Click on the login image
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked hide the InputBox
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            String inputEmail = Nom.Text;
            String inputPassword = Password.Text;

            // Clear InputBox.
            Nom.Text = String.Empty;
            Password.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            // Clear InputBox.
            Nom.Text = String.Empty;
            Password.Text = String.Empty;
        }



        // Click on the submit button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // MySql connections params
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "terrios";


                MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
                MyCon.Open();

                string sql = "SELECT * FROM `employes` WHERE `nom`=@Nom AND `password`=@Password;";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nom", Nom.Text);
                cmd.Parameters.AddWithValue("@Password", Password.Text);
                MySqlDataReader reader = cmd.ExecuteReader();


                // Message box if nom and password is correct
                if (reader.HasRows)
                {
                        isadmin = true;
                    while (reader.Read())
                    {
                        MessageBox.Show("Bienvenu " + reader["nom"]);
                        InputBox.Visibility = System.Windows.Visibility.Collapsed;
                        MyListBox.Text = Convert.ToString(reader["nom"]);
                    }
                    MyCon.Close();
                } else
                {
                    MessageBox.Show("Le nom ou le mot de passe est incorrect");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }
        }
    }
}

