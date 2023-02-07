using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using Terrios.MVVM.Model;
using System.Windows.Controls.Primitives;

namespace Terrios.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour EmployeView.xaml
    /// </summary>
    public partial class EmployeView : UserControl
    {
        MainWindow Parentwindow = (MainWindow)Application.Current.MainWindow;
        public EmployeView()
        {
            InitializeComponent();

            // Check if admin and put button on visible
            if (Parentwindow.getisadmin() == true)
            {
                insertservice.Visibility = System.Windows.Visibility.Visible;
                deleteservice.Visibility = System.Windows.Visibility.Visible;
                deletesite.Visibility = System.Windows.Visibility.Visible;
                insertsite.Visibility = System.Windows.Visibility.Visible;

            }

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "terrios";


            MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
            MyCon.Open();

            // Load the datagrid of service
            string sqlservice = "SELECT * FROM `services`;";
            MySqlCommand cmdservice = new MySqlCommand(sqlservice, MyCon);
            MySqlDataReader readerservice = cmdservice.ExecuteReader();

            List<ServiceModel> listServiceModel = new List<ServiceModel>();

            while (readerservice.Read())
            {
                ServiceModel _service = new ServiceModel(Convert.ToInt32(readerservice["idservice"]), Convert.ToString(readerservice["nomservice"]));
                listServiceModel.Add(_service);
            }
            DataGridService.ItemsSource = listServiceModel;
            MyCon.Close();


            // Datas for the location datagrid
            MyCon.Open();
            string sqlsite = "SELECT * FROM `site`;";
            MySqlCommand cmdsite = new MySqlCommand(sqlsite, MyCon);
            MySqlDataReader readersite = cmdsite.ExecuteReader();

            List<SiteModel> listSiteModel = new List<SiteModel>();

            while (readersite.Read())
            {
                SiteModel _site = new SiteModel(Convert.ToInt32(readersite["idsite"]), Convert.ToString(readersite["adresse"]), Convert.ToString(readersite["ville"]), Convert.ToString(readersite["nomsite"]));
                listSiteModel.Add(_site);
            }
            DataGridSite.ItemsSource = listSiteModel;

            readerservice.Close();
            MyCon.Close();


        }

        // Code for the pop page of new service
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            InsertServicebox.Visibility = System.Windows.Visibility.Visible;
        }


        // Code for the insert of a new service
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "terrios";


                MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());

                MyCon.Open();
                string sql = "INSERT INTO `services`(`idservice`,`nomservice`) VALUES (NULL,@nomservice);";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                cmd.Parameters.AddWithValue("@nomservice", insertnomservice.Text);
                var count = cmd.ExecuteNonQuery();
                if (count == 1)
                {

                    MessageBox.Show("Vous avez inseré " + count + " service nommé " + insertnomservice.Text);
                    InsertServicebox.Visibility = System.Windows.Visibility.Collapsed;

                }
                MyCon.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }


        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InsertServicebox.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Code to submit the insert of a new location
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "terrios";


                MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());


                MyCon.Open();
                string sql = "INSERT INTO `site`(`idsite`,`adresse`,`ville`,`nomsite`) VALUES (NULL,@adresse,@ville,@nomsite);";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                cmd.Parameters.AddWithValue("@adresse", insertadressesite.Text);
                cmd.Parameters.AddWithValue("@ville", insertvillesite.Text);
                cmd.Parameters.AddWithValue("@nomsite", insertnomsite.Text);
                var count = cmd.ExecuteNonQuery();
                if (count == 1)
                {

                    MessageBox.Show("Vous avez inseré " + count + " site nommé " + insertnomsite.Text);
                    InsertSitebox.Visibility = System.Windows.Visibility.Collapsed;

                }
                MyCon.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            InsertSitebox.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            InsertSitebox.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            ServiceModel Servicedelete = (ServiceModel)DataGridService.SelectedItem;

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "terrios";


            MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
            MyCon.Open();

            string sql = "SELECT * FROM `employes` WHERE idservice = @idservice;";
            MySqlCommand cmd = new MySqlCommand(sql, MyCon);
            cmd.Parameters.AddWithValue("@idservice", Servicedelete.Idservice);

            MySqlDataReader readerdel = cmd.ExecuteReader();
            if (readerdel.Read() == true)
            {
                MessageBox.Show("Des employés sont dans la base de données, vous ne pouvez pas supprimer ce service");
            }
            else
            {
                readerdel.Close();
                try
                {
                    string sqldel = "DELETE FROM services WHERE `services`.`idservice` = @idservicedel;";
                    MySqlCommand cmddel = new MySqlCommand(sqldel, MyCon);
                    cmddel.Parameters.AddWithValue("@idservicedel", Servicedelete.Idservice);
                    var countdel = cmddel.ExecuteNonQuery();
                    if (countdel == 1)
                    {
                        MessageBox.Show("Vous avez supprimé service");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Problème" + ex);
                }
            }
        }

        private void Button_Delete_2(object sender, RoutedEventArgs e)
        {
            SiteModel Sitedelete = (SiteModel)DataGridSite.SelectedItem;

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "terrios";


            MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
            MyCon.Open();

            string sql = "SELECT * FROM `employes` WHERE idsite = @idsite;";
            MySqlCommand cmd = new MySqlCommand(sql, MyCon);
            cmd.Parameters.AddWithValue("@idsite", Sitedelete.Idsite);

            MySqlDataReader readerdel = cmd.ExecuteReader();
            if (readerdel.Read() == true)
            {
                MessageBox.Show("Des employés sont dans la base de données, vous ne pouvez pas supprimer ce site");
            }
            else
            {
                readerdel.Close();
                try
                {
                    string sqldel = "DELETE FROM site WHERE `site`.`idsite` = @idsitedel;";
                    MySqlCommand cmddel = new MySqlCommand(sqldel, MyCon);
                    cmddel.Parameters.AddWithValue("@idsitedel", Sitedelete.Idsite);
                    var countdel = cmddel.ExecuteNonQuery();
                    if (countdel == 1)
                    {
                        MessageBox.Show("Vous avez supprimé site");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Problème" + ex);
                }
            }
        }
    }

}
