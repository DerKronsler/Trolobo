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
using System.Data;
using System.Data.SqlClient;
using Trolobo.TroloDBDataSetTableAdapters;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für kundeVerwalten.xaml
    /// </summary>
    public partial class kundeVerwalten : Window
    {
        public int test;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");
        public kundeVerwalten()
        {
            InitializeComponent();
            con.Open();
            refr();
            // firmenDataGrid.ContextMenu;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Trolobo.TroloDBDataSet troloDBDataSet = ((Trolobo.TroloDBDataSet)(this.FindResource("troloDBDataSet")));
            // Lädt Daten in Tabelle "Firmen". Sie können diesen Code nach Bedarf ändern.
            Trolobo.TroloDBDataSetTableAdapters.FirmenTableAdapter troloDBDataSetFirmenTableAdapter = new Trolobo.TroloDBDataSetTableAdapters.FirmenTableAdapter();
            troloDBDataSetFirmenTableAdapter.Fill(troloDBDataSet.Firmen);
            System.Windows.Data.CollectionViewSource firmenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("firmenViewSource")));
            firmenViewSource.View.MoveCurrentToFirst();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {

           // con.Open();
            DataRowView dataRowView = firmenDataGrid.SelectedItem as DataRowView;
            String Test = dataRowView["FID"].ToString();
            test = Int32.Parse(Test);

            SqlCommand del = con.CreateCommand();
            SqlCommand delauftrag = con.CreateCommand();
            delauftrag.CommandType = CommandType.Text;
            delauftrag.CommandText = "delete from Auftraege where FID = @fidA";
            delauftrag.Parameters.AddWithValue("@fidA", test);
            delauftrag.ExecuteNonQuery();
            del.CommandType = CommandType.Text;
            del.CommandText = "delete from Firmen where FID = @fid";
            del.Parameters.AddWithValue("@fid", test);
            del.ExecuteNonQuery();
            refr();
            // MessageBox.Show(Test);
            // con.Close();
        }

        private void refr()
        {
            //con.Open();
            string sqlstm = "Select * from Firmen";
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstm, con);
            DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds, "Firmen");
            firmenDataGrid.DataContext = ds.Tables[0];
           // con.Close();
        }

        private void Kunde_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = firmenDataGrid.SelectedItem as DataRowView;
            String Test = dataRowView["FID"].ToString();
            test = Int32.Parse(Test);
            firmenInfo firmenInfo = new firmenInfo(test);    
            firmenInfo.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            con.Close();
            Visibility = Visibility.Collapsed;
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.Show();

  
            

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = firmenDataGrid.SelectedItem as DataRowView;
            String Test = dataRowView["FID"].ToString();
            test = Int32.Parse(Test);
            KundenUpdate kundenUpdate = new KundenUpdate(test);
            kundenUpdate.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            refr();
        }
    }
}
