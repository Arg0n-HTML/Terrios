using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using Terrios.MVVM.Model;

namespace Terrios.MVVM.View
{
    public partial class ServicesView : UserControl
    {
        MainWindow Parentwindow = (MainWindow)Application.Current.MainWindow;
        /* public static Boolean UpdateEmploye(int Idemploye)
         {

         }  **/

        private int resultOfCmbSite;
        private int resultOfCmbService;

        public ServicesView()
        {
            InitializeComponent();

            if (Parentwindow.getisadmin() == true)
            {
                buttoninsertemploye_Copy.Visibility = System.Windows.Visibility.Visible;
                buttondeleteemploye.Visibility = System.Windows.Visibility.Visible;
                buttondeleteemploye_Copy.Visibility = System.Windows.Visibility.Visible;
            }

            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "terrios";
            MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());
            MyCon.Open();

            
            string sql = "SELECT * FROM `employes` JOIN `site` ON employes.idsite = site.idsite JOIN `services` on employes.idservice = services.idservice;";
            MySqlCommand cmd = new MySqlCommand(sql, MyCon);
            MySqlDataReader reader = cmd.ExecuteReader();


            List<EmployeModel> listEmployesModel = new List<EmployeModel>();

            while (reader.Read())
            {
                EmployeModel _employe = new EmployeModel(Convert.ToInt32(reader["idemploye"]), Convert.ToString(reader["nom"]), Convert.ToString(reader["prenom"]), Convert.ToString(reader["tel_pro"]), Convert.ToString(reader["tel_perso"]), Convert.ToString(reader["email"]), Convert.ToString(reader["nomservice"]), Convert.ToString(reader["nomsite"]));
                listEmployesModel.Add(_employe);
            }
            DataGridEmploye.ItemsSource = listEmployesModel;
            MyCon.Close();
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
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
                string sql = "SELECT * FROM `employes`;";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MessageBox.Show("nom : " + reader["nom"] + "\n prenom : " + reader["prenom"]);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }
        }
        // Bouton pour submit le nouvel employé
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            InsertEmployebox.Visibility = System.Windows.Visibility.Visible;

        }

        // Bouton pour valider l'insertion d'un nouvel employé
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                // Récuperation de la valeur du combobox pour le site de l'employé
                string valueOfCmbSite = cmbSite.Text;
                string valueOfCmbService = cmbService.Text;
                //int resultOfCmbSite;
                //int resultOfCmbService;
                switch (valueOfCmbSite)
                {
                    case "Paris": resultOfCmbSite = 1; break;
                    case "Nantes": resultOfCmbSite = 2; break;
                    case "Toulouse": resultOfCmbSite = 3; break;
                    case "Nice": resultOfCmbSite = 4; break;
                    case "Lille": resultOfCmbSite = 5; break;
                }

                // Récuperation de la valeur du combobox pour le service de l'employé
                switch (valueOfCmbService)
                {
                    case "Informatique": resultOfCmbService = 1; break;
                    case "Commercial": resultOfCmbService = 2; break;
                    case "Comptabilité": resultOfCmbService = 3; break;
                    case "Administration": resultOfCmbService = 4; break;
                    case "Sécurité": resultOfCmbService = 5; break;
                    case "Ouvrier": resultOfCmbService = 6; break;
                    case "Communication": resultOfCmbService = 7; break;

                }
                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "terrios";


                MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());

                MyCon.Open();
                string sql = "INSERT INTO `employes`(`idemploye`, `nom`, `prenom`, `tel_pro`, `tel_perso`, `email`, `idsite`, `idservice`) VALUES " +
                 "(NULL,@nom,@prenom,@telpro,@telperso,@email,@idsite,@idservice)";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                cmd.Parameters.AddWithValue("@nom", insertnomemploye.Text);
                cmd.Parameters.AddWithValue("@prenom", insertprenomemploye.Text);
                cmd.Parameters.AddWithValue("@telpro", inserttelproemploye.Text);
                cmd.Parameters.AddWithValue("@telperso", inserttelpersoemploye.Text);
                cmd.Parameters.AddWithValue("@email", insertemailemploye.Text);
                cmd.Parameters.AddWithValue("@idsite", resultOfCmbSite);
                cmd.Parameters.AddWithValue("@idservice", resultOfCmbService);
                var count = cmd.ExecuteNonQuery();
                if (count == 1)
                {
                    MessageBox.Show("Vous avez inseré " + count + " employé");
                    InsertEmployebox.Visibility = System.Windows.Visibility.Collapsed;
                }

                // Clear the textbox input for new employe
                insertnomemploye.Text = String.Empty;
                insertprenomemploye.Text = String.Empty;
                inserttelproemploye.Text = String.Empty;
                inserttelpersoemploye.Text = String.Empty;
                insertemailemploye.Text = String.Empty;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }

        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InsertEmployebox.Visibility = System.Windows.Visibility.Collapsed;
        }


        private void Rechercher(object sender, RoutedEventArgs e)
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
                string sql;

                string barrerecherche = rechercheemploye.Text;

                sql = "SELECT * FROM `employes` JOIN `site` ON (employes.idsite = site.idsite) JOIN `services` ON (employes.idservice = services.idservice) WHERE UPPER(nom) LIKE '%" + barrerecherche + "%' OR UPPER(prenom) LIKE '%" + barrerecherche + "%';";

                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                //cmd.Parameters.AddWithValue("@recherche", barrerecherche.ToUpper());
                MySqlDataReader reader = cmd.ExecuteReader();


                List<EmployeModel> listemployeModel = new List<EmployeModel>();

                while (reader.Read())
                {
                    EmployeModel _employe = new EmployeModel(Convert.ToInt32(reader["idemploye"]), Convert.ToString(reader["nom"]), Convert.ToString(reader["prenom"]), Convert.ToString(reader["tel_pro"]), Convert.ToString(reader["tel_perso"]), Convert.ToString(reader["email"]), Convert.ToString(reader["nomservice"]), Convert.ToString(reader["nomsite"]));
                    listemployeModel.Add(_employe);
                }
                DataGridEmploye.ItemsSource = listemployeModel;
                MyCon.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }
        }

        private void buttondeleteemploye_Click(object sender, RoutedEventArgs e)
        {
            EmployeModel Employedelete = (EmployeModel)DataGridEmploye.SelectedItem;
            if (Employedelete != null )
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
                    string sql = "DELETE FROM `employes` WHERE idemploye = @id;";
                    MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                    cmd.Parameters.AddWithValue("@id", Employedelete.IdEmploye);
                    var count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        MessageBox.Show("Vous avez supprimé " + count + " employé");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Problème" + ex);
                }
            } else
            {
                MessageBox.Show("Aucun employé a été selectionné");
            }
        }

        private void buttondeleteemploye_Copy_Click(object sender, RoutedEventArgs e)
        {
            ModifyEmployebox.Visibility = System.Windows.Visibility.Visible;
        }

        private void NoButton_ClickModify(object sender, RoutedEventArgs e)
        {
            ModifyEmployebox.Visibility = System.Windows.Visibility.Collapsed;
        }

        // Code for update the employe info
        private void Button_UpdateEmploye(object sender, RoutedEventArgs e)
        {
            EmployeModel EmployeUpdate = (EmployeModel)DataGridEmploye.SelectedItem;
            try
            {
                // Getting the data of the combobox for new employee
                string valueOfCmbSite = cmbSiteModify.Text;
                string valueOfCmbService = cmbService.Text;

                // Switch for the location of new employee
                switch (valueOfCmbSite)
                {
                    case "Paris": resultOfCmbSite = 1; break;
                    case "Nantes": resultOfCmbSite = 2; break;
                    case "Toulouse": resultOfCmbSite = 3; break;
                    case "Nice": resultOfCmbSite = 4; break;
                    case "Lille": resultOfCmbSite = 5; break;
                }

                // Switch for the service of new employee
                switch (valueOfCmbService)
                {
                    case "Informatique": resultOfCmbService = 1; break;
                    case "Commercial": resultOfCmbService = 2; break;
                    case "Comptabilité": resultOfCmbService = 3; break;
                    case "Administration": resultOfCmbService = 4; break;
                    case "Sécurité": resultOfCmbService = 5; break;
                    case "Ouvrier": resultOfCmbService = 6; break;
                    case "Communication": resultOfCmbService = 7; break;

                }


                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "";
                conn_string.Database = "terrios";


                MySqlConnection MyCon = new MySqlConnection(conn_string.ToString());

                MyCon.Open();
                string sql = "UPDATE `employes` SET `nom`=@nom,`prenom`=@prenom,`tel_pro`=@telpro',`tel_perso`=@telperso,`email`=@email,`idsite` = @idsite,`idservice`=@idsite WHERE `idemploye` = @id;";
                MySqlCommand cmd = new MySqlCommand(sql, MyCon);
                cmd.Parameters.AddWithValue("@id", EmployeUpdate.IdEmploye);
                cmd.Parameters.AddWithValue("@nom", updatenomemploye.Text);
                cmd.Parameters.AddWithValue("@prenom", updatenomemploye.Text);
                cmd.Parameters.AddWithValue("@telpro", updatenomemploye.Text);
                cmd.Parameters.AddWithValue("@telperso", updatenomemploye.Text);
                cmd.Parameters.AddWithValue("@email", updatenomemploye.Text);
                cmd.Parameters.AddWithValue("@idsite", EmployeUpdate.SiteEmploye);
                cmd.Parameters.AddWithValue("@idservice", EmployeUpdate.ServiceEmploye);
                var count = cmd.ExecuteNonQuery();
                if (count == 1)
                {
                    MessageBox.Show("Vous avez modifié " + count + " employé");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Problème" + ex);
            }
        }

        private void buttondeleteemploye_Copy_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /*private void buttonmodifieremploye(object sender, RoutedEventArgs e)
        {
            EmployeModel EmployeUpdate = (EmployeModel)DataGridEmploye.SelectedItem;
            ModifyEmployebox.Visibility = System.Windows.Visibility.Visible;
            UpdateEmploye(EmployeModel.Idemploye);

        }
**/
    }
}
