using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Trolobo
{
    /// <summary>
    /// Interaktionslogik für firmenInfo.xaml
    /// </summary>
    public partial class firmenInfo : Window
    {
        public int uebergabe;
        public int aid;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\mkronenberger\\source\\repos\\Trolobo\\Trolobo\\TroloDB.mdf;Integrated Security=True");
        public firmenInfo(int test)
        {
            InitializeComponent();
            uebergabe = test;
            con.Open();
            refr();
            con.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                Trolobo.TroloDBDataSet troloDBDataSet = ((Trolobo.TroloDBDataSet)(this.FindResource("troloDBDataSet")));
                // Lädt Daten in Tabelle "Auftraege". Sie können diesen Code nach Bedarf ändern.
                Trolobo.TroloDBDataSetTableAdapters.AuftraegeTableAdapter troloDBDataSetAuftraegeTableAdapter = new Trolobo.TroloDBDataSetTableAdapters.AuftraegeTableAdapter();
                troloDBDataSetAuftraegeTableAdapter.Fill(troloDBDataSet.Auftraege);
                System.Windows.Data.CollectionViewSource auftraegeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("auftraegeViewSource")));
                auftraegeViewSource.View.MoveCurrentToFirst();
                // Lädt Daten in Tabelle "Firmen". Sie können diesen Code nach Bedarf ändern.
                Trolobo.TroloDBDataSetTableAdapters.FirmenTableAdapter troloDBDataSetFirmenTableAdapter = new Trolobo.TroloDBDataSetTableAdapters.FirmenTableAdapter();
                troloDBDataSetFirmenTableAdapter.Fill(troloDBDataSet.Firmen);
                System.Windows.Data.CollectionViewSource firmenViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("firmenViewSource")));
                firmenViewSource.View.MoveCurrentToFirst();
            }
            catch
            {

                
            }
        }
        private void refr()
        {
            string sqlstm = "Select AID, FID, Beschreibung, Auftragshoehe + '€' as Auftragshoehe from Auftraege where FID = " + uebergabe;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstm, con);
            DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds, "Auftraege");
            auftraegeDataGrid.DataContext = ds.Tables[0];
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            con.Open();    
            DataRowView dataRowView = auftraegeDataGrid.SelectedItem as DataRowView;
            String Test = dataRowView["AID"].ToString();
            int test = Int32.Parse(Test);

            SqlCommand del = con.CreateCommand();
            SqlCommand delauftrag = con.CreateCommand();
            delauftrag.CommandType = CommandType.Text;
            delauftrag.CommandText = "delete from Auftraege where AID = @AidA";
            delauftrag.Parameters.AddWithValue("@AidA", test);
            delauftrag.ExecuteNonQuery();
            refr();
            con.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = auftraegeDataGrid.SelectedItem as DataRowView;
            String Test = dataRowView["AID"].ToString();
            aid = Int32.Parse(Test);
            auftragUpdate auftragUpdate1 = new auftragUpdate(aid);
            auftragUpdate1.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            refr();
        }
    }
}



